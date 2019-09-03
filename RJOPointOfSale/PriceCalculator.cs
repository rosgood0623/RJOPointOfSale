using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    public class PriceCalculator
    {
        private Entree m_menuItem;
        private readonly int[] m_copyOfAttrs;
        private readonly int[] m_copyOfOriginalAttrs;
        private const decimal m_premiumToppingPrice = 1.29M;
        private const decimal m_premiumCheesePrice = 0.70M;
        private const decimal m_premiumProteinPrice = 2.00M;
        private const int m_singleAddition = 1;
        private const int m_extraAddition = 2;

        public decimal BasePrice { get; }
        public decimal AdditionalPricing { get; private set; }

        public PriceCalculator(Entree a_entree)
        {
            m_menuItem = a_entree;
            BasePrice = m_menuItem.Price;
            m_copyOfAttrs = new int[m_menuItem.DecomposeAttributes().Length];
            m_copyOfOriginalAttrs = new int[m_menuItem.DecomposeOriginalAttributes().Length];
            Array.Copy(m_menuItem.DecomposeAttributes(), m_copyOfAttrs, m_menuItem.DecomposeAttributes().Length);
            Array.Copy(m_menuItem.DecomposeOriginalAttributes(), m_copyOfOriginalAttrs, m_menuItem.DecomposeOriginalAttributes().Length);
        }

        public decimal Calculate()
        {
            DecrementSignatureIncludedPremiums();
            //If Premium = -1; NO order
            //If Premium = 0; Single order - increase price only if 0 in original attrs. Indicates a premium topping addition.
            //If Premium = 1; EXTRA order
            //If Premium = 2; Light Premium - no price increase
            //If Premium = 3; On side Premium - no price increase.
            CalculateToppingsAdditions();
            CalculateCheeseAdditions();
            CalculateProteinAdditions();
            return BasePrice + AdditionalPricing;
        }

        private void DecrementSignatureIncludedPremiums()
        {
            foreach (int premium in MenuItemAttributes.PremiumAttributes)
            {
                m_copyOfAttrs[premium]--;
            }
        }

        private void CalculateToppingsAdditions()
        {
            foreach (int topping in MenuItemAttributes.PremiumAttributesToppings)
            {
                if (m_copyOfAttrs[topping] == 0 && m_copyOfOriginalAttrs[topping] == 0)
                {
                    IncreaseAdditionalPricing(m_singleAddition, m_premiumToppingPrice);
                }
                else if (m_copyOfAttrs[topping] == 1 && m_copyOfOriginalAttrs[topping] == 0)
                {
                    IncreaseAdditionalPricing(m_extraAddition, m_premiumToppingPrice);
                }
            }
        }

        private void CalculateCheeseAdditions()
        {
            foreach (int cheese in MenuItemAttributes.PremiumAttributesCheese)
            {
                if (m_copyOfAttrs[cheese] == 0 && m_copyOfOriginalAttrs[cheese] == 0)
                {
                    IncreaseAdditionalPricing(m_singleAddition, m_premiumCheesePrice);
                }
                else if (m_copyOfAttrs[cheese] == 1 && m_copyOfOriginalAttrs[cheese] == 0)
                {
                    IncreaseAdditionalPricing(m_extraAddition, m_premiumCheesePrice);
                }
            }
        }

        private void CalculateProteinAdditions()
        {
            foreach (int protein in MenuItemAttributes.PremiumAttributesProtein)
            {
                if (m_copyOfAttrs[protein] == 0 && m_copyOfOriginalAttrs[protein] == 0)
                {
                    IncreaseAdditionalPricing(m_singleAddition, m_premiumProteinPrice);
                }
                else if (m_copyOfAttrs[protein] == 1 && m_copyOfOriginalAttrs[protein] == 1)
                {
                    IncreaseAdditionalPricing(m_singleAddition, m_premiumProteinPrice);
                }
                else if (m_copyOfAttrs[protein] == 1 && m_copyOfOriginalAttrs[protein] == 0)
                {
                    IncreaseAdditionalPricing(m_extraAddition, m_premiumProteinPrice);
                }
                else if (m_copyOfAttrs[protein] > 1 && (m_copyOfOriginalAttrs[protein] == 1 ||
                         m_copyOfOriginalAttrs[protein] == 0))
                {
                    IncreaseAdditionalPricing(m_copyOfAttrs[protein], m_premiumProteinPrice);
                }
            }

            if (m_copyOfAttrs[MenuItemAttributes.PremiumAttributeBeefDouble] == 0 &&
                m_copyOfOriginalAttrs[MenuItemAttributes.PremiumAttributeBeefDouble] == 1)
            {
                IncreaseAdditionalPricing(m_singleAddition, m_premiumProteinPrice);
            }
        }

        private void IncreaseAdditionalPricing(int a_additionType, decimal a_price)
        {
            AdditionalPricing += a_price * a_additionType;
        }
    }
}
