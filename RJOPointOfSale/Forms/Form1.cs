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

        /// <summary>
        /// The default constructor for Form1. Initializes the form's components.
        /// </summary>
        /// <remarks>
        /// NAME: Form1
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public Form1()
        {
            InitializeComponent();
        } /*public Form1()*/

        /// <summary>
        /// The Load event for Form1. This event is performed after the object is
        /// initialized. Establishes this form's DatabaseConnection object.
        /// </summary>
        /// <remarks>
        /// NAME: Form1_Load
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The object that triggered the event - Form1</param>
        /// <param name="e">The EventArgs associated with the event.</param>
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
        }/*private void Form1_Load(object sender, EventArgs e)*/
        /// <summary>
        /// A generic click event applied to all the number buttons in this form. Accesses the
        /// buttons text and applies it to the string that represents the employee's code.
        /// </summary>
        /// <remarks>
        /// NAME: BtnKeypad_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnKeypad_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            m_strCode += clicked.Text;
            lblCode.Text += '*';
        } /*rivate void BtnKeypad_Click(object sender, EventArgs e)*/
        /// <summary>
        /// Clears out the employee code string and the 'encrypted' label code that represents
        /// the employee code.
        /// </summary>
        /// <remarks>
        /// NAME: BtnClear_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered that event - the clear button</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            m_strCode = string.Empty;
            lblCode.Text = string.Empty;
        }/*private void BtnClear_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Occurs when the user hits the Enter button. The code that the employee
        /// entered will be queried against the database. If the code exists within
        /// the system, open the MainPoSForm. Else, display and error message.
        /// </summary>
        /// <remarks>
        /// NAME: BtnEnter_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggers this event - the Enter button.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
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

                if (m_maxRows > 0 && serializedForm == null)
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
        }/*private void BtnEnter_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Clear the relevent members of this form. Used primarily 
        /// to ensure no leftover data from previous form iterations 
        /// is being used.
        /// </summary>
        /// <remarks>
        /// NAME: ClearMembers
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        private void ClearMembers()
        {
            m_strCode = string.Empty;
            m_enteredCode = 0;
            lblCode.Text = string.Empty;
            lblError.Text = string.Empty;
        } /*private void ClearMembers()*/

        /// <summary>
        /// This event occurs when this Form's visibility changes. The Owner
        /// property is not null when this Form is being manipulated from another Form. 
        /// This allows the ability to 'save' the Owner Form, in this case MainPoSMenu.
        /// </summary>
        /// <remarks>
        /// NAME: Form1_VisibleChanged
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The object that triggered the event - Form1</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            Form1 temp = sender as Form1;

            if (temp.Owner != null)
            {
                serializedForm = (MainPoSForm)temp.Owner;
            }

            ClearMembers();
        } /*private void Form1_VisibleChanged(object sender, EventArgs e)*/
    }
}
