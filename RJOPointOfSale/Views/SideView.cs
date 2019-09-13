using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    /// <summary>
    /// The view for the side model. Parses how the models information
    /// will be displayed on the main menu.
    /// </summary>
    /// <remarks>
    /// NAME: MenuItem
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class SideView
    {
        private readonly Side m_side;

        /// <summary>
        /// A constructor for the SideView object. The passed side is what this object is based off of.
        /// </summary>
        /// <remarks>
        /// NAME: SideView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_side">The side which this object is based off of.</param>
        public SideView(Side a_side)
        {
            m_side = a_side;
        }/*public SideView(Side a_side)*/

        /// <summary>
        /// This method breaks down the attributes of the side and makes them suitable for
        /// display.
        /// </summary>
        /// <remarks>
        /// NAME: GetFormattedDetailsForDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// The formatted string ready for the view.
        /// </returns>
        public string GetFormattedDetailsForDisplay()
        {
            string formatted = "";
            string price = Convert.ToString(m_side.Price, CultureInfo.CurrentCulture).PadLeft(30);

            if (m_side.SideIdentifier != null)
            {
                string identifier = m_side.SideIdentifier;
                formatted = identifier;
                return formatted;
            }

            return formatted;
        }/*public string GetFormattedDetailsForDisplay()*/


    }
}