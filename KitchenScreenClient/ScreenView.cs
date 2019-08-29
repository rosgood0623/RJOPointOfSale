using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using RJOPointOfSale;
using FontStyle = System.Drawing.FontStyle;

namespace KitchenScreenClient
{
    class ScreenView
    {
        private FlowLayoutPanel m_screen;
        private List<OnScreenOrderControl> m_onScreen = new List<OnScreenOrderControl>();
        public int CursorPosition { get; set; }
        /// <summary>
        /// A constructor for the ScreenView object. Passes in the 'screen'
        /// which in this case is a FlowLayoutPanel.
        /// </summary>
        /// <remarks>
        /// NAME: ScreenView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_screen">The screen from the Screen object. </param>
        public ScreenView(FlowLayoutPanel a_screen)
        {
            m_screen = a_screen;
        }/*public ScreenView(FlowLayoutPanel a_screen)*/

        /// <summary>
        /// Adds each OnScreenOrder object to the Screen's View. 
        /// </summary>
        /// <remarks>
        /// NAME: AddElementToScreen
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_order">The OnScreenOrder object that contains 
        /// the parsed data that will be displayed on the view.</param>
        /// <param name="a_color">The Color of the order, indicating which order the
        /// cursor is currently on.</param>
        public void AddElementToScreenWithControls(OnScreenOrder a_order, Color a_color)
        {
            OnScreenOrderControl newOrderOnScreen = new OnScreenOrderControl{BackColor = a_color};

            newOrderOnScreen.PostRegularElementsToControl(a_order);
            newOrderOnScreen.PostVoidedElementsToControl(a_order);
            newOrderOnScreen.FinalizeOnScreenInfo();

            m_screen.Controls.Add(newOrderOnScreen);

        }/*public void AddElementToScreen(OnScreenOrder a_order, Color a_textBoxColor)*/

        /// <summary>
        /// Clears the screen member for screen invalidation.
        /// </summary>
        /// <remarks>
        /// NAME: ClearScreenForRefresh
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public void ClearScreenForRefresh()
        {
            m_screen.Controls.Clear();
        }/*public void ClearScreenForRefresh()*/

    }
}
