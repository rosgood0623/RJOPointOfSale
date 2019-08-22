using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class PostgresDataSet
    {
        private List<string> dataSet;

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
            dataSet = new List<string>();
        }/*public PostgresDataSet()*/

        /// <summary>
        /// Adds the data from the database to the PostgresDataset.
        /// </summary>
        /// <remarks>
        /// NAME: AddData
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="dataToAdd"> The data to be added.</param>
        public void AddData(string dataToAdd)
        {
            dataSet.Add(dataToAdd);
        }/*public void AddData(string dataToAdd)*/

        /// <summary>
        /// Get the data at a given index.
        /// </summary>
        /// <remarks>
        /// NAME: GetAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="index">The provided index</param>
        /// <returns>The data at the given index.</returns>
        public string GetAtIndex(int index)
        {
            return dataSet.ElementAt(index);
        }/*public string GetAtIndex(int index)*/
    }
}