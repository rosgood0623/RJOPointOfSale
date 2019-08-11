using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    public partial class Form1 : Form
    {               
        private DatabaseConnection m_conn;
        private string m_strCode;
        private int m_enteredCode;

        private List<PostgresDataSet> m_pgresDataSet;
        private int m_maxRows;

        private MainPoSForm serializedForm;
    

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //m_conn = new DatabaseConnection(Properties.Settings.Default.PostgresSQLConnection); 
                m_conn = new DatabaseConnection(Properties.Settings.Default.ElephantSQLConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnKeypad_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            m_strCode += clicked.Text;
            lblCode.Text += '*';
        }
   
        private void BtnClear_Click(object sender, EventArgs e)
        {
            m_strCode = string.Empty;
            lblCode.Text = string.Empty;
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            m_enteredCode = int.Parse(m_strCode);
            
            try
            {
                //m_conn.Query = @"SELECT * FROM public.tbl_employees WHERE ""employeeCode"" = :enteredCode";
                m_conn.Query = @"SELECT * FROM public.tbl_employees WHERE employeeCode = :enteredCode";
                m_conn.SetLogin = m_enteredCode;

                m_pgresDataSet = m_conn.QueryEmployeeIdentification();
                m_maxRows = m_pgresDataSet.Count;

                if(m_maxRows > 0 && serializedForm == null)
                {                  
                    var mainPoS = new MainPoSForm(m_enteredCode);
                    mainPoS.Show();
                    Hide();                  
                }
                else if (m_maxRows > 0 && serializedForm != null)
                {
                    MainPoSForm newMainPoSForm = new MainPoSForm();
                    newMainPoSForm = serializedForm;
                    newMainPoSForm.Show();
                    Hide();
                }
                else
                {
                    lblError.Text = @"Employee Code not found. Try again.";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);             
            }
        }

        private void ClearMembers()
        {
            m_strCode = string.Empty;
            m_enteredCode = 0;
            lblCode.Text = string.Empty;
            lblError.Text = string.Empty;
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            Form1 temp = sender as Form1;

            if (temp.Owner != null)
            {
                serializedForm = (MainPoSForm)temp.Owner;
            }

            ClearMembers();
        }
    }
}
