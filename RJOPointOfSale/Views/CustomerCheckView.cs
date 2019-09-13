﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace RJOPointOfSale
{
    /// <summary>
    /// The view for the customer's order information. Determines how
    /// the customer's order appears on the register's main menu.
    /// </summary>
    /// <remarks>
    /// NAME: MenuItem
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class CustomerCheckView
    {
        private CustomerCheck m_customerCheck;
        private readonly List<MealView> m_customerMealViews = new List<MealView>();
        private readonly BindingList<string> m_menuItemsForDisplay = new BindingList<string>();
        private readonly List<int> m_savedIndexesForDisplay = new List<int>();
        private readonly List<decimal> m_pricingOfOrders = new List<decimal>();

        private const string m_tilde = "~";
        private const string m_lineFeed = "\r";
        private const string m_kidsMeal = "Kids";
        private const int m_indexOfFirstItem = 0;


        /// <summary>       
        /// A constructor for the CustomerCheckView. Sets 
        /// its customer check and enables the BindingList events.
        /// </summary>
        /// <remarks>
        /// NAME: CustomerCheckView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_customerOrders">The CustomerCheck that belongs to this CustomerCheckView.</param>
        public CustomerCheckView(CustomerCheck a_customerOrders)
        {
            m_customerCheck = a_customerOrders;
            m_menuItemsForDisplay.ListChanged += MenuItemsForDisplay_ListChanged;
        }/*public CustomerCheckView(CustomerCheck a_customerOrders)*/

        /// <summary>
        /// Clears and re-initializes all members for updating.
        /// </summary>
        /// <remarks>
        /// NAME: UpdateMembersForDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_customerCheck">The CustomerCheck that belongs to this CustomerCheckView</param>
        public void UpdateMembersForDisplay(CustomerCheck a_customerCheck)
        {
            ClearMembersForReinitialization();
            m_customerCheck = a_customerCheck;
            RefreshMealViews();
            RetrieveItemDetails();
            CompleteMealPricingForView();
        }/*public void UpdateMembersForDisplay(CustomerCheck a_customerCheck)*/

        /// <summary>
        /// Clears all the members that are vital for display to ensure there are no garbage or
        /// leftover values from updating.
        /// </summary>
        /// <remarks>
        /// NAME: ClearMembersForReinitialization
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        public void ClearMembersForReinitialization()
        {
            m_customerCheck = null;
            m_menuItemsForDisplay.Clear();
            m_customerMealViews.Clear();
            m_pricingOfOrders.Clear();
        }/*public void ClearMembersForReinitialization()*/

        /// <summary>
        /// Using the meals within the CustomerCheck, generate their respective
        /// MealViews and add them to the MealView list.
        /// </summary>
        /// <remarks>
        /// NAME: RefreshMealViews
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        public void RefreshMealViews()
        {
            for (int i = 0; i < m_customerCheck.NumberOfMeals(); i++)
            {
                Meal meal = new Meal();
                meal = m_customerCheck.GetMealAtIndex(i);

                MealView mealView = new MealView(meal);
                m_customerMealViews.Add(mealView);
            }
        }/*public void RefreshMealViews()*/

        /// <summary>
        /// Retrieves the item details from each meal view.
        /// </summary>
        /// <remarks>
        /// NAME: RetrieveItemDetails
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        public void RetrieveItemDetails()
        {
            m_savedIndexesForDisplay.Clear();

            foreach (MealView mv in m_customerMealViews)
            {
                PerformMealViewFormatting(mv);
            }
        }/*public void RetrieveItemDetails()*/

        /// <summary>
        /// Gets the prices for each meal and adds them to a member list.
        /// </summary>
        /// <remarks>
        /// NAME: CompleteMealPricingForView
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        private void CompleteMealPricingForView()
        {
            m_pricingOfOrders.AddRange(m_customerCheck.CompilePricingOfOrders());
        }/*private void CompleteMealPricingForView()*/

        /// <summary>
        /// Steps through a list containing all the indexes of the base
        /// view line to add the price to. 
        /// </summary>
        /// <remarks>
        /// NAME: CompleteMealPricingForView
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        public void ModifyLinesWithPrices()
        {
            List<int> priceIndexes = MakeSavedIndexesUsefulForPricing();
            
            for (int i = 0; i < priceIndexes.Count - 1; i++)
            {
                string target = m_menuItemsForDisplay[priceIndexes[i]];
                m_menuItemsForDisplay[priceIndexes[i]] = target.PadRight(40, ' ') + m_pricingOfOrders[i];

            }
        }/*public void ModifyLinesWithPrices()*/

        /// <summary>
        /// Uses the saved index list to make a new list that contains
        /// all the indexes of the base view line for each menu item rung up.
        /// </summary>
        /// <remarks>
        /// NAME: CompleteMealPricingForView
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        /// <returns>
        /// A list of indexes of the view to add the price tag to.
        /// </returns>
        private List<int> MakeSavedIndexesUsefulForPricing()
        {
            List<int> newIndexes = new List<int>
            {
                m_indexOfFirstItem
            };

            for (int i = 0; i < m_savedIndexesForDisplay.Count; i++)
            {
                int next = m_savedIndexesForDisplay[i];
                newIndexes.Add(newIndexes[i] + next);
            }

            return newIndexes;
        }/*private List<int> MakeSavedIndexesUsefulForPricing()*/

        /// <summary>
        /// Gets the raw display items from the MealView and parses out
        /// the vital information.
        /// </summary>
        /// <remarks>
        /// NAME: PerformMealViewFormatting
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_view">The MealView to parse data from.</param>
        private void PerformMealViewFormatting(MealView a_view)
        {
            a_view.FillMealDisplay();
            BindingList<string> viewDisplayDetails = a_view.GetDisplay();
            string[] indexArr = new string[viewDisplayDetails.Count];
            viewDisplayDetails.CopyTo(indexArr, 0);

            SaveNumberOfDisplayLinesOnEachAddition(indexArr);

            foreach (string val in viewDisplayDetails)
            {
                if (!val.Equals("") && !val.Equals(m_lineFeed) && !val.Equals(m_tilde))
                {
                    m_menuItemsForDisplay.Add(val);
                }
            }
        }/*private void PerformMealViewFormatting(MealView a_view)*/

        /// <summary>
        /// An accessor method for getting the full check's details for displaying.
        /// </summary>
        /// <remarks>
        /// NAME: GetItemsForDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <returns>
        /// The BindingList that contains the strings for display.
        /// </returns>
        public BindingList<string> GetItemsForDisplay()
        {
            return m_menuItemsForDisplay;
        }/*public BindingList<string> GetItemsForDisplay()*/

        /// <summary>
        /// Gets the full list of all the lines in the display that have not been
        /// sent to the KitchenScreensClient yet. Determines this primarily through
        /// the SentFlag property of the Meal object.
        /// </summary>
        /// <remarks>
        /// NAME: GetUnsentItemsForSending
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <returns>
        /// A list of all strings that have not been sent to the KitchenScreensClient yet.
        /// </returns>
        public List<string> GetUnsentItemsForSending()
        {
            List<string> stringSender = new List<string>();

            for (int i = 0; i < m_customerCheck.NumberOfMeals(); i++)
            {
                Meal potentialSender = m_customerCheck.GetMealAtIndex(i);
                if (!potentialSender.SentFlag)
                {
                    MealView tempMealView = new MealView(potentialSender);
                    tempMealView.FillMealDisplay();

                    foreach (string str in tempMealView.GetDisplay())
                    {
                        if (!str.Contains(m_tilde))
                        {
                            stringSender.Add(str + '\n');
                        }
                    }
                }
            }
            return stringSender;
        }/*public List<string> GetUnsentItemsForSending()*/

        /// <summary>
        /// Gets a count of the number of lines in the display 
        /// that have been sent to the KitchenScreenClient.
        /// </summary>
        /// <remarks>
        /// NAME: GetCountInDisplayOfSentItems
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <returns>
        /// An integer which is the number of sent lines to the KitchenScreenClient.
        /// </returns>
        public int GetCountInDisplayOfSentItems()
        {
            int numberOfSentLines = 0;

            for (int i = 0; i < m_customerCheck.NumberOfMeals(); i++)
            {
                Meal potentialSender = m_customerCheck.GetMealAtIndex(i);
                if (potentialSender.SentFlag)
                {
                    MealView tempMealView = new MealView(potentialSender);
                    tempMealView.FillMealDisplay();
                    numberOfSentLines += tempMealView.NumberOfDisplayItems;
                }
            }

            return numberOfSentLines;
        }/*public int GetCountInDisplayOfSentItems()*/


        /// <summary>
        /// Display the new index of the item added.
        /// Used mostly for debugging purposes.
        /// </summary>
        /// <remarks>
        /// NAME: MenuItemsForDisplay_ListChanged
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="sender">The object that triggered the event - the BindingList.</param>
        /// <param name="e">The ListChangedEventArgs associated with this event.</param>
        void MenuItemsForDisplay_ListChanged(object sender, ListChangedEventArgs e)
        {
            Debug.WriteLine("CustomerCheckView: " + e.NewIndex);
        }/*void MenuItemsForDisplay_ListChanged(object sender, ListChangedEventArgs e)*/

        /// <summary>
        /// This method is used to retrieve at what index in the display the user 
        /// selected the item. This is done this way because every item has a chance of 
        /// being a variable number of lines due to modification.
        /// </summary>
        /// <remarks>
        /// NAME: GetIndexOfSelected
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_value">The threshold value of selection.</param>
        /// <returns>
        /// The index of the selected item in the display.
        /// </returns>
        public int GetIndexOfSelected(int a_value)
        {
            //numOfLinesInDisplay starts at -1 because m_savedIndexesForDisplay doesn't start at 0
            //And the SelectedItem property of listboxes does. Setting this variable to -1 is the least ugly 
            //solution I could come up with to prevent this desync issue.
            int numOfLinesInDisplay = -1;
            int indexOfSelection = 0;

            for (int i = 0; i < m_savedIndexesForDisplay.Count; i++)
            {
                numOfLinesInDisplay += m_savedIndexesForDisplay.ElementAt(i);

                if (numOfLinesInDisplay >= a_value)
                {
                    indexOfSelection = i;
                    break;
                }
            }
            return indexOfSelection;
        }/*public int GetIndexOfSelected(int a_value)*/

        /// <summary>
        /// Saves the number of lines that will be shown in the display that represents
        /// that one item.
        /// </summary>
        /// <remarks>
        /// NAME: SaveNumberOfDisplayLinesOnEachAddition
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_splitsForDisplay">The unparsed strings for display for an item.</param>
        private void SaveNumberOfDisplayLinesOnEachAddition(string[] a_splitsForDisplay)
        {
            int numOfAttrs = 0;
            foreach (string split in a_splitsForDisplay)
            {
                if (split.Contains(m_kidsMeal))
                {
                    numOfAttrs += 3;
                    break;
                }

                if (!split.Equals("") && !split.Equals(m_lineFeed) && !split.Equals(m_tilde))
                {
                    numOfAttrs++;
                }
                else if (split.Equals(m_tilde) && numOfAttrs != 0)
                {
                    m_savedIndexesForDisplay.Add(numOfAttrs);
                    numOfAttrs = 0;
                }
            }

            if (numOfAttrs != 0) m_savedIndexesForDisplay.Add(numOfAttrs);
        }/*private void SaveNumberOfDisplayLinesOnEachAddition(string[] a_splitsForDisplay)*/
    }
}
