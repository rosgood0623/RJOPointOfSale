using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class KidsMeal : Meal
    {
        private const string m_regularFry = "RgFrenchFry";
        private const string m_kidsSoda = "KidsSoda";
        private string m_kidsMealBeverage;

        public string KidsMealSide { get; set; }
        public string KidsMealBeverage { get; set; }


        /// <summary>
        /// Creates a kids meal, a special type of meal with specific interactions and behaviors
        /// </summary>
        /// <remarks>
        /// NAME: KidsMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public KidsMeal()
        {
            GenerateDefaultKidsMeal();
        }/*public KidsMeal()*/

        /// <summary>
        /// Generates the default kids meal, which includes a plain cheesebuger, a regular fry
        /// and a kids sized drink.
        /// </summary>
        /// <remarks>
        /// NAME: GenerateDefaultKidsMeal
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void GenerateDefaultKidsMeal()
        {
            Side kidsSide = new Side();
            kidsSide.RetrieveSideFromDb(m_regularFry);
            AddSideToMeal(kidsSide);

            Beverage kidsBev = new Beverage();
            kidsBev.RetrieveBevFromDb(m_kidsSoda);
            AddBeverageToMeal(kidsBev);
        }/*private void GenerateDefaultKidsMeal()*/

        /// <summary>
        /// Updates the kids side with a new side, querying the database to find the desired side.
        /// </summary>
        /// <remarks>
        /// NAME: UpdateKidsMealSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_newSideIdentifier">The string representation of the desired side.</param>
        public void UpdateKidsMealSide(string a_newSideIdentifier)
        {
            RemoveSide();
            Side newSide = new Side();
            newSide.RetrieveSideFromDb(a_newSideIdentifier);
            AddSideToMeal(newSide);
        }/*public void UpdateKidsMealSide(string a_newSideIdentifier)*/

        /// <summary>
        /// Updates the kids meal with a new beverage, query the database to find the desired item.
        /// </summary>
        /// <remarks>
        /// NAME: UpdateKidsMealBeverage
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_newBevIdentifier">The string presentation of the deisred beverage.</param>
        public void UpdateKidsMealBeverage(string a_newBevIdentifier)
        {
            RemoveBeverage();
            Beverage newBev = new Beverage();
            newBev.RetrieveBevFromDb(a_newBevIdentifier);
            AddBeverageToMeal(newBev);
        }/*public void UpdateKidsMealBeverage(string a_newBevIdentifier)*/

    }
}
