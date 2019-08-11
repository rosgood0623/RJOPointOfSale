using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class BeverageView
    {
        private readonly Beverage m_beverage;

        public BeverageView(Beverage a_beverage)
        {
            m_beverage = a_beverage;
        }

        public string GetFormattedDetailsForDisplay()
        {
            string formatted = "";
            string price = Convert.ToString(m_beverage.Price, CultureInfo.CurrentCulture).PadLeft(30);

            if (m_beverage.BeverageIdentifier != null)
            {
                string identifier = m_beverage.BeverageIdentifier;
                formatted = identifier;
                return formatted;
            }

            return formatted;
        }
    }
}
