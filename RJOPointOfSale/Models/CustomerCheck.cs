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

        /// <summary>
        /// The default constructor for the CustomerCheck object. Generates a unique
        /// GUID for itself.
        /// </summary>
        /// <remarks>
        /// NAME: CustomerCheck
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        public CustomerCheck()
        {
            var checkGUID = Guid.NewGuid();
            m_checkID = checkGUID.ToString();
            Name = m_checkID.Substring(0, 4);
        }/*public CustomerCheck()*/
        public string Name { get; set; }

        /// <summary>
        /// Adds a meal to the list that represents the customer's orders
        /// </summary>
        /// <remarks>
        /// NAME: AddMealToCheck
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_meal">The meal to be added to the customer checks</param>
        public void AddMealToCheck(Meal a_meal)
        {
            m_customerOrders.Add(a_meal);
        }/*public void AddMealToCheck(Meal a_meal)*/

        /// <summary>
        /// Simply gets the number of items in the meal list.
        /// </summary>
        /// <remarks>
        /// NAME: NumberOfMeals
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// Integer - number of meals in the meal list.
        /// </returns>
        public int NumberOfMeals()
        {
            return m_customerOrders.Count;
        }/*public int NumberOfMeals()*/

        /// <summary>
        /// Simply retrieves the meal at a given index.
        /// </summary>
        /// <remarks>
        /// NAME: GetMealAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_index">The index of the desired meal</param>
        /// <returns>
        /// The meal at the passed index
        /// </returns>
        public Meal GetMealAtIndex(int a_index)
        {
            return m_customerOrders.ElementAt(a_index);
        }/*public Meal GetMealAtIndex(int a_index)*/

        /// <summary>
        /// Deletes a meal at an index
        /// </summary>
        /// <remarks>
        /// NAME: DeleteMealAtIndex
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_index">The index of the desired meal</param>
        public void DeleteMealAtIndex(int a_index)
        {
            m_customerOrders.RemoveAt(a_index);
        }/*public void DeleteMealAtIndex(int a_index)*/

        /// <summary>
        /// Iterates through the meal list and deletes any meals that do not have a 
        /// entree, side and beverage.
        /// </summary>
        /// <remarks>
        /// NAME: ClearEmptyMeals
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
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
        }/*public void ClearEmptyMeals()*/

        /// <summary>
        /// With the passed Entree, either create a brand new Meal object out of the entree,
        /// or add the entree to a meal that doesn't have one yet.
        /// </summary>
        /// <remarks>
        /// NAME: CreateNewMealOrAddEntreeToMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_entree">The entree to be added to a meal or as a meal</param>
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
        }/*public void CreateNewMealOrAddEntreeToMeal(Entree a_entree)*/

        /// <summary>
        /// With the passed Side object, either create a brand new Meal object out of the Side,
        /// or add the Side to a meal that doesn't have one already.
        /// </summary>
        /// <remarks>
        /// NAME: CreateNewMealOrAddSideToMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_side">The side to be added to a meal or as a new meal</param>
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
        }/*public void CreateNewMealOrAddSideToMeal(Side a_side)*/

        /// <summary>
        /// With the passed Beverage object, either add the Beverage as a new Meal object,
        /// or add it to a Meal that doesn't already have a beverage.
        /// </summary>
        /// <remarks>
        /// NAME: CreateNewMealOrAddBeverageToMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_bev">The Beverage object to be added to a meal or as a new meal</param>
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
        }/*public void CreateNewMealOrAddBeverageToMeal(Beverage a_bev)*/

        /// <summary>
        /// Generates the Meal Matrix of the current landscape of the customer's orders.
        /// The Meal Matrix is used in conjunction with the selected index of the ListBox in MainPoSForm
        /// to properly locate the desired meal to modify or delete.
        /// </summary>
        /// <remarks>
        /// NAME: CompileMealMatrix
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
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
        }/*public void CompileMealMatrix()*/

        /// <summary>
        /// After the meal matrix is compiled, this method can be used to 
        /// locate the desired item with the index selected in the display.
        /// </summary>
        /// <remarks>
        /// NAME: FindMealViaMatrix
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_selectionIndex">The index of the selected item in the display.</param>
        /// <returns>
        /// A tuple, Item1 is the position within the meal list of the selected item. Item2 is the type of 
        /// menu item (Entree, Side, Beverage). 
        /// </returns>
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
        }/*public Tuple<int, int> FindMealViaMatrix(int a_selectionIndex)*/
    }
}
