using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class MealView
    {
        private readonly Meal m_customerMeal;
        private readonly BindingList<string> m_mealDisplay = new BindingList<string>();
        private const string m_lineBreakForParsing = "~";
        public int NumberOfDisplayItems { get; private set; }

        /// <summary>
        /// A constructor for the MealView object. Passes a Meal which this object is passed around.
        /// </summary>
        /// <remarks>
        /// NAME: MealView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_meal">The meal with which this MealView is based on.</param>
        public MealView(Meal a_meal)
        {
            m_customerMeal = a_meal;
        }/*public MealView(Meal a_meal)*/

        /// <summary>
        /// Firstly, checks to see if the meal passed into this MealView has an Entree. Depending on 
        /// this, perform an action.
        /// </summary>
        /// <remarks>
        /// NAME: GetEntreeForDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// If the Meal has an Entree, return a new EntreeView with the entree passed into it. Else,
        /// return a new, empty EntreeView.
        /// </returns>
        public EntreeView GetEntreeForDisplay()
        {
            return m_customerMeal.HasEntree() ? new EntreeView(m_customerMeal.GetEntree()) : new EntreeView(new Entree());
        }/*public EntreeView GetEntreeForDisplay()*/

        /// <summary>
        /// Firstly, checks to see if the meal passed into this MealView has a Side. Depending on
        /// this, perform an action.
        /// </summary>
        /// <remarks>
        /// NAME: GetSideForDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// If the Meal has a Side, return a new SideView with the meal's Side passed into it. Else,
        /// return a new, empty SideView.
        /// </returns>
        public SideView GetSideForDisplay()
        {
            return m_customerMeal.HasSide() ? new SideView(m_customerMeal.GetSide()) : new SideView(new Side());
        }/*public SideView GetSideForDisplay()*/

        /// <summary>
        /// Firstly, check to see if the meal passed into this MealView has a Beverage. Depending
        /// on this, perform an action.
        /// </summary>
        /// <remarks>
        /// NAME: GetBeverageForDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// If the Meal has a Beverage, return a new BeverageView with the meal's Beverage passed into it. Else,
        /// return a new, empty BeverageView.
        /// </returns>
        public BeverageView GetBeverageForDisplay()
        {
            return m_customerMeal.HasBeverage() ? new BeverageView(m_customerMeal.GetBeverage()) : new BeverageView(new Beverage());
        }/*public BeverageView GetBeverageForDisplay()*/

        /// <summary>
        /// The main method that performs the MealView display formatting.
        /// </summary>
        /// <remarks>
        /// NAME: FillMealDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public void FillMealDisplay()
        {
            PerformEntreeViewFormatting();
            PerformSideViewFormatting();
            PerformBeverageViewFormatting();
            SetNumberOfDisplayItems();
        }/*public void FillMealDisplay()*/

        /// <summary>
        /// Parses the EntreeView into suitable view strings. Uses null-coalescing operator to parse
        /// out potential exceptions. Adds the line break for parsing after the entire entree with any
        /// modifiers is added.
        /// </summary>
        /// <remarks>
        /// NAME: PerformEntreeViewFormatting
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void PerformEntreeViewFormatting()
        {
            EntreeView ev = GetEntreeForDisplay();
            ev.DecomposeAttributesToString();

            foreach (string item in ev.GetTextMods())
            {
                m_mealDisplay.Add(item ?? "");
            }
            m_mealDisplay.Add(m_lineBreakForParsing);
        }/*private void PerformEntreeViewFormatting()*/

        /// <summary>
        /// Parses the SideView into strings suitable for display. If the Meal doesn't have a Side,
        /// an empty SideView would be returned from the GetSideForDisplay() call, which would
        /// subsequently failed to enter the if statement. 
        /// </summary>
        /// <remarks>
        /// NAME: PerformSideViewFormatting
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void PerformSideViewFormatting()
        {
            SideView sv = GetSideForDisplay();
            if (!sv.GetFormattedDetailsForDisplay().Equals(""))
            {
                m_mealDisplay.Add(sv.GetFormattedDetailsForDisplay());
                m_mealDisplay.Add(m_lineBreakForParsing);
            }
        }/*private void PerformSideViewFormatting()*/

        /// <summary>
        /// Parses the BeverageView into strings suitable for display. If the Meal doesn't have a Beverage,
        /// an empty Beverage would be returned from the GetBeverageForDisplay() call, which would
        /// subsequently failed to enter the if statement. 
        /// </summary>
        /// <remarks>
        /// NAME: PerformBeverageViewFormatting
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void PerformBeverageViewFormatting()
        {
            BeverageView bv = GetBeverageForDisplay();
            if (!bv.GetFormattedDetailsForDisplay().Equals(""))
            {
                m_mealDisplay.Add(bv.GetFormattedDetailsForDisplay());
                m_mealDisplay.Add(m_lineBreakForParsing);
            }
        }/*private void PerformBeverageViewFormatting()*/

        /// <summary>
        /// An accessor method for the items within the view BindingList. 
        /// </summary>
        /// <remarks>
        /// NAME: GetDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// A BindingList, which contains the parsed strings suitable for display.
        /// </returns>
        public BindingList<string> GetDisplay()
        {
            return m_mealDisplay;
        }/*public BindingList<string> GetDisplay()*/

        /// <summary>
        /// This method counts the number of items that will be displayed in the view.
        /// Primarily used for determining exactly how many elements of the display have been 
        /// sent to the kitchen screens, and thusly which elements of the display to italicize. 
        /// </summary>
        /// <remarks>
        /// NAME: SetNumberOfDisplayItems
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void SetNumberOfDisplayItems()
        {
            foreach (string str in m_mealDisplay)
            {
                if (!str.Contains("~") && !str.Equals(""))
                {
                    NumberOfDisplayItems++;
                }
            }
        }/*private void SetNumberOfDisplayItems()*/
    }
}
