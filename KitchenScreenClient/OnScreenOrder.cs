using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KitchenScreenClient
{
    public class OnScreenOrder
    {
        private readonly List<string> m_order = new List<string>();
        private readonly List<string> m_elementsToBeVoided = new List<string>();
        private readonly List<string> m_voidedElements = new List<string>();

        private string m_orderGUIDNickName;
        private string m_orderGUID;
        private string m_rawData;

        private const int m_orderGUIDNickNamePosition = 0;
        private const int m_orderGUIDPosition = 1;
        private const int m_voidActionPosition = 2;

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
            m_rawData = a_sent;
            string[] parsed = a_sent.Split('\n');
            
            for (int i = 0; i < parsed.Length; i++)
            {
                if (i == m_orderGUIDNickNamePosition)
                {
                    m_orderGUIDNickName = parsed[i];
                    m_order.Add(parsed[i]);
                }
                else if (i == m_orderGUIDPosition)
                {
                    m_orderGUID = parsed[i];
                }
                else
                {
                    m_order.Add(parsed[i]);
                }
            }

            MakeListProper(m_order);
            SizeOfOrder = m_order.Count;
        }/*public void ParseSentStringIntoOrder(string a_sent)*/

        private void MakeListProper(List<string> a_enumerable)
        {
           
            for (int i = 0; i < a_enumerable.Count(); i++)
            {
                if (a_enumerable.ElementAt(i).Contains('\t'))
                {
                    a_enumerable[i] = a_enumerable[i].Replace("\t", string.Empty);
                    a_enumerable[i] = "    " + a_enumerable[i];
                }
            }

            
        }


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

        public string GetVoidedElementAtIndex(int a_index)
        {
            return m_voidedElements.ElementAt(a_index);
        }


        public bool CompareCheckGUID(string a_guid)
        {
            return m_orderGUID.Equals(a_guid);
        }

        public void AddVoidElements(string a_voidMessage)
        {
            string[] parsed = a_voidMessage.Split('\n');
            Array.Resize(ref parsed, parsed.Length-1);
            List<string> parsedList = new List<string>(parsed);
            MakeListProper(parsedList);

            for (int i = 2; i < parsedList.Count; i++)
            {
                if (!m_order.Contains(parsedList[i]))
                {
                    return;
                }

                if (!m_elementsToBeVoided.Contains(parsedList[i]))
                {
                    m_elementsToBeVoided.Add(parsedList[i]);
                }
            }

            VoidSpecifiedElements();
        }

        private void VoidSpecifiedElements()
        {
            foreach (string s in m_elementsToBeVoided)
            {
                if (m_order.Contains(s))
                {
                    m_order.Remove(s);
                    m_voidedElements.Add(s);
                }
            }

            m_elementsToBeVoided.Clear();
        }

        public int GetSizeOfRegularOrders()
        {
            return m_order.Count;
        }

        public int GetSizeOfVoidedOrders()
        {
            return m_voidedElements.Count;
        }

        public bool DetermineIfVoidBelongs(string[] a_parsedData)
        {
            Array.Resize(ref a_parsedData, a_parsedData.Length - 1);
            List<string> listedParsed = new List<string>(a_parsedData);
            listedParsed.RemoveAt(1);
            MakeListProper(listedParsed);

            foreach (string s in listedParsed)
            {
                if (!m_order.Contains(s))
                {
                    return false;
                }
            }

            return true;
        }
    }
}