using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    /// <summary>
    /// A helper class for database querying. Stores the data from the
    /// database in this object. 
    /// </summary>
    /// <remarks>
    /// NAME: MenuItem
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class PostgresDataSet
    {
        private readonly List<string> m_dataSet;

        /// <summary>
        /// The default constructor for the PostgresDataset
        /// </summary>
        /// <remarks>
        /// NAME: PostgresDataSet
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public PostgresDataSet()
        {
            m_dataSet = new List<string>();
        }/*public PostgresDataSet()*/

        /// <summary>
        /// Adds the data from the database to the PostgresDataset.
        /// </summary>
        /// <remarks>
        /// NAME: AddData
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_dataToAdd"> The data to be added.</param>
        public void AddData(string a_dataToAdd)
        {
            m_dataSet.Add(a_dataToAdd);
        }/*public void AddData(string dataToAdd)*/

        /// <summary>
        /// Get the data at a given index.
        /// </summary>
        /// <remarks>
        /// NAME: GetAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_index">The provided index</param>
        /// <returns>The data at the given index.</returns>
        public string GetAtIndex(int a_index)
        {
            return m_dataSet.ElementAt(a_index);
        }/*public string GetAtIndex(int index)*/
    }
}