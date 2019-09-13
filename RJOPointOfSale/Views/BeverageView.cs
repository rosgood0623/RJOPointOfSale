﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    /// <summary>
    /// The view for the beverage model. Facilitates how the
    /// beverage item will appear on the register menu. 
    /// </summary>
    /// <remarks>
    /// NAME: MenuItem
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class BeverageView
    {
        private readonly Beverage m_beverage;

        /// <summary>
        /// A constructor for the BeverageView object. Handles all view related behaviors for 
        /// the Beverage object. 
        /// </summary>
        /// <remarks>
        /// NAME: BeverageView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_beverage">The beverage that belongs to this BeverageView.</param>
        public BeverageView(Beverage a_beverage)
        {
            m_beverage = a_beverage;
        }/*public BeverageView(Beverage a_beverage)*/

        /// <summary>
        /// This method breaks down the attributes of the beverage and makes them suitable for
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
            
            if (m_beverage.BeverageIdentifier != null)
            {
                string identifier = m_beverage.BeverageIdentifier;
                formatted = identifier;
                return formatted;
            }

            return formatted;
        }/*public string GetFormattedDetailsForDisplay()*/
    }
}