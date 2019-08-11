using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RJOPointOfSale.Properties;

namespace RJOPointOfSale
{
    public partial class AttributeModification : Form
    {
        private const int m_excludeProteinOptions = 4;
        private const int m_hasAttributeFlag = 1;
        private const int m_emptyAttribute = 0;

        private readonly int[] m_attributes = new int[MenuItemAttributes.NumOfAttributes];
        private int[] m_originalAttributes;
        private readonly Entree m_entreeToBeModified;
        private readonly EntreeView m_entreeView;

        private List<Button> m_buttonAttributes;
        private RadioButton m_extraLightOnSide;

        public AttributeModification()
        {
            InitializeComponent();
        }

        public AttributeModification(Entree a_entreeFromMain) : this()
        {
            m_entreeToBeModified = a_entreeFromMain;
            m_attributes = m_entreeToBeModified.DecomposeAttributes();
            m_entreeView = new EntreeView(m_entreeToBeModified);
            m_entreeView.DecomposeAttributesToString();
            lbModifiedEntree.DataSource = m_entreeView.GetTextMods();
            
            GenerateAttributeButtonListAndSetProperties();
            GenerateOriginalAttrs();
            CompleteButtonDisplay();
        }

        
        private void GenerateOriginalAttrs()
        {
            m_originalAttributes = new int[MenuItemAttributes.NumOfAttributes];

            for (int i = 0; i < MenuItemAttributes.NumOfAttributes; i++)
            {
                m_originalAttributes[i] = m_entreeToBeModified.GetOriginalAttrAtIndex(i);
            }
        }

        private void CompleteButtonDisplay()
        {
            for (int i = 1; i < m_buttonAttributes.Count - m_excludeProteinOptions; i++)
            {
                if (m_attributes[i] != m_emptyAttribute)
                {
                    m_buttonAttributes[i-1].BackgroundImage = Resources.green_check_icon_png_4;
                    m_buttonAttributes[i-1].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (m_attributes[i] == m_emptyAttribute && m_originalAttributes[i] == m_hasAttributeFlag)
                {
                    m_buttonAttributes[i - 1].BackgroundImage = Resources.RedX;
                    m_buttonAttributes[i - 1].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void GenerateAttributeButtonListAndSetProperties()
        {
            m_buttonAttributes = new List<Button>
            {
                btnNoBun, btnClassicBun, btnChipotleBun, btnMultigrain, btnGlutenFree,
                btnBBQSauce, btnRanch, btnChipotleMayo, btnSmashSauce, btnKetchup,
                btnMustard, btnTruffleMayo, btnMayo, btnBalsamicVin, btnGuacamole,
                btnLettuce, btnTomatoes, btnRedOnion, btnPickles, btnGrilledOnion,
                btnMushrooms, btnBacon, btnJalapenos, btnAmerican, btnPepperjack,
                btnSwiss, btnCheddar, btnShredChed, btnBleu, btnAvocado,
                btnHaystacks, btnFriedEgg, btnAddBeef, btnAddGrilledChicken, btnAddCrispy,
                btnAddBBPatty
            };

            foreach (var button in m_buttonAttributes)
            {
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }


        }

        private void BtnAddActionModification(object sender, EventArgs e)
        {
            Button actMod = sender as Button;
            m_entreeToBeModified.AddActionMod(actMod.Text);
            m_entreeView.DecomposeAttributesToString();
        }

        private void BtnAddProtein_Click(object sender, EventArgs e)
        {
            Button addProtein = sender as Button;
            string buttonTag = (string) addProtein.Tag;

            if (buttonTag.Equals("Beef") && !m_entreeToBeModified.EntreeIdentifier.Contains("Double"))
            {
                m_attributes[MenuItemAttributes.SandwichBeefSingle]++;
            }
            else if (buttonTag.Equals("Beef") && m_entreeToBeModified.EntreeIdentifier.Contains("Double") && m_attributes[MenuItemAttributes.SandwichBeefDouble] == 1)
            {
                m_attributes[MenuItemAttributes.SandwichBeefSingle]++;
            }
            else if (buttonTag.Equals("Beef") && m_entreeToBeModified.EntreeIdentifier.Contains("Double") && m_attributes[MenuItemAttributes.SandwichBeefDouble] == 0)
            {
                m_attributes[MenuItemAttributes.SandwichBeefDouble]++;
            }
            else if (buttonTag.Equals("Crispy"))
            {
                m_attributes[MenuItemAttributes.SandwichCrispyChicken]++;
            }
            else if (buttonTag.Equals("Grilled"))
            {
                m_attributes[MenuItemAttributes.SandwichGrilledChicken]++;
            }
            else if (buttonTag.Equals("BB"))
            {
                m_attributes[MenuItemAttributes.SandwichBlackBean]++;
            }

            m_entreeView.DecomposeAttributesToString();
        }

        private void BtnSubProtein_Click(object sender, EventArgs e)
        {
            Button addProtein = sender as Button;
            string buttonTag = (string)addProtein.Tag;
            int returnThreshold = m_attributes[MenuItemAttributes.SandwichBeefSingle] +
                                  m_attributes[MenuItemAttributes.SandwichCrispyChicken] +
                                  m_attributes[MenuItemAttributes.SandwichGrilledChicken] +
                                  m_attributes[MenuItemAttributes.SandwichBlackBean] +
                                  m_attributes[MenuItemAttributes.SandwichBeefDouble];

            if (returnThreshold == 1)
            {
                MessageBox.Show(@"If the customer wants NO MEAT on their sandwich, click the NO MEAT button");
                return;
            }

            if (buttonTag.Equals("Beef") && m_attributes[MenuItemAttributes.SandwichBeefSingle] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichBeefSingle]--;
               
            }
            else if (buttonTag.Equals("Crispy") && m_attributes[MenuItemAttributes.SandwichCrispyChicken] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichCrispyChicken]--;
            }
            else if (buttonTag.Equals("Grilled") && m_attributes[MenuItemAttributes.SandwichGrilledChicken] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichGrilledChicken]--;
            }
            else if (buttonTag.Equals("BB") && m_attributes[MenuItemAttributes.SandwichBlackBean] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichBlackBean]--;
            }

            m_entreeView.DecomposeAttributesToString();
        }

