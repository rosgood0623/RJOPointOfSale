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
        /// <summary>
        /// The default constructor. Simply initializes the components
        /// </summary>
        /// <remarks>
        /// NAME: BeverageSelectionForm
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public BeverageSelectionForm()
        {
            InitializeComponent();
        }/*public BeverageSelectionForm()*/

        /// <summary>
        /// A constructor for BeverageSelectionForm. Inherits from the default constructor and
        /// receives a beverage object. 
        /// </summary>
        /// <remarks>
        /// NAME: BeverageSelectionForm
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_bev"></param>
        public BeverageSelectionForm(Beverage a_bev) : this()
        {
            m_beverage = a_bev;
        }/*public BeverageSelectionForm(Beverage a_bev) : this()*/

        /// <summary>
        /// A generic method applied to all beverage buttons on the form.
        /// Once clicked, queries the database for the selected beverage 
        /// and closes the form.
        /// </summary>
        /// <remarks>
        /// NAME: BtnSelection_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        public void BtnSelection_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;
            m_beverage.RetrieveBevFromDb((string)selection.Tag);
            Close();
        }/*public void BtnSelection_Click(object sender, EventArgs e)*/
    }
}
