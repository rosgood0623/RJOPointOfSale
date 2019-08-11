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
    public partial class BeverageSelectionForm : Form
    {
        Beverage m_beverage = new Beverage();
        public BeverageSelectionForm()
        {
            InitializeComponent();
        }

        public BeverageSelectionForm(Beverage a_bev) : this()
        {
            m_beverage = a_bev;
        }

        public void BtnSelection_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;
            m_beverage.RetrieveBevFromDb((string)selection.Tag);
            Close();
        }
    }
}
