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
        private List<string> m_order = new List<string>();

        public int SizeOfOrder { get; private set; }
        public void ParseSentStringIntoOrder(string a_sent)
        {
            string[] parsed = a_sent.Split('\n');
            foreach (string s in parsed)
            {
                   m_order.Add(s); 
            }

            SizeOfOrder = m_order.Count;
        }

        public string GetElementAtIndex(int a_index)
        {
            return m_order.ElementAt(a_index);
        }
    }
}
