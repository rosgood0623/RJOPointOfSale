using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RJOPointOfSale.Properties;
using HiawathaSocketAsync;

namespace RJOPointOfSale
{
    public partial class MainPoSForm : Form
    {
        /*TODO: Implement the Clear button.*/
        /*TODO: Implement Pricing
          TODO: Later On: A system clock on the this form*/
        private readonly HiawathaSocketServer m_server = new HiawathaSocketServer();
        private DatabaseConnection m_conn;        
        private int m_numOfRetrievedRows;
        
        private readonly Form1 m_enterCodeForm = new Form1();
        private readonly List<Button> m_customerTabsButtons = new List<Button>();
        private readonly List<CustomerCheck> m_customerChecks = new List<CustomerCheck>();
        private CustomerCheckView m_customerCheckView;

        private int m_buttonLocationFormatter;      
        private const int m_buttonSizeX = 45;
        private const int m_buttonSizeY = 45;
        private int m_buttonID;
        private int m_currentlySelectedTab = 0;
        private const int m_maximumTabCount = 5;
        private const int m_removeEntree = 0;
        private const int m_removeSide = 1;
        private const int m_removeBeverage = 2;
        private int m_sisterApplicationID;
        private int m_numOfSentLines = 0;


        private readonly int m_employeeCode;

        private RadioButton m_selectedSignature;

        public MainPoSForm()
        {
            InitializeComponent();
        }

        public MainPoSForm(int a_enteredCode) : this()
        {
            m_employeeCode = a_enteredCode;
        }

        private void MainPoSForm_Load(object sender, EventArgs e)
        {
            Button defaultTabButton = new Button
            {
                Size = new Size(m_buttonSizeX, m_buttonSizeY),
                Location = new Point(0, 10)
            };

            defaultTabButton.Click += BtnTabInfo_Click;
            defaultTabButton.Tag = m_buttonID;
            m_buttonID++;

            flpTabs.Controls.Add(defaultTabButton);
            m_customerTabsButtons.Add(defaultTabButton);

            CustomerCheck defaultCustomerCheck = new CustomerCheck();
            m_customerChecks.Add(defaultCustomerCheck);
            m_customerCheckView = new CustomerCheckView(defaultCustomerCheck);

            InitializeDatabaseConnection();
            RecognizeEmployee();
            RecognizeEmployeeTitle();

            lbCustomerCheck.DataSource = m_customerCheckView.GetItemsForDisplay();
            
        }

        private void BtnNewTab_Click(object sender, EventArgs e)
        {
            if (m_customerTabsButtons.Count < m_maximumTabCount)
            {
                m_buttonLocationFormatter += 45;

                Button newButton = new Button
                {
                    Size = new Size(m_buttonSizeX, m_buttonSizeY),
                    Location = new Point(m_buttonLocationFormatter, 10)
                    
                };

                newButton.Click += BtnTabInfo_Click;
                newButton.AutoEllipsis = true;
                newButton.Tag = m_buttonID;
                m_buttonID++;

                CustomerCheck newCheck = new CustomerCheck();
                m_customerChecks.Add(newCheck);
                
                m_customerTabsButtons.Add(newButton);
                flpTabs.Controls.Add(newButton);
            }
            else
            {
                MessageBox.Show(@"Maximum tab capacity reached.");
            }
            
        }

        private void BtnTabInfo_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            m_currentlySelectedTab = (int)currentButton.Tag;
            m_customerCheckView.ClearMembersForReinitialization();
            m_customerCheckView.UpdateMembersForDisplay(m_customerChecks[m_currentlySelectedTab]);
        }

        private void BtnCloseTab_Click(object sender, EventArgs e)
        {
            if(m_customerTabsButtons.Count == 1 && m_currentlySelectedTab == 0) { return; }

            for(int i = 0; i < m_customerTabsButtons.Count; i++)
            {
                Button temp = m_customerTabsButtons[i];
                if((int)temp.Tag == m_currentlySelectedTab)
                {
                    temp = m_customerTabsButtons[i];
                    flpTabs.Controls.Remove(temp);
                    m_customerTabsButtons.RemoveAt(i);
                    m_customerChecks.RemoveAt(i);
                }
            }
            
            RedefineButtonTags();
        }

