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

        public PostgresDataSet()
        {
            dataSet = new List<string>();
        }

        public void AddData(string dataToAdd)
        {
            dataSet.Add(dataToAdd);
        }
        public int DataSetCount => dataSet.Count;
        public string GetAtIndex(int index)
        {
            return dataSet.ElementAt(index);
        }
    }
}
