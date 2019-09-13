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
    /// <summary>
    /// The model for the customer order information. Handles which pieces of data
    /// are considered void or standard elements.
    /// </summary>
    /// <remarks>
    /// NAME: OnScreenOrder
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks> 
    public class OnScreenOrder
    {
        private readonly List<string> m_activeOrder = new List<string>();
        private readonly List<string> m_elementsToBeVoided = new List<string>();
        private readonly List<string> m_voidedElements = new List<string>();
        private string m_orderGUID;

        private const int m_orderGUIDNickNamePosition = 0;
        private const int m_orderGUIDPosition = 1;

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
            
            for (int i = 0; i < parsed.Length; i++)
            {
                switch (i)
                {
                    case m_orderGUIDNickNamePosition:
                        m_activeOrder.Add(parsed[i]);
                        break;
                    case m_orderGUIDPosition:
                        m_orderGUID = parsed[i];
                        break;
                    default:
                        m_activeOrder.Add(parsed[i]);
                        break;
                }
            }

            MakeListProper(m_activeOrder);
            SizeOfOrder = m_activeOrder.Count;
        }/*public void ParseSentStringIntoOrder(string a_sent)*/

        /// <summary>
        /// Takes a list that handles order view data and parses it so
        /// there are no inconsistencies between lists when comparing.
        /// </summary>
        /// <remarks>
        /// NAME: MakeListProper
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019 
        /// </remarks>
        /// <param name="a_enumerable">The list to make proper.</param>
        private static void MakeListProper(List<string> a_enumerable)
        {
            for (int i = 0; i < a_enumerable.Count(); i++)
            {
                if (a_enumerable.ElementAt(i).Contains('\t'))
                {
                    a_enumerable[i] = a_enumerable[i].Replace("\t", string.Empty);
                    a_enumerable[i] = "    " + a_enumerable[i];
                }
            }
        } /*private static void MakeListProper(List<string> a_enumerable)*/

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
            return m_activeOrder.ElementAt(a_index);
        }/*public string GetElementAtIndex(int a_index)*/

        /// <summary>
        /// Get an voided element at the specified index.
        /// </summary>
        /// <remarks>
        /// NAME: GetVoidedElementAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <param name="a_index">The specified index.</param>
        /// <returns>
        /// The string representation of the voided element for the view.
        /// </returns>
        public string GetVoidedElementAtIndex(int a_index)
        {
            return m_voidedElements.ElementAt(a_index);
        }/*public string GetVoidedElementAtIndex(int a_index)*/

        /// <summary>
        /// Compared the GUID of the passed GUID and the OnScreenOrder's GUID
        /// </summary>
        /// <remarks>
        /// NAME: CompareCheckGUID
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <param name="a_guid">The passed string GUID</param>
        /// <returns>
        /// Returns true if the GUIDs are equal, false otherwise.
        /// </returns>
        public bool CompareCheckGUID(string a_guid)
        {
            return m_orderGUID.Equals(a_guid);
        }/*public bool CompareCheckGUID(string a_guid)*/

        /// <summary>
        /// Adds the parsed void elements to this OnScreenOrder object. 
        /// </summary>
        /// <remarks>
        /// NAME: AddVoidElements
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <param name="a_voidMessage">The void message. Contains all data that will be voided.</param>
        public void AddVoidElements(string a_voidMessage)
        {
            string[] parsed = a_voidMessage.Split('\n');
            Array.Resize(ref parsed, parsed.Length-1);
            List<string> parsedList = new List<string>(parsed);
            MakeListProper(parsedList);

            for (int i = 2; i < parsedList.Count; i++)
            {
                if (!m_activeOrder.Contains(parsedList[i]))
                {
                    return;
                }

                if (!m_elementsToBeVoided.Contains(parsedList[i]))
                {
                    m_elementsToBeVoided.Add(parsedList[i]);
                }
            }

            VoidSpecifiedElements();
        }/*public void AddVoidElements(string a_voidMessage)*/

        /// <summary>
        /// If the void element is contained within the active order list,
        /// move it to the void list.
        /// </summary>
        /// <remarks>
        /// NAME: VoidSpecifiedElements
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        private void VoidSpecifiedElements()
        {
            foreach (string s in m_elementsToBeVoided)
            {
                if (m_activeOrder.Contains(s))
                {
                    m_activeOrder.Remove(s);
                    m_voidedElements.Add(s);
                }
            }

            m_elementsToBeVoided.Clear();
        }/*private void VoidSpecifiedElements()*/

        /// <summary>
        /// Retrieves the size of the active orders list
        /// </summary>
        /// <remarks>
        /// NAME: GetSizeOfRegularOrders
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <returns>
        /// An integer that represents the size of the active order list.
        /// </returns>
        public int GetSizeOfRegularOrders()
        {
            return m_activeOrder.Count;
        }/*public int GetSizeOfRegularOrders()*/

        /// <summary>
        /// Retrieves the size of the voided orders list.
        /// </summary>
        /// <remarks>
        /// NAME: GetSizeOfVoidedOrders
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <returns>
        /// An integer that represents the size of the active order list.
        /// </returns>
        public int GetSizeOfVoidedOrders()
        {
            return m_voidedElements.Count;
        }/*public int GetSizeOfVoidedOrders()*/

        /// <summary>
        /// Since the entire list of OnScreenOrders is iterated on, we first have to check if
        /// the current void even belongs to this OnScreenOrder. This method handles this issue.
        /// </summary>
        /// <remarks>
        /// NAME: DetermineIfVoidBelongs
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/30/2019
        /// </remarks>
        /// <param name="a_parsedData">The parsed data of the voids.</param>
        /// <returns>
        /// Returns true if the void belongs, false otherwise.
        /// </returns>
        public bool DetermineIfVoidBelongs(string[] a_parsedData)
        {
            Array.Resize(ref a_parsedData, a_parsedData.Length - 1);
            List<string> listedParsed = new List<string>(a_parsedData);
            listedParsed.RemoveAt(1);
            MakeListProper(listedParsed);

            foreach (string s in listedParsed)
            {
                if (!m_activeOrder.Contains(s))
                {
                    m_elementsToBeVoided.Clear();
                    return false;
                }
            }
            return true;
        }/*public bool DetermineIfVoidBelongs(string[] a_parsedData)*/
    }
}