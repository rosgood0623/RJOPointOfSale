using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    /// <summary>
    /// The abstract class that contains general members that
    /// have to do with menu items. 
    /// </summary>
    /// <remarks>
    /// NAME: MenuItem
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public abstract class MenuItem
    {
        protected DatabaseConnection m_conn;
        protected int m_numOfRetrievedRows;
        protected const int m_hasAttributeFlag = 1;
        protected const int m_columnNameIndex = 1;
        protected const int m_columnPriceIndex = 2;
        public decimal Price { get; protected set; }
        public abstract decimal CalculatePrice();
    }
}
