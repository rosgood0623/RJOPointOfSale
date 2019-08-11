using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    public partial class KeyboardForm : Form
    {
        private CustomerCheck checkToModify;
        public KeyboardForm()
        {
            InitializeComponent();
        }

        public KeyboardForm(CustomerCheck check) : this()
        {
            checkToModify = check;          
        }

        private void BtnKeyboard_Click(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            lbCheckID.Text += pressedButton.Text;
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            if (lbCheckID.Text.Trim().Length == 0)
            {
                lbIDErr.Text = "Please Enter a Valid Name";
                lbCheckID.Text = " ";
                return;
            }
            checkToModify.Name = lbCheckID.Text;
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lbCheckID.Text.Length == 0) { return; }
            string currentID = lbCheckID.Text;          
            lbCheckID.Text = currentID.Remove(lbCheckID.Text.Length-1);       
        }

        private void BtnSpace_Click(object sender, EventArgs e)
        {
            lbCheckID.Text += ' ';
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
