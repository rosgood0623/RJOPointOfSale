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


        public KidsMeal()
        {
            GenerateDefaultKidsMeal();
        }

        private void GenerateDefaultKidsMeal()
        {
            Side kidsSide = new Side();
            kidsSide.RetrieveSideFromDb(m_regularFry);
            AddSideToMeal(kidsSide);

            Beverage kidsBev = new Beverage();
            kidsBev.RetrieveBevFromDb(m_kidsSoda);
            AddBeverageToMeal(kidsBev);
        }

        public void UpdateKidsMealSide(string a_newSideIdentifier)
        {
            RemoveSide();
            Side newSide = new Side();
            newSide.RetrieveSideFromDb(a_newSideIdentifier);
            AddSideToMeal(newSide);
        }

        public void UpdateKidsMealBeverage(string a_newBevIdentifier)
        {
            RemoveBeverage();
            Beverage newBev = new Beverage();
            newBev.RetrieveBevFromDb(a_newBevIdentifier);
            AddBeverageToMeal(newBev);
        }

    }
}
