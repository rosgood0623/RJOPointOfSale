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
    /// <summary>
    /// Handles the modification of an entree's attributes. Used so the
    /// cashier can make any additions or subtractions of ingredients
    /// from an entree by the request of the customer.
    /// </summary>
    /// <remarks>
    /// NAME: AttributeModification
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks> 
    public partial class AttributeModification : Form
    {
        private const int m_excludeProteinOptions = 4;
        private const int m_hasAttributeFlag = 1;
        private const int m_emptyAttribute = 0;
        private const string m_beefIdentifier = "Beef";
        private const string m_crispyIdentifier = "Crispy";
        private const string m_grilledIdentifier = "Grilled";
        private const string m_doubleIdentifier = "Double";
        private const string m_blackBeanIdentifier = "BB";
        private const string m_saladIdentifier = "Salad";
        private const string m_extraIdentifier = "Extra";
        private const string m_onSideIdentifier = "On Side";
        private const string m_lightIdentifier = "Light";

        private readonly int[] m_attributes = new int[MenuItemAttributes.NumOfAttributes];
        private int[] m_originalAttributes;
        private readonly Entree m_entreeToBeModified;
        private readonly EntreeView m_entreeView;

        private List<Button> m_buttonAttributes;
        private RadioButton m_extraLightOnSide;

        /// <summary>
        /// The default constructor for the AttributeModification form. Initializes the components.
        /// </summary>
        /// <remarks>
        /// NAME: AttributeModification
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public AttributeModification()
        {
            InitializeComponent();
        }/*public AttributeModification()*/

        /// <summary>
        /// A constructor for the AttributeModification form. Inherits from the default
        /// constructor and passes in a Entree as an argument.
        /// Initializes all vital members for the entree modifications.
        /// </summary>
        /// <remarks>
        /// NAME: AttributeModification
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_entreeFromMain">The entree to be modified.</param>
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
        }/*public AttributeModification(Entree a_entreeFromMain) : this()*/

        /// <summary>
        /// Gets the original attributes from the entree. This is very important for 
        /// comparison to the user's selections.
        /// </summary>
        /// <remarks>
        /// NAME: GenerateOriginalAttrs
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        private void GenerateOriginalAttrs()
        {
            m_originalAttributes = new int[MenuItemAttributes.NumOfAttributes];

            for (int i = 0; i < MenuItemAttributes.NumOfAttributes; i++)
            {
                m_originalAttributes[i] = m_entreeToBeModified.GetOriginalAttrAtIndex(i);
            }
        }/*private void GenerateOriginalAttrs()*/

        /// <summary>
        /// Updates the each attribute button's background to accurately 
        /// reflect the status of the attributes. 
        /// </summary>
        /// <remarks>
        /// NAME: CompleteButtonDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        private void CompleteButtonDisplay()
        {
            for (int i = 1; i <= m_buttonAttributes.Count - m_excludeProteinOptions; i++)
            {
                if (m_attributes[i] != m_emptyAttribute)
                {
                    m_buttonAttributes[i - 1].BackgroundImage = Resources.green_check_icon_png_4;
                    m_buttonAttributes[i - 1].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (m_attributes[i] == m_emptyAttribute && m_originalAttributes[i] == m_hasAttributeFlag)
                {
                    m_buttonAttributes[i - 1].BackgroundImage = Resources.RedX;
                    m_buttonAttributes[i - 1].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }/*private void CompleteButtonDisplay()*/

        /// <summary>
        /// Adds the attribute buttons to a list to make for convenient 
        /// future manipulation. The buttons are ordered with respect to
        /// the order of the attributes array.
        /// </summary>
        /// <remarks>
        /// NAME: GenerateAttributeButtonListAndSetProperties
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
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
        }/*private void GenerateAttributeButtonListAndSetProperties()*/

        /// <summary>
        /// A generic click event for the action modification buttons. 
        /// Adds the text of the selected action mod to the entree's action mod 
        /// list.
        /// </summary>
        /// <remarks>
        /// NAME: BtnAddActionModification
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the action mods buttons.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnAddActionModification(object sender, EventArgs e)
        {
            Button actMod = sender as Button;
            m_entreeToBeModified.AddActionMod(actMod.Text);
            m_entreeView.DecomposeAttributesToString();
        }/*private void BtnAddActionModification(object sender, EventArgs e)*/

        /// <summary>
        /// This method handles the logic for adding protein options to the entree.
        /// The aspect of this logic that takes up the most space is Single vs Double
        /// beef sandwich. See remarks for more.
        /// </summary>
        /// <remarks>
        /// NAME: BtnAddProtein_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// The reason why the beef sandwiches takes up the most logic is because
        /// there is an attribute for a Single patty and Double patty. Adding a beef patty
        /// to a double to make a triple is an entirely separate set of logic when adding a single
        /// patty to a single sandwich
        /// </remarks>
        /// <param name="sender">The button that triggered the event - any of the protein addition buttons.</param>
        /// <param name="e">The EventArgs associated with the event.</param>
        private void BtnAddProtein_Click(object sender, EventArgs e)
        {
            Button addProtein = sender as Button;
            string buttonTag = (string)addProtein.Tag;

            if (buttonTag.Equals(m_beefIdentifier) && !m_entreeToBeModified.EntreeIdentifier.Contains(m_doubleIdentifier))
            {
                m_attributes[MenuItemAttributes.SandwichBeefSingle]++;
            }
            else if (buttonTag.Equals(m_beefIdentifier) 
                     && m_entreeToBeModified.EntreeIdentifier.Contains(m_doubleIdentifier) 
                     && m_attributes[MenuItemAttributes.SandwichBeefDouble] == 1)
            {
                m_attributes[MenuItemAttributes.SandwichBeefSingle]++;
            }
            else if (buttonTag.Equals(m_beefIdentifier) 
                     && m_entreeToBeModified.EntreeIdentifier.Contains(m_doubleIdentifier) 
                     && m_attributes[MenuItemAttributes.SandwichBeefDouble] == 0)
            {
                m_attributes[MenuItemAttributes.SandwichBeefDouble]++;
            }
            else if (buttonTag.Equals(m_crispyIdentifier))
            {
                m_attributes[MenuItemAttributes.SandwichCrispyChicken]++;
            }
            else if (buttonTag.Equals(m_grilledIdentifier))
            {
                m_attributes[MenuItemAttributes.SandwichGrilledChicken]++;
            }
            else if (buttonTag.Equals(m_blackBeanIdentifier))
            {
                m_attributes[MenuItemAttributes.SandwichBlackBean]++;
            }

            m_entreeView.DecomposeAttributesToString();
        }/*private void BtnAddProtein_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This method handles the logic for subtracting protein options for
        /// a sandwich. 
        /// </summary>
        /// <remarks>
        /// NAME: BtnSubProtein_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the protein subtraction buttons.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnSubProtein_Click(object sender, EventArgs e)
        {
            Button addProtein = sender as Button;
            string buttonTag = (string)addProtein.Tag;
            int returnThreshold = m_attributes[MenuItemAttributes.SandwichBeefSingle] +
                                  m_attributes[MenuItemAttributes.SandwichCrispyChicken] +
                                  m_attributes[MenuItemAttributes.SandwichGrilledChicken] +
                                  m_attributes[MenuItemAttributes.SandwichBlackBean] +
                                  m_attributes[MenuItemAttributes.SandwichBeefDouble];

            if (returnThreshold == 1 && !m_entreeToBeModified.EntreeIdentifier.Contains(m_saladIdentifier))
            {
                MessageBox.Show(@"If the customer wants NO MEAT on their sandwich, click the NO MEAT button");
                return;
            }

            if (buttonTag.Equals(m_beefIdentifier) && m_attributes[MenuItemAttributes.SandwichBeefSingle] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichBeefSingle]--;
            }
            else if (buttonTag.Equals(m_crispyIdentifier) && m_attributes[MenuItemAttributes.SandwichCrispyChicken] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichCrispyChicken]--;
            }
            else if (buttonTag.Equals(m_grilledIdentifier) && m_attributes[MenuItemAttributes.SandwichGrilledChicken] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichGrilledChicken]--;
            }
            else if (buttonTag.Equals(m_blackBeanIdentifier) && m_attributes[MenuItemAttributes.SandwichBlackBean] >= 1)
            {
                m_attributes[MenuItemAttributes.SandwichBlackBean]--;
            }

            m_entreeView.DecomposeAttributesToString();
        }/*private void BtnSubProtein_Click(object sender, EventArgs e)*/

        /// <summary>
        /// The click event for signifying that there is no protein on the sandwich. 
        /// This method clears all protein related attributes in the attribute array.
        /// </summary>
        /// <remarks>
        /// NAME: BtnNoMeat_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered the event - the NO MEAT button.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnNoMeat_Click(object sender, EventArgs e)
        {
            for (int i = MenuItemAttributes.SandwichBeefSingle; i <= MenuItemAttributes.SandwichBlackBean; i++)
            {
                m_attributes[i] = m_emptyAttribute;
            }

            m_entreeView.DecomposeAttributesToString();
        }/*private void BtnNoMeat_Click(object sender, EventArgs e)*/

        /// <summary>
        /// Closes the attribute modification form.
        /// </summary>
        /// <remarks>
        /// NAME: BtnModify_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggers this event - the modify button.</param>
        /// <param name="e">The EventArgs associated with the event.</param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            Close();
        }/*private void BtnModify_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This method handles the initial logic for modifying an 
        /// attribute of an entree. Perform a different action depending
        /// on the m_extraLightOnSide flag.
        /// </summary>
        /// <remarks>
        /// NAME: BtnToppings_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - any of the toppings buttons.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void BtnToppings_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;

            bool conversion = int.TryParse((string)selection.Tag, out var selectionTag);

            if (conversion && m_extraLightOnSide == null)
            {
                ModifyAttribute(selectionTag);
            }
            else if (conversion && m_extraLightOnSide.Text.Equals(m_extraIdentifier))
            {
                ModifyAttributeExtraLightOnSide(selectionTag, MenuItemAttributes.ExtraAttribute);
            }
            else if (conversion && m_extraLightOnSide.Text.Equals(m_lightIdentifier))
            {
                ModifyAttributeExtraLightOnSide(selectionTag, MenuItemAttributes.LightAttribute);
            }
            else if (conversion && m_extraLightOnSide.Text.Equals(m_onSideIdentifier))
            {
                ModifyAttributeExtraLightOnSide(selectionTag, MenuItemAttributes.OnSideAttribute);
            }

            m_entreeView.DecomposeAttributesToString();
        }/*private void BtnToppings_Click(object sender, EventArgs e)*/

        /// <summary>
        /// The click event for signifying that the customer would like no bun with their order.
        /// This method clears all bun-related attributes from the attributes array.
        /// </summary>
        /// <remarks>
        /// NAME: BtnNoBun_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered the event - the NO BUN button.</param>
        /// <param name="e">The EventARgs associated with this event.</param>
        private void BtnNoBun_Click(object sender, EventArgs e)
        {
            for (int i = MenuItemAttributes.SandwichEggBun; i <= MenuItemAttributes.SandwichGlutenFreeBun; i++)
            {
                m_attributes[i] = m_hasAttributeFlag;
                ModifyAttribute(i);
            }

            m_entreeView.DecomposeAttributesToString();
        }/*private void BtnNoBun_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This method handles the modification of a physical attribute.
        /// Behavior is different for if an entree originally had an attibute vs
        /// if the entree didn't originally have an attribute. Handles both
        /// the selection and deselection of an attribute.
        /// </summary>
        /// <remarks>
        /// NAME: ModifyAttribute
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_tag">The tag of the button which this integer came from, which is
        /// also the index of the attribute array.</param>
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
        }/*private void ModifyAttribute(int a_tag)*/

        /// <summary>
        /// This method handles the setup of the modification of a physical attribute with the Extra,
        /// Light or On Side modifications. Behavior is different depending on whether or not 
        /// an entree originally had an attribute or not. 
        /// </summary>
        /// <remarks>
        /// NAME: ModifyAttributeExtraLightOnSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_tag">The tag of the button which this integer came from, which is
        /// also the index of the attribute array.</param>
        /// <param name="a_typeOfModification">The type of modification: Extra, Light or On Side.</param>
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
        }/*private void ModifyAttributeExtraLightOnSide(int a_tag, int a_typeOfModification)*/

        /// <summary>
        /// This method exists to reflect the selection of Extra, Light or On Side 
        /// modification.
        /// </summary>
        /// <remarks>
        /// NAME: RadioButtonSelection_CheckedChanged
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the Extra, Light or On Side buttons.</param>
        /// <param name="e"></param>
        private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selection = sender as RadioButton;

            if (selection.Checked)
            {
                m_extraLightOnSide = selection;
            }
        }/*private void RadioButtonSelection_CheckedChanged(object sender, EventArgs e)*/

        /// <summary>
        /// This method is called from the ModifyAttributeExtraLightOnSide method. This method performs the
        /// physical modification of an attribute. 
        /// </summary>
        /// <remarks>
        /// NAME: ModifyButtonsForExtraLightOnSide
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_attributesIndex">The index of the attribute to be modified.</param>
        /// <param name="a_typeOfMod">The integer representation of Extra, Light or On Side. 2 = Extra, 
        /// 3 = Light, 4 = On Side.</param>
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
        }/*private void ModifyButtonsForExtraLightOnSide(int a_attributesIndex, int a_typeOfMod)*/

        /// <summary>
        /// This method formats the font of the text in the display. 
        /// </summary>
        /// <remarks>
        /// NAME: lbModifiedEntree_DrawItem
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The object that triggered this event - the ListBox.</param>
        /// <param name="e">The DrawItemEventArgs associated with this event.</param>
        private void LbModifiedEntree_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(lbModifiedEntree.Items[e.Index].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }/*private void lbModifiedEntree_DrawItem(object sender, DrawItemEventArgs e)*/

    }
}
