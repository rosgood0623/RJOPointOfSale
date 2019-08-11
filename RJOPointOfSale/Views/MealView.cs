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

        public MealView(Meal a_meal)
        {
            m_customerMeal = a_meal;
        }

        public EntreeView GetEntreeForDisplay()
        {
            return m_customerMeal.HasEntree() ? new EntreeView(m_customerMeal.GetEntree()) : new EntreeView(new Entree());
        }

        public SideView GetSideForDisplay()
        {
            return m_customerMeal.HasSide() ? new SideView(m_customerMeal.GetSide()) : new SideView(new Side());
        }

        public BeverageView GetBeverageForDisplay()
        {
            return m_customerMeal.HasBeverage() ? new BeverageView(m_customerMeal.GetBeverage()) : new BeverageView(new Beverage());
        }

        public void FillMealDisplay()
        {
            PerformEntreeViewFormatting();
            PerformSideViewFormatting();
            PerformBeverageViewFormatting();
            SetNumberOfDisplayItems();
        }

        private void PerformEntreeViewFormatting()
        {
            EntreeView ev = GetEntreeForDisplay();
            ev.DecomposeAttributesToString();

            foreach (string item in ev.GetTextMods())
            {
                m_mealDisplay.Add(item ?? "");
            }
            m_mealDisplay.Add(m_lineBreakForParsing);
        }

        private void PerformSideViewFormatting()
        {
            SideView sv = GetSideForDisplay();
            if (!sv.GetFormattedDetailsForDisplay().Equals(""))
            {
                m_mealDisplay.Add(sv.GetFormattedDetailsForDisplay());
                m_mealDisplay.Add(m_lineBreakForParsing);
            }

        }

        private void PerformBeverageViewFormatting()
        {
            BeverageView bv = GetBeverageForDisplay();
            if (!bv.GetFormattedDetailsForDisplay().Equals(""))
            {
                m_mealDisplay.Add(bv.GetFormattedDetailsForDisplay());
                m_mealDisplay.Add(m_lineBreakForParsing);
            }
        }

        public BindingList<string> GetDisplay()
        {
            return m_mealDisplay;
        }

        private void SetNumberOfDisplayItems()
        {
            foreach (string str in m_mealDisplay)
            {
                if (!str.Contains("~") && !str.Equals(""))
                {
                    NumberOfDisplayItems++;
                }
            }
        }
    }
}
