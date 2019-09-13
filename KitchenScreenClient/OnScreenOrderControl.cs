using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitchenScreenClient
{
    /// <summary>
    /// The view for the OnScreenOrder's model. Formats the information from
    /// the OnScreenOrder to reflect correctness.
    /// </summary>
    /// <remarks>
    /// NAME: OnScreenOrderControl
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks> 
    public class OnScreenOrderControl : FlowLayoutPanel
    {
        private readonly List<Label> m_activeLabels;
        private readonly List<Label> m_voidedLabels;

        /// <summary>
        /// A constructor for the OnScreenOrderControl. Initializes the properties of the
        /// FlowLayoutPanel which this control inherits from.
        /// </summary>
        /// <remarks>
        /// NAME: OnScreenOrderControl
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        public OnScreenOrderControl()
        {
            BorderStyle = BorderStyle.Fixed3D;
            FlowDirection = FlowDirection.TopDown;
            AutoSize = true;
            Padding = new Padding(0);
            m_activeLabels = new List<Label>();
            m_voidedLabels = new List<Label>();
        }/*public OnScreenOrderControl()*/

        /// <summary>
        /// Accesses the elements of the active order's list and posts them to active orders list in this control.
        /// </summary>
        /// <remarks>
        /// NAME: PostRegularElementsToControl
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <param name="a_screenOrder">The OnScreenOrder object from which to post from.</param>
        public void PostRegularElementsToControl(OnScreenOrder a_screenOrder)
        {
            
            for (int i = 0; i < a_screenOrder.GetSizeOfRegularOrders(); i++)
            {
                Label onScreenElement = new Label();

                string element = a_screenOrder.GetElementAtIndex(i);
                if (element.Contains('\t'))
                {
                    element = element.Replace("\t", string.Empty);
                    onScreenElement.Text = "    " + element;
                }
                else
                {
                    onScreenElement.Text = element;
                }

                m_activeLabels.Add(onScreenElement);
            }
        }/*public void PostRegularElementsToControl(OnScreenOrder a_screenOrder)*/

        /// <summary>
        /// Accesses the elements of the voided orders list and posts them to voided list in this control.
        /// </summary>
        /// <remarks>
        /// NAME: PostVoidedElementsToControl
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <param name="a_screenOrder">The OnScreenOrder object from which to void from.</param>
        public void PostVoidedElementsToControl(OnScreenOrder a_screenOrder)
        {
            for (int i = 0; i < a_screenOrder.GetSizeOfVoidedOrders(); i++)
            {
                Label onScreenElement = new Label();
                string element = a_screenOrder.GetVoidedElementAtIndex(i);
                if (element.Contains('\t'))
                {
                    element = element.Replace("\t", string.Empty);
                    onScreenElement.Text = "    " + element;
                }
                else
                {
                    onScreenElement.Text = element;
                }
                onScreenElement.Font = new Font(onScreenElement.Font, FontStyle.Strikeout);
                m_voidedLabels.Add(onScreenElement);
            }
        }/*public void PostVoidedElementsToControl(OnScreenOrder a_screenOrder)*/

        /// <summary>
        /// Gets the labels from each of the void and active lines and posts them to the control.
        /// </summary>
        /// <remarks>
        /// NAME: FinalizeOnScreenInfo
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        public void FinalizeOnScreenInfo()
        {
            foreach (Label l in m_voidedLabels)
            {
                l.AutoSize = true;
                Controls.Add(l);
            }

            foreach (Label l in m_activeLabels)
            {
                l.AutoSize = true;
                Controls.Add(l);
            }
        }/*public void FinalizeOnScreenInfo()*/
    }
}
