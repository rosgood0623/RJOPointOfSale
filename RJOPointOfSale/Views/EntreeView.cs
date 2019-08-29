using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RJOPointOfSale
{
    public class EntreeView
    {
        private readonly Entree m_entree;
        private readonly BindingList<string> m_textAttributes = new BindingList<string>();
        private readonly StringBuilder m_attrBuilder = new StringBuilder();
        private const int m_includesThisProteinFlag = 1;
        private const int m_excludesThisProteinFlag = 0;

        /// <summary>
        /// A constructor for the EntreeView. Initializes the ListChanged event and the Entree object.
        /// </summary>
        /// <remarks>
        /// NAME: EntreeView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_entree">The Entree that belongs to this EntreeView.</param>
        public EntreeView(Entree a_entree)
        {
            m_entree = a_entree;
            m_textAttributes.ListChanged += TextAttributes_ListChanged;
        }/*public EntreeView(Entree a_entree)*/

        /// <summary>
        /// The setup method for firing off the other view formatting methods.
        /// </summary>
        /// <remarks>
        /// NAME: DecomposeAttributesToString
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <returns>
        /// The formatted view of the EntreeView's attributes.
        /// </returns>
        public string DecomposeAttributesToString()
        {
            m_attrBuilder.Clear();
            m_textAttributes.Clear();
            int[] sandwichAttrs = m_entree.DecomposeAttributes();
            int[] sandwichOriginalAttrs = m_entree.DecomposeOriginalAttributes();
            string sandwichIdentifier = m_entree.EntreeIdentifier;
            m_textAttributes.Add(sandwichIdentifier);
            m_attrBuilder.AppendLine(sandwichIdentifier);

            if (sandwichIdentifier != null)
            {
                AddTextAttributeModificationsToView(sandwichAttrs, sandwichOriginalAttrs);
                StartAddingTextAttributeProteinAdditionsToViewForBeef(sandwichAttrs, sandwichIdentifier);
                StartAddingTextAttributeProteinAdditionsToViewForGrilled(sandwichAttrs, sandwichIdentifier);
                StartAddingTextAttributeProteinAdditionsToViewForCrispy(sandwichAttrs, sandwichIdentifier);
                StartAddingTextAttributeProteinAdditionsToViewForBlackBean(sandwichAttrs, sandwichIdentifier);
                AddTextActionModifiersToView();
            }

            return m_attrBuilder.ToString();
        }/*public string DecomposeAttributesToString()*/

        /// <summary>
        /// The accessor method for the text attributes of the Entree.
        /// </summary>
        /// <remarks>
        /// NAME: GetTextMods
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <returns>
        /// The BindingList of the EntreeView, contains the display-ready strings for the view.
        /// </returns>
        public BindingList<string> GetTextMods()
        {
            return m_textAttributes;
        }/*public BindingList<string> GetTextMods()*/

        /// <summary>
        /// Used mostly for debugging the behavior of the BindingList.
        /// </summary>
        /// <remarks>
        /// NAME: TextAttributes_ListChanged
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="sender">The object that triggers the event, in this case, the BindingList.</param>
        /// <param name="e">The ListChangedEventArgs object associated with the event.</param>
        void TextAttributes_ListChanged(object sender, ListChangedEventArgs e)
        {
            Debug.WriteLine(e.NewIndex.ToString());
        }/*void TextAttributes_ListChanged(object sender, ListChangedEventArgs e)*/

        /// <summary>
        /// This method helps in handling the many different protein addition scenarios for beef sandwiches, 
        /// like if the item includes or excludes a protein type originally or if the sandwich is a 
        /// beef double (because it has it's own index in the attribute array.)
        /// </summary>
        /// <remarks>
        /// NAME: StartAddingTextAttributeProteinAdditionsToViewForBeef
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_attrs">The attribute array of the entree</param>
        /// <param name="a_id">The Entree identification of the entree.</param>
        private void StartAddingTextAttributeProteinAdditionsToViewForBeef(int[] a_attrs, string a_id)
        {
            if (a_attrs[MenuItemAttributes.SandwichBeefSingle] > m_includesThisProteinFlag && (a_id.Contains("Single") || a_id.Contains("Double")))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichBeefSingle] - 1, MenuItemAttributes.SandwichBeefSingle);
            }
            else if (a_attrs[MenuItemAttributes.SandwichBeefSingle] > m_excludesThisProteinFlag && !a_id.Contains("Single") && !a_id.Contains("Double"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichBeefSingle], MenuItemAttributes.SandwichBeefSingle);
            }
            else if (a_attrs[MenuItemAttributes.SandwichBeefSingle] > m_excludesThisProteinFlag && a_id.Contains("Double"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichBeefSingle], MenuItemAttributes.SandwichBeefSingle);
            }
            else if (a_attrs[MenuItemAttributes.SandwichBeefSingle] == m_excludesThisProteinFlag && a_id.Contains("Single"))
            {
                AddTextAttributeNoProteinToView();
            }
            else if (a_attrs[MenuItemAttributes.SandwichBeefDouble] == m_excludesThisProteinFlag && a_id.Contains("Double"))
            {
                AddTextAttributeNoProteinToView();
            }
        }/*private void StartAddingTextAttributeProteinAdditionsToViewForBeef(int[] a_attrs, string a_id)*/

        /// <summary>
        /// This method helps in handling the many different protein addition scenarios for Grilled chicken sandwiches, 
        /// like if the item includes or excludes a protein type originally or if the sandwich is a 
        /// beef double (because it has it's own index in the attribute array.)
        /// </summary>
        /// <remarks>
        /// NAME: StartAddingTextAttributeProteinAdditionsToViewForGrilled
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_attrs">The attribute array of the entree.</param>
        /// <param name="a_id">The Entree Identifier of the entree.</param>
        private void StartAddingTextAttributeProteinAdditionsToViewForGrilled(int[] a_attrs, string a_id)
        {
            if (a_attrs[MenuItemAttributes.SandwichGrilledChicken] > m_includesThisProteinFlag && a_id.Contains("Grilled"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichGrilledChicken] - 1, MenuItemAttributes.SandwichGrilledChicken);
            }
            else if (a_attrs[MenuItemAttributes.SandwichGrilledChicken] > m_excludesThisProteinFlag && !a_id.Contains("Grilled"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichGrilledChicken], MenuItemAttributes.SandwichGrilledChicken);
            }
            else if (a_attrs[MenuItemAttributes.SandwichGrilledChicken] > m_excludesThisProteinFlag && a_id.Contains("Double"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichGrilledChicken], MenuItemAttributes.SandwichGrilledChicken);
            }
            else if (a_attrs[MenuItemAttributes.SandwichGrilledChicken] == m_excludesThisProteinFlag && a_id.Contains("Grilled"))
            {
                AddTextAttributeNoProteinToView();
            }
        }/*private void StartAddingTextAttributeProteinAdditionsToViewForGrilled(int[] a_attrs, string a_id)*/

        /// <summary>
        /// This method helps in handling the many different protein addition scenarios for Crispy sandwiches, 
        /// like if the item includes or excludes a protein type originally or if the sandwich is a 
        /// beef double (because it has it's own index in the attribute array.)
        /// </summary>
        /// <remarks>
        /// NAME: StartAddingTextAttributeProteinAdditionsToViewForCrispy
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_attrs">The attribute array of the entree.</param>
        /// <param name="a_id">The Entree identifier of the entree.</param>
        private void StartAddingTextAttributeProteinAdditionsToViewForCrispy(int[] a_attrs, string a_id)
        {
            if (a_attrs[MenuItemAttributes.SandwichCrispyChicken] > m_includesThisProteinFlag && a_id.Contains("Crispy"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichCrispyChicken] - 1, MenuItemAttributes.SandwichCrispyChicken);
            }
            else if (a_attrs[MenuItemAttributes.SandwichCrispyChicken] > m_excludesThisProteinFlag && !a_id.Contains("Crispy"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichCrispyChicken], MenuItemAttributes.SandwichCrispyChicken);
            }
            else if (a_attrs[MenuItemAttributes.SandwichCrispyChicken] > m_excludesThisProteinFlag && a_id.Contains("Double"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichCrispyChicken], MenuItemAttributes.SandwichCrispyChicken);
            }
            else if (a_attrs[MenuItemAttributes.SandwichCrispyChicken] == m_excludesThisProteinFlag && a_id.Contains("Crispy"))
            {
                AddTextAttributeNoProteinToView();
            }
        }/*private void StartAddingTextAttributeProteinAdditionsToViewForCrispy(int[] a_attrs, string a_id)*/

        /// <summary>
        /// This method helps in handling the many different protein addition scenarios for Black Bean sandwiches, 
        /// like if the item includes or excludes a protein type originally or if the sandwich is a 
        /// beef double (because it has it's own index in the attribute array.)
        /// </summary>
        /// <remarks>
        /// NAME: StartAddingTextAttributeProteinAdditionsToViewForBlackBean
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_attrs">The attribute array of the entree.</param>
        /// <param name="a_id">The Entree identifier of the entree.</param>
        private void StartAddingTextAttributeProteinAdditionsToViewForBlackBean(int[] a_attrs, string a_id)
        {
            if (a_attrs[MenuItemAttributes.SandwichBlackBean] > m_includesThisProteinFlag && a_id.Contains("BB"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichBlackBean] - 1, MenuItemAttributes.SandwichBlackBean);
            }
            else if (a_attrs[MenuItemAttributes.SandwichBlackBean] > m_excludesThisProteinFlag && !a_id.Contains("BB"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichBlackBean], MenuItemAttributes.SandwichBlackBean);
            }
            else if (a_attrs[MenuItemAttributes.SandwichBlackBean] > m_excludesThisProteinFlag && a_id.Contains("Double"))
            {
                AddTextAttributeProteinAdditionsToView(a_attrs[MenuItemAttributes.SandwichBlackBean], MenuItemAttributes.SandwichBlackBean);
            }
            else if (a_attrs[MenuItemAttributes.SandwichBlackBean] == m_excludesThisProteinFlag && a_id.Contains(" BB"))
            {
                AddTextAttributeNoProteinToView();
            }
        }/*private void StartAddingTextAttributeProteinAdditionsToViewForBlackBean(int[] a_attrs, string a_id)*/

        /// <summary>
        /// This method is called for when there are any protein additions to the menu item.
        /// </summary>
        /// <remarks>
        /// NAME: AddTextAttributeProteinAdditionsToView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_threshold">The number of additions to be made to reflect in the view.</param>
        /// <param name="a_menuItemAttr">The specified menu modification.</param>
        private void AddTextAttributeProteinAdditionsToView(int a_threshold, int a_menuItemAttr)
        {
            for (int i = 0; i < a_threshold; i++)
            {
                string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.AddAttribute, MenuItemAttributes.attributesDictionary[a_menuItemAttr]);
                m_textAttributes.Add(combinedModAttr);
                m_attrBuilder.AppendLine(combinedModAttr);
            }
        }/*private void AddTextAttributeProteinAdditionsToView(int a_threshold, int a_menuItemAttr)*/

        /// <summary>
        /// This method is called when the above logic dictates (based off the attribute arrays) that the 
        /// customer desires NO MEAT on their sandwich. This method parses that modification and adds it 
        /// to the text attributes list.
        /// </summary>
        /// <remarks>
        /// NAME: AddTextAttributeNoProteinToView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        private void AddTextAttributeNoProteinToView()
        {
            string combinedModAttr = TextAttributeCombinator("", MenuItemAttributes.attributesDictionary[MenuItemAttributes.SandwichNoMeat]);
            m_textAttributes.Add(combinedModAttr);
            m_attrBuilder.AppendLine(combinedModAttr);
        }/*private void AddTextAttributeNoProteinToView()*/

        /// <summary>
        /// Compares the current entree's attributes to the original attributes. Steps through each
        /// attribute and uses the flag's status to determine the modification, if any. 
        /// If there aren't any modification to the attributes, and the original attributes 
        /// equals the current attributes, just the entree's identifier will be displayed.
        /// </summary>
        /// <remarks>
        /// NAME: AddTextAttributeModificationsToView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_attrs">The entree's current attributes.</param>
        /// <param name="a_origAttrs">The original attributes of the sandwich.</param>
        private void AddTextAttributeModificationsToView(int[] a_attrs, int[] a_origAttrs)
        {

            for (int i = 1; i <= MenuItemAttributes.SandwichFriedEgg; i++)
            {
                if (a_attrs[i] == 0 && a_origAttrs[i] == 1)
                {
                    string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.NoAttribute, MenuItemAttributes.attributesDictionary[i]);
                    m_textAttributes.Add(combinedModAttr);
                    m_attrBuilder.AppendLine(combinedModAttr);

                }
                else if (a_attrs[i] == 1 && a_origAttrs[i] == 0)
                {
                    string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.AddAttribute, MenuItemAttributes.attributesDictionary[i]);
                    m_textAttributes.Add(combinedModAttr);
                    m_attrBuilder.AppendLine(combinedModAttr);

                }
                else if (a_attrs[i] == MenuItemAttributes.ExtraAttribute)
                {
                    string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.ExtraString, MenuItemAttributes.attributesDictionary[i]);
                    m_textAttributes.Add(combinedModAttr);
                    m_attrBuilder.AppendLine(combinedModAttr);
                }
                else if (a_attrs[i] == MenuItemAttributes.LightAttribute)
                {
                    string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.LightString, MenuItemAttributes.attributesDictionary[i]);
                    m_textAttributes.Add(combinedModAttr);
                    m_attrBuilder.AppendLine(combinedModAttr);
                }
                else if (a_attrs[i] == MenuItemAttributes.OnSideAttribute)
                {
                    string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.OnSideString, MenuItemAttributes.attributesDictionary[i]);
                    m_textAttributes.Add(combinedModAttr);
                    m_attrBuilder.AppendLine(combinedModAttr);
                }
            }
        }/*private void AddTextAttributeModificationsToView(int[] a_attrs, int[] a_origAttrs)*/

        /// <summary>
        /// A simple method for combining the the attribute with it's modification.
        /// </summary>
        /// <remarks>
        /// NAME: TextAttributeCombinator
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        /// <param name="a_mod">The string representation of the modification of the attribute.</param>
        /// <param name="a_attr">The string representation of the attribute.</param>
        /// <returns></returns>
        private static string TextAttributeCombinator(string a_mod, string a_attr)
        {
            return "\t" + a_mod + " " + a_attr;
        }/*private static string TextAttributeCombinator(string a_mod, string a_attr)*/

        /// <summary>
        /// This method checks for existence of any action mods within the Entree. If so, 
        /// add them to the attributes list.
        /// </summary>
        /// <remarks>
        /// NAME: AddTextActionModifiersToView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/16/2019
        /// </remarks>
        private void AddTextActionModifiersToView()
        {
            if (!m_entree.IsEmptyActionMods())
            {
                for (int i = 0; i < m_entree.ActionModsCount(); i++)
                {
                    m_textAttributes.Add("\t**" + m_entree.GetActionModAtIndex(i));
                    m_attrBuilder.AppendLine(m_entree.GetActionModAtIndex(i));
                }
            }
        }/*private void AddTextActionModifiersToView()*/

    }
}
