using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJOPointOfSale
{
    
    public class PriceCalculator
    {
        private readonly int[] m_copyOfAttrs;
        private readonly int[] m_copyOfOriginalAttrs;
        private const decimal m_premiumToppingPrice = 1.29M;
        private const decimal m_premiumCheesePrice = 0.70M;
        private const decimal m_premiumProteinPrice = 2.00M;
        private const int m_singleAddition = 1;
        private const int m_extraAddition = 2;

        public decimal BasePrice { get; }
        public decimal AdditionalPricing { get; private set; }

        /// <summary>
        /// A constructor for PriceCalculator object. Initializes members and prepares them for calculating.
        /// </summary>
        /// <remarks>
        /// NAME: PriceCalculator
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        /// <param name="a_entree">The Entree whose price needs calculating</param>
        public PriceCalculator(Entree a_entree)
        {
            BasePrice = a_entree.Price;
            m_copyOfAttrs = new int[a_entree.DecomposeAttributes().Length];
            m_copyOfOriginalAttrs = new int[a_entree.DecomposeOriginalAttributes().Length];
            Array.Copy(a_entree.DecomposeAttributes(), m_copyOfAttrs, a_entree.DecomposeAttributes().Length);
            Array.Copy(a_entree.DecomposeOriginalAttributes(), m_copyOfOriginalAttrs, a_entree.DecomposeOriginalAttributes().Length);
        }/*public PriceCalculator(Entree a_entree)*/

        /// <summary>
        /// The main method calls the calculator methods in this object. 
        /// </summary>
        /// <remarks>
        /// NAME: Calculate
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        /// <returns>
        /// Returns the base price of the entree + any price additions via premiums
        /// </returns>
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
        } /*public decimal Calculate()*/

        /// <summary>
        /// Some signatures include premium ingredients and are baked into the base price.
        /// This method simply decrements all premium additions to make them easier to work
        /// with.
        /// </summary>
        /// <remarks>
        /// NAME: DecrementSignatureIncludedPremiums
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        private void DecrementSignatureIncludedPremiums()
        {
            foreach (int premium in MenuItemAttributes.PremiumAttributes)
            {
                m_copyOfAttrs[premium]--;
            }
        }/*private void DecrementSignatureIncludedPremiums()*/

        /// <summary>
        /// Steps through all the premium toppings indexes and depending on the value
        /// at that index, determines if there is a price increase necessary.
        /// </summary>
        /// <remarks>
        /// NAME: CalculateToppingsAdditions
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
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
        }/*private void CalculateToppingsAdditions()*/

        /// <summary>
        /// Steps through all the premium cheese indexes and depending on the value
        /// at that index, determines if there is a price increase necessary. 
        /// </summary>
        /// <remarks>
        /// NAME: CalculateCheeseAdditions
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
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
        }/*private void CalculateCheeseAdditions()*/

        /// <summary>
        /// Steps through all the premium protein indexes and depending on the value
        /// at that index, determines if there is a price increase necessary. 
        /// </summary>
        /// <remarks>
        /// NAME: CalculateProteinAdditions
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
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
        }/*private void CalculateProteinAdditions()*/

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// NAME: Calculate
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/3/2019
        /// </remarks>
        /// <param name="a_additionType"></param>
        /// <param name="a_price"></param>
        private void IncreaseAdditionalPricing(int a_additionType, decimal a_price)
        {
            AdditionalPricing += a_price * a_additionType;
        }/*private void IncreaseAdditionalPricing(int a_additionType, decimal a_price)*/
    }
}
