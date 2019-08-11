using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class PriceCalculator
    {
        private MenuItem m_menuItem;

        public PriceCalculator(MenuItem item)
        {
            m_menuItem = item;
        }

        public decimal Price { get; private set; }

        public decimal Calculate()
        {
            //TODO: THIS
            return 0;
        }
    }
}
