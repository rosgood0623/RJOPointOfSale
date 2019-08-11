using System;
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
        
        public EntreeView(Entree a_entree)
        {
            m_entree = a_entree;
            m_textAttributes.ListChanged += TextAttributes_ListChanged;
        }

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
        }

        public BindingList<string> GetTextMods()
        {
            return m_textAttributes;
        }

        void TextAttributes_ListChanged(object sender, ListChangedEventArgs e)
        {
            Debug.WriteLine(e.NewIndex.ToString());
        }


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
        }

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
        }

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
        }

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
        }

        private void AddTextAttributeProteinAdditionsToView(int a_threshold, int a_menuItemAttr)
        {
            for (int i = 0; i < a_threshold; i++)
            {
                string combinedModAttr = TextAttributeCombinator(MenuItemAttributes.AddAttribute, MenuItemAttributes.attributesDictionary[a_menuItemAttr]);
                m_textAttributes.Add(combinedModAttr);
                m_attrBuilder.AppendLine(combinedModAttr);
            }
        }

        private void AddTextAttributeNoProteinToView()
        {
            string combinedModAttr = TextAttributeCombinator("", MenuItemAttributes.attributesDictionary[MenuItemAttributes.SandwichNoMeat]);
            m_textAttributes.Add(combinedModAttr);
            m_attrBuilder.AppendLine(combinedModAttr);
        }

        private void AddTextAttributeModificationsToView(int[] a_attrs, int[] a_origAttrs)
        {
            for (int i = 1; i < MenuItemAttributes.SandwichFriedEgg; i++)
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
        }

        private static string TextAttributeCombinator(string a_mod, string a_attr)
        {
            return "\t" + a_mod + " " + a_attr;
        }

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
        }

    }
}
