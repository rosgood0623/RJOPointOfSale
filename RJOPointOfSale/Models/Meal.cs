using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class Meal
    {
        private Entree m_entree;
        private Side m_side;
        private Beverage m_beverage;
        public bool SentFlag { get; set; }
        /// <summary>
        /// The default constructor for the Meal object.
        /// </summary>
        /// <remarks>
        /// NAME: Meal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public Meal()
        {

        }/*public Meal()*/

        /// <summary>
        /// A constructor for the Meal object. Called when a new meal is created from an entree
        /// </summary>
        /// <remarks>
        /// NAME: Meal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_entree"></param>
        public Meal(Entree a_entree)
        {
            m_entree = a_entree;
        }/*public Meal(Entree a_entree)*/

        /// <summary>
        /// A constructor for the Meal object. Called when a new meal is created from a side.
        /// </summary>
        /// <remarks>
        /// NAME: Meal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_side"></param>
        public Meal(Side a_side)
        {
            m_side = a_side;
        }/*public Meal(Side a_side)*/

        /// <summary>
        /// A constructor for the Meal object. Called when a new meal is created from a beverage.
        /// </summary>
        /// <remarks>
        /// NAME: Meal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_beverage"></param>
        public Meal(Beverage a_beverage)
        {
            m_beverage = a_beverage;
        }/*public Meal(Beverage a_beverage)*/

        /// <summary>
        /// Checks if the meal has an entree.
        /// </summary>
        /// <remarks>
        /// NAME: HasEntree
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// Returns true if the entree isn't null, false otherwise.
        /// </returns>
        public bool HasEntree()
        {
            return m_entree != null;
        }/*public bool HasEntree()*/

        /// <summary>
        /// Checks if the meal has a side.
        /// </summary>
        /// <remarks>
        /// NAME: HasSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// Returns if the side isn't null, false otherwise.
        /// </returns>
        public bool HasSide()
        {
            return m_side != null;
        }/* public bool HasSide()*/

        /// <summary>
        /// Checks if the meal have a beverage.
        /// </summary>
        /// <remarks>
        /// NAME: HasBeverage
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// Returns true if the beverage isn't null, false otherwise.
        /// </returns>
        public bool HasBeverage()
        {
            return m_beverage != null;
        }/*public bool HasBeverage()*/

        /// <summary>
        /// Adds the passed Entree into the meal.
        /// </summary>
        /// <remarks>
        /// NAME: AddEntreeToMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_entree">The Entree object to be added.</param>
        public void AddEntreeToMeal(Entree a_entree)
        {
            m_entree = a_entree;
        }/*public void AddEntreeToMeal(Entree a_entree)*/

        /// <summary>
        /// Adds the passed Side into the meal
        /// </summary>
        /// <remarks>
        /// NAME: AddSideToMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_side">The Side object to be added.</param>
        public void AddSideToMeal(Side a_side)
        {
            m_side = a_side;
        }/*public void AddSideToMeal(Side a_side)*/

        /// <summary>
        /// Adds the passed Beverage into the meal.
        /// </summary>
        /// <remarks>
        /// NAME: AddBeverageToMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_beverage">The Beverage object to be added.</param>
        public void AddBeverageToMeal(Beverage a_beverage)
        {
            m_beverage = a_beverage;
        }/*public void AddBeverageToMeal(Beverage a_beverage)*/

        /// <summary>
        /// An accessor method to retrieve the entree
        /// </summary>
        /// <remarks>
        /// NAME: GetEntree
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// The Entree of the meal
        /// </returns>
        public Entree GetEntree()
        {
            return m_entree;
        }/*public Entree GetEntree()*/

        /// <summary>
        /// An accessor method to retrieve the side of the meal.
        /// </summary>
        /// <remarks>
        /// NAME: GetSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// The Side object of the meal.
        /// </returns>
        public Side GetSide()
        {
            return m_side;
        }/*public Side GetSide()*/

        /// <summary>
        /// An accessor method to retrieve the Beverage of the meal.
        /// </summary>
        /// <remarks>
        /// NAME: GetBeverage
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// The Beverage object of the meal.
        /// </returns>
        public Beverage GetBeverage()
        {
            return m_beverage;
        }/*public Beverage GetBeverage()*/

        /// <summary>
        /// Removes the current entree from the meal.
        /// </summary>
        /// <remarks>
        /// NAME: RemoveEntree
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public void RemoveEntree()
        {
            m_entree = null;
        }/*public void RemoveEntree()*/

        /// <summary>
        /// Removes the current Side from the meal.
        /// </summary>
        /// <remarks>
        /// NAME: RemoveSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public void RemoveSide()
        {
            m_side = null;
        }/*public void RemoveSide()*/
        /// <summary>
        /// Removes the current beverage from the meal.
        /// </summary>
        /// <remarks>
        /// NAME: RemoveBeverage
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public void RemoveBeverage()
        {
            m_beverage = null;
        }/*public void RemoveBeverage()*/

        /// <summary>
        /// Checks to see if the current Meal is a KidsMeal.
        /// </summary>
        /// <remarks>
        /// NAME: IsKidsMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <returns>
        /// Returns true if the meal is a KidsMeal, false otherwise.
        /// </returns>
        public bool IsKidsMeal()
        {
            return m_entree?.EntreeIdentifier.Contains("Kids") ?? false;
        }/*public bool IsKidsMeal()*/
    }
}