        private void BtnNoMeat_Click(object sender, EventArgs e)
        {
            for (int i = MenuItemAttributes.SandwichBeefSingle; i <= MenuItemAttributes.SandwichBlackBean; i++)
            {
                m_attributes[i] = m_emptyAttribute;
            }

            m_entreeView.DecomposeAttributesToString();
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnToppings_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;

            bool conversion = int.TryParse((string)selection.Tag, out var selectionTag);

            if (conversion && m_extraLightOnSide == null)
            {
                ModifyAttribute(selectionTag);
            }
            else if (conversion && m_extraLightOnSide.Text.Equals("Extra"))
            {
                ModifyAttributeExtraLightOnSide(selectionTag, MenuItemAttributes.ExtraAttribute);
            }
            else if (conversion && m_extraLightOnSide.Text.Equals("Light"))
            {
                ModifyAttributeExtraLightOnSide(selectionTag, MenuItemAttributes.LightAttribute);
            }
            else if (conversion && m_extraLightOnSide.Text.Equals("On Side"))
            {
                ModifyAttributeExtraLightOnSide(selectionTag, MenuItemAttributes.OnSideAttribute);
            }

            m_entreeView.DecomposeAttributesToString();
        }

        private void BtnNoBun_Click(object sender, EventArgs e)
        {
            for (int i = MenuItemAttributes.SandwichEggBun; i <= MenuItemAttributes.SandwichGlutenFreeBun; i++)
            {
                m_attributes[i] = m_hasAttributeFlag;
                ModifyAttribute(i);
            }

            m_entreeView.DecomposeAttributesToString();
        }

