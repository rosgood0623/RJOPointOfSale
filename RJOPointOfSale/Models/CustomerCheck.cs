using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RJOPointOfSale
{
    public class CustomerCheck
    {
        private readonly string m_checkID;
        private int m_checkPrice = 0;
        private readonly List<Meal> m_customerOrders = new List<Meal>();
        private readonly List<List<int>> m_mealMatrix = new List<List<int>>();
        private const int m_itemMissingInMeal = -1;

        public CustomerCheck()
        {
            var checkGUID = Guid.NewGuid();
            m_checkID = checkGUID.ToString();
            Name = m_checkID.Substring(0, 4);
        }
        public string Name { get; set; }

        public void AddMealToCheck(Meal a_meal)
        {
            m_customerOrders.Add(a_meal);
        }

        public int NumberOfMeals()
        {
            return m_customerOrders.Count;
        }

        public Meal GetMealAtIndex(int a_index)
        {
            return m_customerOrders.ElementAt(a_index);
        }

        public void DeleteMealAtIndex(int a_index)
        {
            m_customerOrders.RemoveAt(a_index);
        }

        public void ClearEmptyMeals()
        {
            for (int i = 0; i < m_customerOrders.Count; i++)
            {
                Meal target = GetMealAtIndex(i);
                if (!target.HasEntree() && !target.HasSide() && !target.HasBeverage())
                {
                    m_customerOrders.Remove(target);
                }
            }
        }

        public void CreateNewMealOrAddEntreeToMeal(Entree a_entree)
        {
            if (m_customerOrders.Count == 0)
            {
                Meal createNewMeal = new Meal(a_entree);
                m_customerOrders.Add(createNewMeal);
                return;
            }

            foreach (Meal meal in m_customerOrders)
            {
                if (!meal.HasEntree() && !meal.SentFlag)
                {
                    meal.AddEntreeToMeal(a_entree);
                    return;
                }
            }

            Meal newMeal = new Meal(a_entree);
            m_customerOrders.Add(newMeal);


        }

        public void CreateNewMealOrAddSideToMeal(Side a_side)
        {
            if (m_customerOrders.Count == 0)
            {
                Meal createNewMeal = new Meal(a_side);
                m_customerOrders.Add(createNewMeal);
                return;
            }

            foreach (Meal meal in m_customerOrders)
            {
                if (!meal.HasSide() && !meal.IsKidsMeal() && !meal.SentFlag)
                {
                    meal.AddSideToMeal(a_side);
                    return;
                }
            }

            Meal newMeal = new Meal(a_side);
            m_customerOrders.Add(newMeal);
        }

        public void CreateNewMealOrAddBeverageToMeal(Beverage a_bev)
        {
            if (m_customerOrders.Count == 0)
            {
                Meal createNewMeal = new Meal(a_bev);
                m_customerOrders.Add(createNewMeal);
                return;
            }

            foreach (Meal meal in m_customerOrders)
            {
                if (!meal.HasBeverage() && !meal.IsKidsMeal() && !meal.SentFlag)
                {
                    meal.AddBeverageToMeal(a_bev);
                    return;
                }
            }

            Meal newMeal = new Meal(a_bev);
            m_customerOrders.Add(newMeal);
        }

        public void CompileMealMatrix()
        {
            m_mealMatrix.Clear();
            int index = 0;
            foreach (Meal meal in m_customerOrders)
            {
                List<int> row = new List<int>
                {
                    meal.HasEntree() ? index++ : m_itemMissingInMeal,
                    meal.HasSide() ? index++ : m_itemMissingInMeal,
                    meal.HasBeverage() ? index++ : m_itemMissingInMeal
                };

                m_mealMatrix.Add(row);
            }
        }

        public Tuple<int, int> FindMealViaMatrix(int a_selectionIndex)
        {
            int targetMeal = 0;
            int itemType = 0;
            for (int i = 0; i < m_mealMatrix.Count; i++)
            {
                for (int j = 0; j < m_mealMatrix[i].Count; j++)
                {
                    if (m_mealMatrix[i][j] == a_selectionIndex)
                    {
                        targetMeal = i;
                        itemType = j;
                    }
                }
            }

            return new Tuple<int, int>(targetMeal, itemType);
        }
    }
}
