﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    public class Side : MenuItem
    {
        public string SideIdentifier { get; private set; }

        public override int CalculatePrice()
        {
            return 0;
        }

        /// <summary>
        /// Queries the database for the name of the beverage from the MainPoSForm
        /// </summary>
        /// <remarks>
        /// NAME: RetrieveSideFromDb
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_productName">The desired side name to query the database with.</param>
        public void RetrieveSideFromDb(string a_productName)
        {
            try
            {
                conn = new DatabaseConnection(Properties.Settings.Default.ElephantSQLConnection)
                {
                    Query = @"SELECT * FROM public.tbl_sides WHERE tbl_sides.name = :sideName"
                };

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = conn.QuerySideRetrieval(a_productName);
                numOfRetrievedRows = data_set.Count;

                if (numOfRetrievedRows > 0)
                {
                    SideIdentifier = data_set[0].GetAtIndex(m_columnNameIndex);
                    Price = Convert.ToDecimal(data_set[0].GetAtIndex(m_columnPriceIndex));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }/*public void RetrieveSideFromDb(string a_productName)*/
        }

    }
}