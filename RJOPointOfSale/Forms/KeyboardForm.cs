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
    /// <summary>
    /// Handles the ability for the cashier to add a unique
    /// identifier to the current check. A check will be given
    /// a default, randomized name if one isn't provided.
    /// </summary>
    /// <remarks>
    /// NAME: KeyboardForm
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks> 
    public partial class KeyboardForm : Form
    {
        private readonly CustomerCheck m_checkToModify;

        /// <summary>
        /// The default constructor for the KeyboardForm. Initializes the components.
        /// </summary>
        /// <remarks>
        /// NAME: KeyboardForm
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public KeyboardForm()
        {
            InitializeComponent();
        } /*public KeyboardForm()*/

        /// <summary>
        /// A constructor for the KeyboardForm. Inherits from
        /// the default constructor and member CustomerCheck 
        /// to the passed CustomerCheck.
        /// </summary>
        /// <remarks>
        /// NAME: KeyboardForm
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_check">The CustomerCheck in which is getting advanced identification.</param>
        public KeyboardForm(CustomerCheck a_check) : this()
        {
            m_checkToModify = a_check;
        } /*public KeyboardForm(CustomerCheck check) : this()*/

        /// <summary>
        /// A generic click event for all letter buttons on the form.
        /// When clicked, adds the buttons text to the checkID. 
        /// </summary>
        /// <remarks>
        /// NAME: BtnKeyboard_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button which triggered this event.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnKeyboard_Click(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            lbCheckID.Text += pressedButton.Text;
        }/*private void BtnKeyboard_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Sets the entered name as the new name for the check with input validation.
        /// </summary>
        /// <remarks>
        /// NAME: BtnEnter_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the Enter Button.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnEnter_Click(object sender, EventArgs e)
        {
            if (lbCheckID.Text.Trim().Length == 0)
            {
                lbIDErr.Text = @"Please Enter a Valid Name";
                lbCheckID.Text = @" ";
                return;
            }
            m_checkToModify.Name = lbCheckID.Text;
            Close();
        }/*private void BtnEnter_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is raised when the user wishes to delete their input.
        /// Deletes one character at a time.
        /// </summary>
        /// <remarks>
        /// NAME: BtnDelete_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this click event - the Delete button.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lbCheckID.Text.Length == 0) { return; }
            string currentId = lbCheckID.Text;
            lbCheckID.Text = currentId.Remove(lbCheckID.Text.Length - 1);
        }/*private void BtnDelete_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Adds a space to the users input.
        /// </summary>
        /// <remarks>
        /// NAME: BtnSpace_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the space bar.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnSpace_Click(object sender, EventArgs e)
        {
            lbCheckID.Text += ' ';
        }/*private void BtnSpace_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event closes the form when it fires. 
        /// </summary>
        /// <remarks>
        /// NAME: BtnClose_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the Close Without ID button. </param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }/*private void BtnClose_Click(object sender, EventArgs e)*/
    }
}
