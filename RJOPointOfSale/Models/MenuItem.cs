using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public abstract class MenuItem
    {
        protected DatabaseConnection conn;
        protected int numOfRetrievedRows;
        protected const int hasAttributeFlag = 1;
        protected const int m_columnNameIndex = 1;
        protected const int m_columnPriceIndex = 2;
        public decimal Price { get; protected set; }
        public abstract decimal CalculatePrice();
    }
}