        private void ModifyAttribute(int a_tag)
        {
            //If attribute is selected && was part of original signature
            if (m_attributes[a_tag] >= m_hasAttributeFlag && m_originalAttributes[a_tag] == m_hasAttributeFlag)
            {
                m_attributes[a_tag] = m_emptyAttribute;
                m_buttonAttributes[a_tag - 1].BackgroundImage = Resources.RedX;
            }
            //if attribute is not selected and was part of original
            else if (m_attributes[a_tag] == m_emptyAttribute && m_originalAttributes[a_tag] == m_hasAttributeFlag)
            {
                m_attributes[a_tag] = m_hasAttributeFlag;
                m_buttonAttributes[a_tag - 1].BackgroundImage = Resources.green_check_icon_png_4;
            }
            //If attribute is selected and wasn't a part of original
            else if (m_attributes[a_tag] >= m_hasAttributeFlag && m_originalAttributes[a_tag] == m_emptyAttribute)
            {
                m_attributes[a_tag] = m_emptyAttribute;
                m_buttonAttributes[a_tag - 1].BackgroundImage = null;
            }
            //If attribute is not selected and wasn't part of original signature
            else if (m_attributes[a_tag] == m_emptyAttribute && m_originalAttributes[a_tag] == m_emptyAttribute)
            {
                m_attributes[a_tag] = m_hasAttributeFlag;
                m_buttonAttributes[a_tag - 1].BackgroundImage = Resources.green_check_icon_png_4;
            }
        }

        private void ModifyAttributeExtraLightOnSide(int a_tag, int a_typeOfModification)
        {
            //If attribute is selected && was part of original signature
            if (m_attributes[a_tag] > m_hasAttributeFlag && m_originalAttributes[a_tag] == m_hasAttributeFlag)
            {
                m_attributes[a_tag] = m_emptyAttribute;
                ModifyButtonsForExtraLightOnSide(a_tag, a_typeOfModification);
            }
            //if attribute is not selected and was part of original
            else if ((m_attributes[a_tag] == m_emptyAttribute || m_attributes[a_tag] == m_hasAttributeFlag) && m_originalAttributes[a_tag] == m_hasAttributeFlag)
            {
                ModifyButtonsForExtraLightOnSide(a_tag, a_typeOfModification);
            }
            //Behavior: Unselect attribute that wasn't a part of the original signature
            else if (m_attributes[a_tag] > m_hasAttributeFlag && m_originalAttributes[a_tag] == m_emptyAttribute)
            {
                m_attributes[a_tag] = m_emptyAttribute;
                ModifyButtonsForExtraLightOnSide(a_tag, a_typeOfModification);

            }
            //Behavior: Select an attribute that wasn't a part of the original signature
            else if (m_attributes[a_tag] == m_emptyAttribute && m_originalAttributes[a_tag] == m_emptyAttribute)
            {
                ModifyButtonsForExtraLightOnSide(a_tag, a_typeOfModification);
            }

            m_extraLightOnSide.Checked = false;
            m_extraLightOnSide = null;
        }

        private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selection = sender as RadioButton;

            if (selection.Checked)
            {
                m_extraLightOnSide = selection;
            }
        }

        private void ModifyButtonsForExtraLightOnSide(int a_attributesIndex, int a_typeOfMod)
        {
            switch (a_typeOfMod)
            {
                case MenuItemAttributes.ExtraAttribute:
                    m_attributes[a_attributesIndex] = MenuItemAttributes.ExtraAttribute;
                    m_buttonAttributes[a_attributesIndex - 1].BackgroundImage = Resources.ExtraAttributes;
                    break;
                case MenuItemAttributes.LightAttribute:
                    m_attributes[a_attributesIndex] = MenuItemAttributes.LightAttribute;
                    m_buttonAttributes[a_attributesIndex - 1].BackgroundImage = Resources.LightAttributes;
                    break;
                case MenuItemAttributes.OnSideAttribute:
                    m_attributes[a_attributesIndex] = MenuItemAttributes.OnSideAttribute;
                    m_buttonAttributes[a_attributesIndex - 1].BackgroundImage = Resources.OnSideAttributes;
                    break;
            }
        }

        private void lbModifiedEntree_DrawItem(object sender, DrawItemEventArgs e)
        {
            //INTERESTING..........COULD POSSIBLY COLOR ITEMS SENT TO THE KITCHEN A DIFFERENT COLOR IN THE FUTURE
            e.DrawBackground();
            e.Graphics.DrawString(lbModifiedEntree.Items[e.Index].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }

    }
}
