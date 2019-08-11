using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;


namespace RJOPointOfSale
{
    public class CustomerCheckView
    {
        private CustomerCheck m_customerCheck;
        private readonly List<MealView> m_customerMealViews = new List<MealView>();
        private readonly BindingList<string> m_menuItemsForDisplay = new BindingList<string>();
        private readonly List<int> m_savedIndexesForDisplay = new List<int>();
       
        public CustomerCheckView(CustomerCheck a_customerOrders)
        {
            m_customerCheck = a_customerOrders;
            m_menuItemsForDisplay.ListChanged += MenuItemsForDisplay_ListChanged;
        }

        public void RefreshMealViews()
        {
            for (int i = 0; i < m_customerCheck.NumberOfMeals(); i++)
            {
                Meal meal = new Meal();
                meal = m_customerCheck.GetMealAtIndex(i);

                MealView mealView = new MealView(meal);
                m_customerMealViews.Add(mealView);
            }
        }

        public void UpdateMembersForDisplay(CustomerCheck a_customerCheck)
        {
            ClearMembersForReinitialization();
            m_customerCheck = a_customerCheck;
            RefreshMealViews();
            RetrieveItemDetails();
        }
        
        public void RetrieveItemDetails()
        {
            m_savedIndexesForDisplay.Clear();

            foreach (MealView mv in m_customerMealViews)
            {
                PerformMealViewFormatting(mv);            
            }
        }     

        public BindingList<string> GetItemsForDisplay()
        {
            return m_menuItemsForDisplay;
        }
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
                        if (!str.Contains("~"))
                        {
                            stringSender.Add(str + '\n');
                        }
                    }
                }
            }

            return stringSender;
        }

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
        }


        void MenuItemsForDisplay_ListChanged(object sender, ListChangedEventArgs e)
        {
            Debug.WriteLine("CustomerCheckView: " + e.NewIndex);
        }

        public void ClearMembersForReinitialization()
        {
            m_customerCheck = null;
            m_menuItemsForDisplay.Clear();
            m_customerMealViews.Clear();
        }

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
        }


        private void PerformMealViewFormatting(MealView a_view)
        {
            a_view.FillMealDisplay();
            BindingList<string> viewDisplayDetails = a_view.GetDisplay();
            string[] indexArr = new string[viewDisplayDetails.Count];
            viewDisplayDetails.CopyTo(indexArr, 0);

            SaveNumberOfDisplayLinesOnEachAddition(indexArr);

            foreach(string val in viewDisplayDetails)
            {
                if (!val.Equals("") && !val.Equals("\r") && !val.Equals("~"))
                {
                    m_menuItemsForDisplay.Add(val);
                }
            }
        }

        private void SaveNumberOfDisplayLinesOnEachAddition(string[] splitsForDisplay)
        {
            int numOfAttrs = 0;
            foreach (string split in splitsForDisplay)
            {
                if (!split.Equals("") && !split.Equals("\r") && !split.Equals("~"))
                {
                    numOfAttrs++;
                }
                else if (split.Equals("~") && numOfAttrs != 0)
                {
                    m_savedIndexesForDisplay.Add(numOfAttrs);
                    numOfAttrs = 0;
                }
            }

            if (numOfAttrs != 0) m_savedIndexesForDisplay.Add(numOfAttrs);
        }
    }
}
