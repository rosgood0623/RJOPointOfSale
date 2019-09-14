using HiawathaSocketAsync;
using RJOPointOfSale.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    /// <summary>
    /// This is the Point of Sale main menu. The main hub for ringing up
    /// all types of menu items and sending those items to the KitchenScreenClient.
    /// </summary>
    /// <remarks>
    /// NAME: MainPoSForm
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public partial class MainPoSForm : Form
    {
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
        private int m_currentlySelectedTab;
        private const int m_maximumTabCount = 5;
        private const int m_removeEntree = 0;
        private const int m_removeSide = 1;
        private const int m_removeBeverage = 2;
        private int m_sisterApplicationID;
        private int m_numOfSentLines = 0;


        private readonly int m_employeeCode;

        private RadioButton m_selectedSignature;

        /// <summary>
        /// The default constructor for the main register menu. 
        /// </summary>
        /// //<remarks>
        /// NAME: MainPoSForm
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        public MainPoSForm()
        {
            InitializeComponent();
        }/*public MainPoSForm()*/

        /// <summary>
        /// A constructor for the main register menu that retrieves the employee code from the passcode form.
        /// </summary>
        /// <remarks>
        /// NAME: MainPoSForm
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_enteredCode"> The employees code as an integer</param>
        public MainPoSForm(int a_enteredCode) : this()
        {
            m_employeeCode = a_enteredCode;
        }/*public MainPoSForm(int a_enteredCode) : this()*/

        /// <summary>
        /// The Load event for the main register menu. This event is raised whenever the form is constructed
        /// This method sets up the menu for use: Generates the default check and its representative button,
        /// establishes connection with the database and sets the Listbox's datasource to the BindingList
        /// </summary>
        /// //<remarks>
        /// NAME: MainPoSForm_Load
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender"> The object that the event is attached to </param>
        /// <param name="e"> The EventArgs of the raised event. </param>
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
        }/*private void MainPoSForm_Load(object sender, EventArgs e)*/

        /// <summary>
        /// This click event is raised whenever the user wants to create a new customer tab.
        /// A customer check object is created along with its representative button. Only 
        /// allows up to 5 tabs max.
        /// </summary>
        /// <remarks>
        /// NAME: BtnNewTab_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event</param>
        /// <param name="e">The EventArgs of the raised event</param>
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

        }/*private void BtnNewTab_Click(object sender, EventArgs e)*/

        /// <summary>
        /// A click event that is raised whenever the user clicks tab.
        /// The Listbox is updated with that tabs information and the user can proceed with
        /// adding new items to that check
        /// </summary>
        /// <remarks>
        /// NAME: BtnTabInfo_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event</param>
        /// <param name="e">The EventArgs associated with the event</param>
        private void BtnTabInfo_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            m_currentlySelectedTab = (int)currentButton.Tag;
            UpdateDisplay();
        }/*private void BtnTabInfo_Click(object sender, EventArgs e)*/

        /// <summary>
        /// The event raised whenever the user instructs the PoS to close a tab. 
        /// Will always sustains at least one tab.
        /// </summary>
        /// <remarks>
        /// NAME: BtnCloseTab_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event</param>
        /// <param name="e"> The EventArgs associated with the event</param>
        private void BtnCloseTab_Click(object sender, EventArgs e)
        {
            if (m_customerTabsButtons.Count == 1 && m_currentlySelectedTab == 0) { return; }

            for (int i = 0; i < m_customerTabsButtons.Count; i++)
            {
                Button temp = m_customerTabsButtons[i];
                if ((int)temp.Tag == m_currentlySelectedTab)
                {
                    temp = m_customerTabsButtons[i];
                    flpTabs.Controls.Remove(temp);
                    m_customerTabsButtons.RemoveAt(i);
                    m_customerChecks.RemoveAt(i);
                }
            }

            RedefineButtonTags();
        }/*private void BtnCloseTab_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Iterates through the list of currently open tabs and redefines their tags.
        /// Is called when the number of open tabs are modified. Important to ensure Model/View
        /// synchronicity
        /// </summary>
        /// <remarks>
        /// NAME: RedefineButtonTags
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        private void RedefineButtonTags()
        {
            int tagRedefinition = 0;
            foreach (Button b in m_customerTabsButtons)
            {
                b.Tag = tagRedefinition;
                tagRedefinition++;
            }
            m_buttonID = tagRedefinition;
        }/*private void RedefineButtonTags()*/

        /// <summary>
        /// Queries the database for the employees information
        /// Retrieves the information based on the employee code used to access the register.
        /// Used to display the employees name
        /// </summary>
        /// <remarks>
        /// NAME: RecognizeEmployee
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        private void RecognizeEmployee()
        {
            try
            {
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

        }/*private void RecognizeEmployee()*/

        /// <summary>
        /// Queries database to for employee information
        /// Retrieves the information based on the employee code used the access the register.
        /// Used to retrieve the employees Title
        /// </summary>
        /// <remarks>
        /// NAME: RecognizeEmployeeTitle
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        private void RecognizeEmployeeTitle()
        {
            try
            {
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

        }/*private void RecognizeEmployeeTitle()*/

        /// <summary>
        /// Establishes the connection with the database.
        /// </summary>
        /// <remarks>
        /// NAME: InitializeDatabaseConnection
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
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
        }/*private void InitializeDatabaseConnection()*/

        /// <summary>
        /// This event is raised whenever the user wants to add a check identifier to the tab.
        /// A keyboard form is constructed for input.
        /// </summary>
        /// <remarks>
        /// NAME: BtnSetCheckName_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender"> The button that raised the event</param>
        /// <param name="e">The EventArgs of the event</param>
        private void BtnSetCheckName_Click(object sender, EventArgs e)
        {
            Button selectedTab = m_customerTabsButtons[m_currentlySelectedTab];
            if (selectedTab.Text.Length > 0) { return; }
            CustomerCheck checkAtSelectedTab = m_customerChecks[m_currentlySelectedTab];

            KeyboardForm keyboardForm = new KeyboardForm(checkAtSelectedTab);
            keyboardForm.ShowDialog();
            selectedTab.Text = checkAtSelectedTab.Name;
        }/*private void BtnSetCheckName_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is raised whenever the user chooses a signature sandwich.
        /// The radio button is saved for when the customer chooses a protein option
        /// </summary>
        /// <remarks>
        /// NAME: RadioButtonSelection_CheckedChanged
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The radio button that raised the event</param>
        /// <param name="e"> The EventArgs of the event</param>    
        private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selection = sender as RadioButton;

            if (selection.Checked)
            {
                m_selectedSignature = selection;
            }
        }/*private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)*/

        /// <summary>
        /// This event is raised whenever the user selects a protein option. If a signature hasn't 
        /// been selected yet, return. Otherwise, create the desired Entree object by retrieving 
        /// the sandwiches attributes from the database.
        /// </summary>
        /// <remarks>
        /// NAME: BtnSignatureAndProteinSelection_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised this event</param>
        /// <param name="e">The EventArgs of the event</param>
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
                signature.RetrieveBasePriceFromDb("ClassicChicken");
            }
            else if (proteinButton.Text.Equals("Crispy") && m_selectedSignature.Text.Equals("Classic Smash"))
            {
                signature.RetrieveAttributesFromDb("ClassicCrispy");
                signature.RetrieveBasePriceFromDb("ClassicCrispy");

            }
            else
            {
                signature.RetrieveAttributesFromDb((string)m_selectedSignature.Tag);
                signature.RetrieveBasePriceFromDb((string)m_selectedSignature.Tag);
            }

            signature.SetProteinType(proteinButton.Text);
            AttributeModification atModForm = new AttributeModification(signature);
            atModForm.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddEntreeToMeal(signature);
            m_selectedSignature.Checked = false;
            m_selectedSignature = null;
            Debug.WriteLine(signature.CalculatePrice());
            UpdateDisplay();
        }/*private void BtnSignatureAndProteinSelection_Click(object sender, EventArgs e)*/


        /// <summary>
        /// This click event is triggered whenever the user selects one of the salad buttons. A new Entree is
        /// created which queries the database for the salad attributes, which is then
        /// passed into the attribute modification form. 
        /// </summary>
        /// <remarks>
        /// NAME: BtnSaladSelection_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the salad buttons</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnSaladSelection_Click(object sender, EventArgs e)
        {
            Button selectedSalad = sender as Button;
            Entree signature = new Entree();

            signature.RetrieveAttributesFromDb((string)selectedSalad.Tag);
            signature.RetrieveBasePriceFromDb((string)selectedSalad.Tag);

            AttributeModification atModForm = new AttributeModification(signature);
            atModForm.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddEntreeToMeal(signature);
            UpdateDisplay();
        }/*BtnSaladSelection_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is raised when any of the Kids Meal options are selected. 
        /// The database is queried for the details of the meal using the 
        /// button's respective tag and the meal is added to the check
        /// </summary>
        /// <remarks>
        /// NAME: BtnKidsMeals_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event</param>
        /// <param name="e">The EventArgs of the event</param>       
        private void BtnKidsMeals_Click(object sender, EventArgs e)
        {
            Button kidsMealButton = sender as Button;
            Entree sandwich = new Entree();

            sandwich.RetrieveAttributesFromDb((string)kidsMealButton.Tag);
            sandwich.RetrieveBasePriceFromDb((string)kidsMealButton.Tag);

            KidsMeal kidsMeal = new KidsMeal();
            kidsMeal.AddEntreeToMeal(sandwich);
            KidsMealModification kmm = new KidsMealModification(kidsMeal);
            kmm.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].AddMealToCheck(kidsMeal);
            UpdateDisplay();
        }/*private void BtnKidsMeals_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is raised whenever the user selects a side. The database is queried for that 
        /// side's information using the button's respective tag and the side is added to the check.
        /// </summary>
        /// <remarks>
        /// NAME: BtnSideSelection_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event</param>
        /// <param name="e"> The EventArgs of the event</param>

        private void BtnSideSelection_Click(object sender, EventArgs e)
        {
            Button sideSelection = sender as Button;
            Side side = new Side();

            side.RetrieveSideFromDb((string)sideSelection.Tag);
            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddSideToMeal(side);

            UpdateDisplay();
        }/*private void BtnSideSelection_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is raised whenever the user selects any of the beverage buttons.
        /// The database is queried using the button's respective tag and the item is added to the check.
        /// </summary>
        /// <remarks>
        /// NAME: BtnBeverageSelection_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event.</param>
        /// <param name="e">The EventArgs of the event.</param>

        private void BtnBeverageSelection_Click(object sender, EventArgs e)
        {
            Button bevButton = sender as Button;
            Beverage beverage = new Beverage();

            beverage.RetrieveBevFromDb((string)bevButton.Tag);
            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddBeverageToMeal(beverage);

            UpdateDisplay();
        }/*private void BtnBeverageSelection_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Updates the view of the Listbox that represents the check's order information.
        /// </summary>
        /// <remarks>
        /// NAME: UpdateDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        private void UpdateDisplay()
        {
            m_customerCheckView.UpdateMembersForDisplay(m_customerChecks[m_currentlySelectedTab]);
            m_customerCheckView.ModifyLinesWithPrices();
            tbTotal.Text = m_customerChecks[m_currentlySelectedTab].CheckTotal.ToString();
        }/*private void UpdateDisplay()*/

        /// <summary>
        /// Handles the logic for when the user selects an item in the check for modification.
        /// Depending on the Meal type, open it's respective modification form to edit the attributes 
        /// of that item.
        /// </summary>
        /// <remarks>
        /// NAME: BtnModify_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event</param>
        /// <param name="e">The EventArgs of the event</param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (m_customerChecks[m_currentlySelectedTab].NumberOfMeals() == 0)
            {
                MessageBox.Show(@"No items to modify");
                return;
            }

            Tuple<int, int> mealTuple = GetDisplaySelection();
            bool determineIfKidsMeal = DetermineIfKidsMeal(mealTuple.Item1);

            if (mealTuple.Item2 != 0)
            {
                MessageBox.Show(@"You can only modify entrees or kids meals.");
                return;
            }

            if (determineIfKidsMeal)
            {
                ModifyKidsMeal(mealTuple.Item1);
            }
            else
            {
                Entree toBeModified = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(mealTuple.Item1).GetEntree();
                ModifyMeal(toBeModified);
            }

        }/*private void BtnModify_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This method modifies the KidMeal at the given index, where the index
        /// is the position the meal is in in the meal list.
        /// </summary>
        /// <remarks>
        /// NAME: ModifyKidsMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_mealIndex">The index of the meal to be modified</param>
        private void ModifyKidsMeal(int a_mealIndex)
        {
            KidsMeal chosenKidsMeal = (KidsMeal)m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex);
            KidsMealModification kmm = new KidsMealModification(chosenKidsMeal);
            kmm.ShowDialog();
            UpdateDisplay();
        }/*private void ModifyKidsMeal(int a_mealIndex)*/


        /// <summary>
        /// Modifies the meal passed into this method via the AttributeModification form.
        /// </summary>
        /// <remarks>
        /// NAME: ModifyMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_toBeModified">The Entree to be modified.</param>
        private void ModifyMeal(Entree a_toBeModified)
        {
            AttributeModification modifySelection = new AttributeModification(a_toBeModified);
            modifySelection.ShowDialog();
            UpdateDisplay();
        }/*private void ModifyMeal(Entree a_toBeModified)*/


        /// <summary>
        /// Handles the initial logic for deleting an item from the customer's check. If the user selected a 
        /// KidsMeal, delete all related menu items. Else, just delete the selected item.
        /// </summary>
        /// <remarks>
        /// NAME: BtnDeleteItem_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that raised the event.</param>
        /// <param name="e">The EventArgs of the event.</param>
        private void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            if (ValidateDeleteAction()) { return;}

            Tuple<int, int> mealTuple = GetDisplaySelection();
            bool voidOrDelete = DetermineIfVoidOrDelete(mealTuple.Item1);
            bool isKidsMeal = DetermineIfKidsMeal(mealTuple.Item1);

            if (voidOrDelete && !isKidsMeal)
            {
                switch (mealTuple.Item2)
                {
                    case m_removeEntree:
                        SendVoidCommandForEntree(mealTuple.Item1);
                        DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
                        break;
                    case m_removeSide:
                        SendVoidCommandForSide(mealTuple.Item1);
                        DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
                        break;
                    case m_removeBeverage:
                        SendVoidCommandForBeverage(mealTuple.Item1);
                        DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
                        break;
                }
            }
            else if (voidOrDelete)
            {
                SendVoidCommandForKidsMeal(mealTuple.Item1);
                DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
            }
            else if (isKidsMeal)
            {
                DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
            }
            else
            {
                DeleteItemOnTill(mealTuple.Item1, mealTuple.Item2);
            }
            
            m_customerChecks[m_currentlySelectedTab].ClearEmptyMeals();
            UpdateDisplay();

        }/*private void BtnDeleteItem_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Ensures there are items on the till to delete to avoid index out of range exceptions.
        /// </summary>
        /// <remarks>
        /// NAME: ValidateDeleteAction
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/1/2019
        /// </remarks>
        /// <returns>True if there are no items to delete, false otherwise.</returns>
        private bool ValidateDeleteAction()
        {
            if (m_customerChecks[m_currentlySelectedTab].NumberOfMeals() == 0)
            {
                MessageBox.Show(@"No items to delete");
                return true;
            }

            return false;
        }/*private bool ValidateDeleteAction()*/

        /// <summary>
        /// Determines if the current delete action is a void or standard delete. A void is
        /// performed when the user wants to delete an item that has already been sent to
        /// the KitchenScreenClient. 
        /// </summary>
        /// <remarks>
        /// NAME: DetermineIfVoidOrDelete
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_mealIndex">The position of the meal within the meal list.</param>
        /// <returns>
        /// Returns true if the action is a void, false if standard delete.
        /// </returns>
        private bool DetermineIfVoidOrDelete(int a_mealIndex)
        {
            CustomerCheck currentlySelectedCheck = m_customerChecks.ElementAt(m_currentlySelectedTab);
            Meal currentlySelectedMeal = currentlySelectedCheck.GetMealAtIndex(a_mealIndex);
            return currentlySelectedMeal.SentFlag;
        }/*private bool DetermineIfVoidOrDelete(int a_mealIndex)*/


        /// <summary>
        /// This method decides whether or not to delete an entire meal, in the case of
        /// kids meals, or just delete an item.
        /// </summary>
        /// <remarks>
        /// NAME: DeleteItemOnTill
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_mealIndex">The position of the meal in the meal list.</param>
        /// <param name="a_itemIndex">The 'position' in the item in the meal: 0 = Entree, 1 = Side, 2 = Beverage.</param>
        private void DeleteItemOnTill(int a_mealIndex, int a_itemIndex)
        {
            bool determineIfKidsMeal = DetermineIfKidsMeal(a_mealIndex);

            if (determineIfKidsMeal)
            {
                DeleteMealAtIndex(a_mealIndex);
            }
            else
            {
                DeleteItemWithinMealAtIndex(a_mealIndex, a_itemIndex);
            }
        }/*private void DeleteItemOnTill(int a_mealIndex, int a_itemIndex)*/

        /// <summary>
        /// Handles the composing of the void command for an Entree item. Parses the selected
        /// Entree's view and sends it to the KitchenScreenClient.
        /// </summary>
        /// <remarks>
        /// NAME: SendVoidCommandForEntree
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/1/2019
        /// </remarks>
        /// <param name="a_mealIndex">The index of the meal of which exists the entree to void.</param>
        private void SendVoidCommandForEntree(int a_mealIndex)
        {
            Entree targetEntree = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex).GetEntree();
            EntreeView targetEntreeView = new EntreeView(targetEntree);

            targetEntreeView.DecomposeAttributesToString();
            List<string> attributesOfVoid = new List<string>();
            foreach (string s in targetEntreeView.GetTextMods())
            {
                if (!s.Equals("") && !s.Equals("\r") && !s.Equals("~"))
                {
                    attributesOfVoid.Add(s +  '\n');
                }
            }

            attributesOfVoid.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
            attributesOfVoid.Insert(1, m_customerChecks[m_currentlySelectedTab].GetCheckGUID() + '\n');
            attributesOfVoid.Insert(2, "VOID" + '\n');
            string toBeSent = string.Join(string.Empty, attributesOfVoid);
            m_server.SendToAll(toBeSent);
            UpdateDisplay();
        }/*private void SendVoidCommandForEntree(int a_mealIndex)*/

        /// <summary>
        /// Handles the composing of the void command for an Side item. Parses the selected
        /// Side's view and sends it to the KitchenScreenClient.
        /// </summary>
        /// <remarks>
        /// NAME: SendVoidCommandForSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/1/2019
        /// </remarks>
        /// <param name="a_mealIndex">The index of the meal of which exists the side to void.</param>
        private void SendVoidCommandForSide(int a_mealIndex)
        {
            Side targetSide = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex).GetSide();
            SideView targetSideView = new SideView(targetSide);

            List<string> attributesOfVoid = new List<string> {targetSideView.GetFormattedDetailsForDisplay()};

            attributesOfVoid.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
            attributesOfVoid.Insert(1, m_customerChecks[m_currentlySelectedTab].GetCheckGUID() + '\n');
            attributesOfVoid.Insert(2, "VOID" + '\n');
            string toBeSent = string.Join(string.Empty, attributesOfVoid);
            m_server.SendToAll(toBeSent);
            UpdateDisplay();
        }/*private void SendVoidCommandForSide(int a_mealIndex)*/

        /// <summary>
        /// Handles the composing of the void command for an Beverage item. Parses the selected
        /// Beverage's view and sends it to the KitchenScreenClient.
        /// </summary>
        /// <remarks>
        /// NAME: SendVoidCommandForBeverage
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/1/2019
        /// </remarks>
        /// <param name="a_mealIndex">The index of the meal of which exists the beverage to void.</param>
        private void SendVoidCommandForBeverage(int a_mealIndex)
        {
            Beverage targetBeverage = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex).GetBeverage();
            BeverageView targetSideView = new BeverageView(targetBeverage);

            List<string> attributesOfVoid = new List<string> {targetSideView.GetFormattedDetailsForDisplay()};

            attributesOfVoid.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
            attributesOfVoid.Insert(1, m_customerChecks[m_currentlySelectedTab].GetCheckGUID() + '\n');
            attributesOfVoid.Insert(2, "VOID" + '\n');
            string toBeSent = string.Join(string.Empty, attributesOfVoid);
            m_server.SendToAll(toBeSent);
            UpdateDisplay();
        }/*private void SendVoidCommandForBeverage(int a_mealIndex)*/

        /// <summary>
        /// Handles the composing of the void command for an KidsMeal item. Parses the selected
        /// KidsMeal's view and sends it to the KitchenScreenClient.
        /// </summary>
        /// <remarks>
        /// NAME: SendVoidCommandForKidsMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/1/2019
        /// </remarks>
        /// <param name="a_mealIndex">The index of the meal of which exists the KidsMeal to void.</param>
        private void SendVoidCommandForKidsMeal(int a_mealIndex)
        {
            Meal targetMeal = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_mealIndex);
            MealView targetMealView = new MealView(targetMeal);

            targetMealView.FillMealDisplay();
            List<string> attributesOfVoid = new List<string>();
            foreach (string s in targetMealView.GetDisplay())
            {
                if (!s.Equals("") && !s.Equals("\r") && !s.Equals("~"))
                {
                    attributesOfVoid.Add(s + '\n');
                }
            }

            attributesOfVoid.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
            attributesOfVoid.Insert(1, m_customerChecks[m_currentlySelectedTab].GetCheckGUID() + '\n');
            attributesOfVoid.Insert(2, "VOID" + '\n');
            string toBeSent = string.Join(string.Empty, attributesOfVoid);
            m_server.SendToAll(toBeSent);
            UpdateDisplay();
        }/*private void SendVoidCommandForKidsMeal(int a_mealIndex)*/

        /// <summary>
        /// Removes the Meal at the given index.
        /// </summary>
        /// <remarks>
        /// NAME: DeleteKidsMealAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_index">The index of the meal to be deleted</param>
        private void DeleteMealAtIndex(int a_index)
        {
            m_customerChecks[m_currentlySelectedTab].DeleteMealAtIndex(a_index);
        }/*private void DeleteKidsMealAtIndex(int a_index)*/

        /// <summary>
        /// A utility method that determines if a meal at an index is a KidsMeal.
        /// </summary>
        /// <remarks>
        /// NAME: DetermineIfKidsMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_targetMeal">The index of the target meal</param>
        /// <returns>
        /// If the target meal is a KidsMeal, return true. Otherwise, return false.
        /// </returns>
        /// 
        private bool DetermineIfKidsMeal(int a_targetMeal)
        {
            Meal toBeDetermined = m_customerChecks[m_currentlySelectedTab].GetMealAtIndex(a_targetMeal);
            return toBeDetermined.IsKidsMeal();
        }/*private bool DetermineIfKidsMeal(int a_targetMeal)*/

        /// <summary>
        /// Retrieves the relevant indices of the item selected in the view.
        /// </summary>
        /// <remarks>
        /// NAME: GetDisplaySelection
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <returns>
        /// A tuple that contains the index of the meal and the type of MenuItem (i.e Entree, Side, Beverage)
        /// </returns>
        private Tuple<int, int> GetDisplaySelection()
        {
            int indexOfListBoxSelection = lbCustomerCheck.SelectedIndex;
            m_customerChecks[m_currentlySelectedTab].CompileMealMatrix();
            int indexOfMeal = m_customerCheckView.GetIndexOfSelected(indexOfListBoxSelection);
            Tuple<int, int> mealTuple = m_customerChecks[m_currentlySelectedTab].FindMealViaMatrix(indexOfMeal);

            return mealTuple;
        }/*private Tuple<int, int> GetDisplaySelection()*/

        /// <summary>
        /// Deletes a target item from the customer's check. The tuple contains the position 
        /// of the meal in the customer's list, and also the type of item that needs to be deleted 
        /// (0 = Entree, 1 = Side, 2 = Beverage).
        /// </summary>
        /// <remarks>
        /// NAME: DeleteItemWithinMealAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="a_mealIndex">A tuple containing the target meal index and target item index</param>
        /// <param name="a_itemIndex"></param>
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
        }/*private void DeleteItemWithinMealAtIndex(int a_mealIndex, int a_itemIndex)*/

        /// <summary>
        /// This method is called whenever the customer's orders are drawn to the screen. Handles logic
        /// to give a customer's check visual clarity on which orders have been sent to the screens or not.
        /// </summary>
        /// <remarks>
        /// NAME: LbCustomerCheck_DrawItem
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The EventArgs object for formatting the view of the ListBox</param>
        private void LbCustomerCheck_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //For when the user clicks on the empty Listbox.
            if (e.Index < 0) { return; }

            string formatable = lbCustomerCheck.Items[e.Index].ToString();

            CustomerCheckView viewTextFormatting = new CustomerCheckView(m_customerChecks[m_currentlySelectedTab]);

            if (e.Index < viewTextFormatting.GetCountInDisplayOfSentItems())
            {
                e.Graphics.DrawString(formatable, new Font("Arial", 10, FontStyle.Italic), Brushes.Black, e.Bounds);
            }
            else
            {
                e.Graphics.DrawString(formatable, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            }

            e.DrawFocusRectangle();

        }/*private void LbCustomerCheck_DrawItem(object sender, DrawItemEventArgs e)*/

        /// <summary>
        /// This click event is fired when the user clicks the beverage selection form.
        /// This provides a more concise, organized way to ring up beverages, and includes 
        /// less common beverage choices.
        /// </summary>
        /// <remarks>
        /// NAME: BtnBeverages_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that fired the event.</param>
        /// <param name="e">The EventArgs object associated with the event.</param>
        private void BtnBeverages_Click(object sender, EventArgs e)
        {
            Beverage bevFromSelectionForm = new Beverage();
            BeverageSelectionForm bsf = new BeverageSelectionForm(bevFromSelectionForm);
            bsf.ShowDialog();

            m_customerChecks[m_currentlySelectedTab].CreateNewMealOrAddBeverageToMeal(bevFromSelectionForm);
            UpdateDisplay();
        }/*private void BtnBeverages_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is a visibility event for the main form. This is primarily used to 
        /// launch the Kitchen Screen application when the main register menu is loaded. This event 
        /// only ever fires when the MainPoSForm is loaded from the passcode form.
        /// </summary>
        /// <remarks>
        /// NAME: MainPoSForm_VisibleChanged
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The object that triggers the event.</param>
        /// <param name="e">The EventArgs associated with the event.</param>
        private void MainPoSForm_VisibleChanged(object sender, EventArgs e)
        {
            if (m_sisterApplicationID == 0)
            {
                LaunchSisterApplication();
            }
        }/*private void MainPoSForm_VisibleChanged(object sender, EventArgs e)*/

        /// <summary>
        /// The goal of this method is to have a system-independent way to get the
        /// path to the KitchenScreenClient.
        /// </summary>
        /// <remarks>
        /// NAME: ParseRelativePath
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <returns>
        /// Returns the full relative path of the machine that the program is being ran on.
        /// </returns>
        private static string ParseRelativePath()
        {
            string relativePath = null;
            try
            {
                string startPath = Application.StartupPath;
                int endIndex = startPath.IndexOf(@"\RJOPointOfSale\", StringComparison.Ordinal);
                string partialPath = startPath.Substring(0, endIndex);
                relativePath = partialPath + @"\RJOPointOfSale\KitchenScreenClient\bin\Debug\KitchenScreenClient.exe";
            }
            catch (ArgumentOutOfRangeException e)
            {

                MessageBox.Show(e.Message + "\n" + Application.StartupPath);
            }

            return relativePath;

        }/*private string ParseRelativePath()*/

        /// <summary>
        /// Uses the relative path from ParseRelativePath to run the KitchenScreensClient.
        /// </summary>
        /// <remarks>
        /// NAME: LaunchSisterApplication
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
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
        }/*private void LaunchSisterApplication()*/

        /// <summary>
        /// Fires when the user clicks the Exit button. Simulates exiting but actually Hide()s.
        /// This is to avoid the serialization of customer's orders.
        /// </summary>
        /// <remarks>
        /// NAME: BtnExit_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The EventArgs associated with the event.</param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Hide();
            //KillSisterProcess();
            //m_sisterApplicationID = 0;
            m_enterCodeForm.Show(this);
        }/*private void BtnExit_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This click event handles the firing of the customer's orders to the 
        /// KitchenScreenClient. Send all unsent items to the screens. 
        /// </summary>
        /// <remarks>
        /// NAME: BtnSendOrdersOnTill_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The EventArgs that are associated with the event.</param>
        private void BtnSendOrdersOnTill_Click(object sender, EventArgs e)
        {
            List<string> listOfItems = new List<string>();
            listOfItems = m_customerCheckView.GetUnsentItemsForSending();
            listOfItems.Insert(0, m_customerChecks[m_currentlySelectedTab].Name + '\n');
            
            if (listOfItems.Count <= 1)
            {
                MessageBox.Show(@"ERR: Empty Check. Please fill the till with items before sending.");
                return;
            }

            if (m_server.NoClients())
            {
                MessageBox.Show(@"ERR: No connection has been established with the server. " +
                                @"Click/Tap the 'Establish Connection with PoS' button on the KitchenScreenClient.");
                return;
            }

            listOfItems.Insert(1, m_customerChecks[m_currentlySelectedTab].GetCheckGUID() + '\n');
            FlagMealsOnTillAsSent();
            string toBeSent = string.Join(string.Empty, listOfItems);
            m_server.SendToAll(toBeSent);
            m_numOfSentLines = m_customerCheckView.GetCountInDisplayOfSentItems();
            UpdateDisplay();
        }/*private void BtnSendOrdersOnTill_Click(object sender, EventArgs e)*/
        
        /// <summary>
        /// Flag all the items on the current tab as sent.
        /// </summary>
        /// <remarks>
        /// NAME: FlagMealsOnTillAsSent
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/13/2019
        /// </remarks>
        private void FlagMealsOnTillAsSent()
        {
            CustomerCheck currentlySelected = m_customerChecks[m_currentlySelectedTab];
            for (int i = 0; i < currentlySelected.NumberOfMeals(); i++)
            {
                Meal mealOnTill = currentlySelected.GetMealAtIndex(i);
                mealOnTill.SentFlag = true;
            }
        }/*private void FlagMealsOnTillAsSent()*/
    }
}
