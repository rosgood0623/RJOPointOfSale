using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RJOPointOfSale
{
    public class Beverage : MenuItem
    {
        public string BeverageIdentifier { get; private set; }

        public override int CalculatePrice()
        {
            return 0;
        }

        public void RetrieveBevFromDb(string a_productName)
        {
            try
            {
                conn = new DatabaseConnection(Properties.Settings.Default.ElephantSQLConnection)
                {
                    Query = @"SELECT * FROM public.tbl_bevs WHERE tbl_bevs.name = :beverageName"
                };

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = conn.QueryBeverageRetrieval(a_productName);
                numOfRetrievedRows = data_set.Count;

                if (numOfRetrievedRows > 0)
                {
                    BeverageIdentifier = data_set[0].GetAtIndex(m_columnNameIndex);
                    Price = Convert.ToDecimal(data_set[0].GetAtIndex(m_columnPriceIndex));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
