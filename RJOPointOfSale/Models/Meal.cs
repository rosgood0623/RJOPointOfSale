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

        public Meal()
        {

        }
        public Meal(Entree a_entree)
        {
            m_entree = a_entree;
        }

        public Meal(Side a_side)
        {
            m_side = a_side;
        }

        public Meal(Beverage a_beverage)
        {
            m_beverage = a_beverage;
        }

        public bool HasEntree()
        {
            return m_entree != null;
        }
        public bool HasSide()
        {
            return m_side != null;
        }

        public bool HasBeverage()
        {
            return m_beverage != null;
        }

        public void AddEntreeToMeal(Entree a_entree)
        {
            m_entree = a_entree;
        }

        public void AddSideToMeal(Side a_side)
        {
            m_side = a_side;
        }

        public void AddBeverageToMeal(Beverage a_beverage)
        {
            m_beverage = a_beverage;
        }

        public Entree GetEntree()
        {
            return m_entree;
        }

        public Side GetSide()
        {
            return m_side;
        }

        public Beverage GetBeverage()
        {
            return m_beverage;
        }

        public void RemoveEntree()
        {
            m_entree = null;
        }

        public void RemoveSide()
        {
            m_side = null;
        }
        public void RemoveBeverage()
        {
            m_beverage = null;
        }
        public bool IsKidsMeal()
        {
            return m_entree?.EntreeIdentifier.Contains("Kids") ?? false;
        }
    }
}
