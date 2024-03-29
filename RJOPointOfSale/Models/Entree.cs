﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    /// <summary>
    /// The model for the entree menu items. 
    /// </summary>
    /// <remarks>
    /// NAME: Entree
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class Entree : MenuItem
    {
        private readonly int[] m_attributes = new int[MenuItemAttributes.NumOfAttributes];
        private readonly int[] m_originalAttributes = new int[MenuItemAttributes.NumOfAttributes];
        private readonly List<string> m_actionModifications = new List<string>();
        private PriceCalculator m_priceCalculator;

        private const string m_singleIdentifier = "Single";
        private const string m_doubleIdentifier = "Double";
        private const string m_grilledIdentifier = "Grilled";
        private const string m_crispyIdentifier = "Crispy";
        private const string m_blackBeanIdentifier = "Black Bean";


        public string EntreeIdentifier { get; private set; }

        public override decimal CalculatePrice()
        {
            m_priceCalculator = new PriceCalculator(this);
            decimal adjustedPrice = m_priceCalculator.Calculate();
            return adjustedPrice;
        }
        /// <summary>
        /// The setup for the DatabaseConnection object to retrieve the data from the database.
        /// </summary>
        /// <remarks>
        /// NAME: RetrieveAttributesFromDb
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_productName">The name of the entree which details will be retrieved from the database</param>
        public void RetrieveAttributesFromDb(string a_productName)
        {
            try
            {
                m_conn = new DatabaseConnection(Properties.Settings.Default.ElephantSQLConnection)
                {
                    Query = @"SELECT * FROM public.tbl_signatures_sandwich WHERE tbl_signatures_sandwich.name = :signatureName"
                };

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = m_conn.QuerySignatureRetrieval(a_productName);
                m_numOfRetrievedRows = data_set.Count;

                if (m_numOfRetrievedRows > 0)
                {
                    FillAttributes(data_set[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }/*public void RetrieveAttributesFromDb(string a_productName)*/

        public void RetrieveBasePriceFromDb(string a_productName)
        {
            try
            {
                m_conn = new DatabaseConnection(Properties.Settings.Default.ElephantSQLConnection)
                {
                    Query = @"SELECT * FROM public.tbl_signatures_prices WHERE tbl_signatures_prices.name = :signatureName"
                };

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = m_conn.QuerySignatureRetrieval(a_productName);
                m_numOfRetrievedRows = data_set.Count;

                if (m_numOfRetrievedRows > 0)
                {
                    Price = Convert.ToDecimal(data_set[0].GetAtIndex(m_columnPriceIndex));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }/*public void RetrieveBasePriceFromDb(string a_productName)*/

        /// <summary>
        /// This method uses the data retrieved from the database querying and sets the attributes 
        /// of the entree.
        /// </summary>
        /// <remarks>
        /// NAME: FillAttributes
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_postgresDataSet">The PostgresDataSet object that contains the attributes 
        /// retrieved from the database.</param>
        public void FillAttributes(PostgresDataSet a_postgresDataSet)
        {
            m_attributes[MenuItemAttributes.Initialized] = m_hasAttributeFlag;
            EntreeIdentifier = a_postgresDataSet.GetAtIndex(1);
            for (int i = MenuItemAttributes.SandwichNoBun; i <= MenuItemAttributes.SandwichBeefSingle; i++)
            {
                if (a_postgresDataSet.GetAtIndex(i).Equals("True"))
                {
                    m_attributes[i - 1] = m_hasAttributeFlag;
                    m_originalAttributes[i - 1] = m_hasAttributeFlag;
                }
            }

        } /* public void FillAttributes(PostgresDataSet a_postgresDataSet)*/

        /// <summary>
        /// An accessor for the current attributes for this entree item.
        /// </summary>
        /// <remarks>
        /// NAME: DecomposeAttributes
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// The current attributes of this entree item. 
        /// </returns>
        public int[] DecomposeAttributes()
        {
            return m_attributes;
        }/*public int[] DecomposeAttributes()*/

        /// <summary>
        /// DecomposeOriginalAttributes()
        /// An accessor for this entree's original attributes. 
        /// </summary>
        /// <remarks>
        /// NAME: DecomposeOriginalAttributes
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// The original attributes of the entree item. That is, the attributes of the item from
        /// the database, before any modifications were made. 
        /// </returns>
        public int[] DecomposeOriginalAttributes()
        {
            return m_originalAttributes;
        }/*public int[] DecomposeOriginalAttributes()*/

        /// <summary>
        /// Modifies the EntreeIdentifier property to reflect the protein choice of this entree.
        /// </summary>
        /// <remarks>
        /// NAME: SetProteinType
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_type">The protein type of the entree.</param>
        public void SetProteinType(string a_type)
        {
            switch (a_type)
            {
                case m_singleIdentifier:
                    m_attributes[MenuItemAttributes.SandwichBeefSingle] = m_hasAttributeFlag;
                    m_originalAttributes[MenuItemAttributes.SandwichBeefSingle] = m_hasAttributeFlag;
                    EntreeIdentifier += " Single";
                    break;
                case m_doubleIdentifier:
                    m_attributes[MenuItemAttributes.SandwichBeefDouble] = m_hasAttributeFlag;
                    m_originalAttributes[MenuItemAttributes.SandwichBeefDouble] = m_hasAttributeFlag;
                    EntreeIdentifier += " Double";
                    break;
                case m_grilledIdentifier:
                    m_attributes[MenuItemAttributes.SandwichGrilledChicken] = m_hasAttributeFlag;
                    m_originalAttributes[MenuItemAttributes.SandwichGrilledChicken] = m_hasAttributeFlag;
                    EntreeIdentifier += " Grilled";
                    break;
                case m_crispyIdentifier:
                    m_attributes[MenuItemAttributes.SandwichCrispyChicken] = m_hasAttributeFlag;
                    m_originalAttributes[MenuItemAttributes.SandwichCrispyChicken] = m_hasAttributeFlag;
                    EntreeIdentifier += " Crispy";
                    break;
                case m_blackBeanIdentifier:
                    m_attributes[MenuItemAttributes.SandwichBlackBean] = m_hasAttributeFlag;
                    m_originalAttributes[MenuItemAttributes.SandwichBlackBean] = m_hasAttributeFlag;
                    EntreeIdentifier += " BB";
                    break;
            }
        }/* public void SetProteinType(string a_type)*/

        /// <summary>
        /// With the desired index, get the original attribute stored there. 
        /// </summary>
        /// <remarks>
        /// NAME: GetOriginalAttrAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_index"></param>
        /// <returns>
        /// The value at the desired index as an int.
        /// </returns>
        public int GetOriginalAttrAtIndex(int a_index)
        {
            return m_originalAttributes[a_index];
        }/*public int GetOriginalAttrAtIndex(int a_index)*/

        /// <summary>
        /// Adds an action mod to this entree item. Duplicates aren't allowed, and if a duplicate is 
        /// passed this method removes that action mod from the entree.
        /// </summary>
        /// <remarks>
        /// NAME: AddActionMod
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_action">The desired action mod to be added to the entree.</param>
        public void AddActionMod(string a_action)
        {
            if (m_actionModifications.Contains(a_action))
            {
                m_actionModifications.Remove(a_action);
                return;
            }

            m_actionModifications.Add(a_action);
        }/*public void AddActionMod(string a_action)*/

        /// <summary>
        /// A simple method to check to see if there are any action mods related to this entree item.
        /// </summary>
        /// <remarks>
        /// NAME: IsEmptyActionMods
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// True if the action mod list is empty, false otherwise.
        /// </returns>
        public bool IsEmptyActionMods()
        {
            return m_actionModifications.Count == 0;
        }/*public bool IsEmptyActionMods()*/

        /// <summary>
        /// Gets the number of action mods currently within the action mod list.
        /// </summary>
        /// <remarks>
        /// NAME: ActionModsCount
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// The number of action mods in the list as an int.
        /// </returns>
        public int ActionModsCount()
        {
            return m_actionModifications.Count;
        }/*public int ActionModsCount()*/

        /// <summary>
        /// Gets the action mod at a given index.
        /// </summary>
        /// <remarks>
        /// NAME: GetActionModAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_index">The index which the desired data lies.</param>
        /// <returns>
        /// The action mod, returned as a string.
        /// </returns>
        public string GetActionModAtIndex(int a_index)
        {
            return m_actionModifications.ElementAt(a_index);
        }/*public string GetActionModAtIndex(int a_index)*/
    }
}