        private void RedefineButtonTags()
        {
            int tagRedefinition = 0;
            foreach(Button b in m_customerTabsButtons)
            {
                b.Tag = tagRedefinition;
                tagRedefinition++;
            }
            m_buttonID = tagRedefinition;
        }

        private void RecognizeEmployee()
        {
            try
            {
                //string sql_statement = @"SELECT ""firstName"" FROM public.""tbl_employees"" WHERE ""employeeCode"" = :enteredCode";
                string sql_statement = @"SELECT firstName FROM public.tbl_employees WHERE employeeCode = :enteredCode";
                m_conn.Query = sql_statement;
                m_conn.SetLogin = m_employeeCode;

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = m_conn.QueryEmployeeIdentification();
                m_numOfRetrievedRows = data_set.Count;

                if (m_numOfRetrievedRows > 0)
                {
                    string employeeName = data_set.ElementAt(0).GetAtIndex(0);
                    lblEmployee.Text = employeeName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void RecognizeEmployeeTitle()
        {
            try
            {
                //string sql_statement = @"SELECT ""tbl_jobCodes"".""roleName"" FROM public.""tbl_jobCodes"" JOIN ""tbl_employees"" ON ""tbl_employees"".""jobCode"" = ""tbl_jobCodes"".""code"" WHERE ""employeeCode"" = :enteredCode";
                string sql_statement = @"SELECT tbl_jobCodes.roleName FROM public.tbl_jobCodes JOIN tbl_employees ON tbl_employees.jobCode = tbl_jobCodes.code WHERE employeeCode = :enteredCode";
                m_conn.Query = sql_statement;
                m_conn.SetLogin = m_employeeCode;

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = m_conn.QueryEmployeeIdentification();
                m_numOfRetrievedRows = data_set.Count;

                if (m_numOfRetrievedRows > 0)
                {
                    string employeeRole = data_set.ElementAt(0).GetAtIndex(0);
                    lblEmployeeJob.Text = employeeRole;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void InitializeDatabaseConnection()
        {
            try
            {
                //m_conn = new DatabaseConnection(Settings.Default.PostgresSQLConnection);    
                m_conn = new DatabaseConnection(Settings.Default.ElephantSQLConnection);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void BtnSetCheckName_Click(object sender, EventArgs e)
        {
            Button selectedTab = m_customerTabsButtons[m_currentlySelectedTab];
            if (selectedTab.Text.Length > 0) { return; }
            CustomerCheck checkAtSelectedTab = m_customerChecks[m_currentlySelectedTab];

            KeyboardForm keyboardForm = new KeyboardForm(checkAtSelectedTab);
            keyboardForm.ShowDialog();
            selectedTab.Text = checkAtSelectedTab.Name;
        }

        private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selection = sender as RadioButton;
        
            if (selection.Checked)
            {
                m_selectedSignature = selection;
            }
        }

        private void BtnSignatureAndProteinSelection_Click(object sender, EventArgs e)
        {
            if (m_selectedSignature == null) { return; }

            Button proteinButton = sender as Button;
            Entree signature = new Entree();
            
            //On the menu, The Classic Signature is the only sandwich which recipe differs depending on the protein selection.
            //This feels like an iffy way to do it, but in the interest of keeping a generic method for ringing up a sandwich,
            //this is the best solution I could think of at the time I fleshed out this part of the project.
            
            if (proteinButton.Text.Equals("Grilled") && m_selectedSignature.Text.Equals("Classic Smash"))
            {
                //Since there is no "Classic Chicken" button, I just need to pass "ClassicChicken" so the db knows to get the chicken version of this signature
                signature.RetrieveAttributesFromDb("ClassicChicken");
            }
            else if (proteinButton.Text.Equals("Crispy") && m_selectedSignature.Text.Equals("Classic Smash"))
            {
                signature.RetrieveAttributesFromDb("ClassicCrispy");
            }
            else
            {
                signature.RetrieveAttributesFromDb((string)m_selectedSignature.Tag);

            }

            signature.SetProteinType(proteinButton.Text);
            AttributeModification atModForm = new AttributeModification(signature);
            atModForm.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddEntreeToMeal(signature);
            m_selectedSignature.Checked = false;
            m_selectedSignature = null;
            UpdateDisplay();
        }

        private void BtnKidsMeals_Click(object sender, EventArgs e)
        {
            Button kidsMealButton = sender as Button;
            Entree sandwich = new Entree();

            sandwich.RetrieveAttributesFromDb((string)kidsMealButton.Tag);

            KidsMeal kidsMeal = new KidsMeal();
            kidsMeal.AddEntreeToMeal(sandwich);
            KidsMealModification kmm = new KidsMealModification(kidsMeal);
            kmm.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].AddMealToCheck(kidsMeal);
            UpdateDisplay();

        }

        private void BtnSideSelection_Click(object sender, EventArgs e)
        {
            Button sideSelection = sender as Button;
            Side side = new Side();

            side.RetrieveSideFromDb((string)sideSelection.Tag);
            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddSideToMeal(side);

            UpdateDisplay();
        }

        private void BtnBeverageSelection_Click(object sender, EventArgs e)
        {
            Button bevButton = sender as Button;
            Beverage beverage = new Beverage();

            beverage.RetrieveBevFromDb((string)bevButton.Tag);
            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddBeverageToMeal(beverage);

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            m_customerCheckView.UpdateMembersForDisplay(m_customerChecks[m_currentlySelectedTab]);
        }

        private void LbCustomerCheck_Format(object sender, ListControlConvertEventArgs e)
        {
            //string toBeFormatted = e.ListItem.ToString();
            //string[] values = toBeFormatted.Split('\n');
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            Tuple<int, int> mealTuple = GetDisplaySelection();
            bool determineIfKidsMeal = DetermineIfKidsMeal(mealTuple.Item1);
            Entree toBeModified = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(mealTuple.Item1).GetEntree();
            if (mealTuple.Item2 != 0)
            {
                MessageBox.Show(@"You can only modify entrees");
                return;
            }

            if (determineIfKidsMeal)
            {
                KidsMeal chosenKidsMeal = (KidsMeal) m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(mealTuple.Item1);
                KidsMealModification kmm = new KidsMealModification(chosenKidsMeal);
                kmm.ShowDialog();
                UpdateDisplay();
            }
            else
            {
                AttributeModification modifySelection = new AttributeModification(toBeModified);
                modifySelection.ShowDialog();
                UpdateDisplay();
            }

        }

        private void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            Tuple<int, int> mealTuple = GetDisplaySelection();
            bool voidOrDelete = DetermineIfVoidOrDelete(mealTuple.Item1);

            if (voidOrDelete)
            {
                //void
                //1) Get selected item
                //2) Parse into string attributes
                //3) listOfItems.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
                //4) Add a VOID command at the front of the attributes.
            }
            else
            {
                DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
            }
            
            m_customerChecks[m_currentlySelectedTab].ClearEmptyMeals();
            m_customerCheckView.UpdateMembersForDisplay(m_customerChecks[m_currentlySelectedTab]);

        }

        private bool DetermineIfVoidOrDelete(int a_mealIndex)
        {
            CustomerCheck currentlySelectedCheck = m_customerChecks.ElementAt(m_currentlySelectedTab);
            Meal currentlySelectedMeal = currentlySelectedCheck.GetMealAtIndex(a_mealIndex);
            return currentlySelectedMeal.SentFlag;
        }

        private void DeleteItemOnTill(int a_mealIndex, int a_itemIndex)
        {
            bool determineIfKidsMeal = DetermineIfKidsMeal(a_mealIndex);

            if (determineIfKidsMeal)
            {
                DeleteKidsMealAtIndex(a_mealIndex);
            }
            else
            {
                DeleteItemWithinMealAtIndex(a_mealIndex, a_itemIndex);
            }
        }

        private void DeleteKidsMealAtIndex(int a_index)
        {
            m_customerChecks[m_currentlySelectedTab].DeleteMealAtIndex(a_index);
        }

        private bool DetermineIfKidsMeal(int a_targetMeal)
        {
            Meal toBeDetermined = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_targetMeal);
            return toBeDetermined.IsKidsMeal();
        }

        private Tuple<int, int> GetDisplaySelection()
        {
            int indexOfListBoxSelection = lbCustomerCheck.SelectedIndex;
            m_customerChecks[m_currentlySelectedTab].CompileMealMatrix();
            int indexOfMeal = m_customerCheckView.GetIndexOfSelected(indexOfListBoxSelection);
            Tuple<int, int> mealTuple = m_customerChecks[m_currentlySelectedTab].FindMealViaMatrix(indexOfMeal);

            return mealTuple;
        }

        private void DeleteItemWithinMealAtIndex(int a_mealIndex, int a_itemIndex)
        {
            switch (a_itemIndex)
            {
                case m_removeEntree:
                    m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex).RemoveEntree();
                    break;
                case m_removeSide:
                    m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex).RemoveSide();
                    break;
                case m_removeBeverage:
                    m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex).RemoveBeverage();
                    break;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            //TODO: Simply clearing the check is easy, IF no orders have been sent to screen. Will come back to this later. 
        }

        private void LbCustomerCheck_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            string formattable = lbCustomerCheck.Items[e.Index].ToString();

            if (e.Index < m_numOfSentLines)
            {
                e.Graphics.DrawString(formattable, new Font("Arial", 10, FontStyle.Italic), Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
            else
            {
                e.Graphics.DrawString(formattable, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
            
        }

        private void BtnBeverages_Click(object sender, EventArgs e)
        {
            Beverage bevFromSelectionForm = new Beverage();
            BeverageSelectionForm bsf = new BeverageSelectionForm(bevFromSelectionForm);
            bsf.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddBeverageToMeal(bevFromSelectionForm);
            UpdateDisplay();
        }

        private void MainPoSForm_VisibleChanged(object sender, EventArgs e)
        {
            if (m_sisterApplicationID == 0)
            {
                LaunchSisterApplication();
            }
        }

        private string ParseRelativePath()
        {
            string startPath = Application.StartupPath;
            int endIndex = startPath.IndexOf(@"\RJOPointOfSale\", StringComparison.Ordinal);
            string partialPath = startPath.Substring(0, endIndex);
            string relativePath = partialPath + @"\RJOPointOfSale\KitchenScreenClient\bin\Debug\KitchenScreenClient.exe";

            return relativePath;
        }

        private void LaunchSisterApplication()
        {
            m_server.StartListeningForIncomingConnection();
            string relativePath = ParseRelativePath();

            if (File.Exists(relativePath))
            {
                Process sister = new Process
                {
                    StartInfo =
                    {
                        FileName = relativePath
                    }
                };

                sister.Start();
                m_sisterApplicationID = sister.Id;
            }

            
            
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Hide();
            KillSisterProcess();
            m_sisterApplicationID = 0;
            m_enterCodeForm.Show(this);
        }

        private void KillSisterProcess()
        {
            try
            {
                Process sister = Process.GetProcessById(m_sisterApplicationID);
                sister.Kill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void BtnSendOrdersOnTill_Click(object sender, EventArgs e)
        {
            List<string> listOfItems = new List<string>();
            listOfItems = m_customerCheckView.GetUnsentItemsForSending();
            listOfItems.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
            FlagMealsOnTillAsSent();

            if (listOfItems.Count <= 1)
            {
                return;
            }

            string toBeSent = string.Join(string.Empty, listOfItems);
            m_server.SendToAll(toBeSent);
            m_numOfSentLines = m_customerCheckView.GetCountInDisplayOfSentItems();
            UpdateDisplay();
        }

        private void SendVoidCommandOfSelectedItemToClients()
        {

        }

        private void FlagMealsOnTillAsSent()
        {
            CustomerCheck currentlySelected = m_customerChecks[m_currentlySelectedTab];
            for (int i = 0; i < currentlySelected.NumberOfMeals(); i++)
            {
                Meal mealOnTill = currentlySelected.GetMealAtIndex(i);
                mealOnTill.SentFlag = true;
            }
        }
    }
}
