using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenScreenClient
{
    class OnScreenOrder
    {
        private readonly List<string> m_order = new List<string>();

        public int SizeOfOrder { get; private set; }
        /// <summary>
        /// Parses the order data line by line.
        /// </summary>
        /// <remarks>
        /// NAME: ParseSentStringIntoOrder
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_sent">The raw, unparsed order data</param>
        public void ParseSentStringIntoOrder(string a_sent)
        {
            string[] parsed = a_sent.Split('\n');
            foreach (string s in parsed)
            {
                m_order.Add(s);
            }

            SizeOfOrder = m_order.Count;
        }/*public void ParseSentStringIntoOrder(string a_sent)*/

        /// <summary>
        /// Gets the order line at the specified index
        /// </summary>
        /// <remarks>
        /// NAME: GetElementAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_index">The specified index.</param>
        /// <returns>
        /// The string representation of the order's line at the given index.
        /// </returns>
        public string GetElementAtIndex(int a_index)
        {
            return m_order.ElementAt(a_index);
        }/*public string GetElementAtIndex(int a_index)*/
    }
}