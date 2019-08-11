using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    public class SideView
    {
        private readonly Side m_side;

        public SideView(Side a_side)
        {
            m_side = a_side;
        }

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
        }

        
    }
}
