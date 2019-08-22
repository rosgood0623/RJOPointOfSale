using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RJOPointOfSale.Properties;

namespace RJOPointOfSale
{
    public partial class KidsMealModification : Form
    {
        private readonly KidsMeal m_kidsMealFromMain;
        private readonly int[] m_attributes = new int[MenuItemAttributes.NumOfAttributes];
        private readonly MealView m_mealView;
        private List<Button> m_buttonAttributes;
        private List<Button> m_SideButtons;
        private List<Button> m_BevButtons;
        private BindingList<string> m_kidsAttributes = new BindingList<string>();
        public string DefaultSide { get; set; }
        public string DefaultBev { get; set; }
        public string CurrentlyChosenSide { get; set; }
        public string CurrentlyChosenBev { get; set; }

        /// <summary>
        /// The defualt constructor for the KidsMealModification. Initializes the components.
        /// </summary>
        /// <remarks>
        /// NAME: KidsMealModification
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        public KidsMealModification()
        {
            InitializeComponent();

        } /*public KidsMealModification()*/

        /// <summary>
        /// A constructor for the KidsMealModification. Inherits from the default constructor.
        /// Passes in a KidsMeal and intializes the members that are vital 
        /// to the ability to modify the meal.
        /// </summary>
        /// <remarks>
        /// NAME: KidsMealModification
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="a_kidsMeals"></param>
        public KidsMealModification(KidsMeal a_kidsMeals) : this()
        {
            m_kidsMealFromMain = a_kidsMeals;
            m_attributes = m_kidsMealFromMain.GetEntree().DecomposeAttributes();
            m_mealView = new MealView(m_kidsMealFromMain);

            PerformViewFormatting();
            lbModifiedKidsMeal.DataSource = m_mealView.GetDisplay();

            DefaultSide = m_kidsMealFromMain.GetSide().SideIdentifier;
            DefaultBev = m_kidsMealFromMain.GetBeverage().BeverageIdentifier;
            CurrentlyChosenSide = m_kidsMealFromMain.GetSide().SideIdentifier;
            CurrentlyChosenBev = m_kidsMealFromMain.GetBeverage().BeverageIdentifier;

            GenerateAttributeButtonListAndSetProperties();
            CompleteButtonDisplay();
        }/*public KidsMealModification(KidsMeal a_kidsMeals) : this()*/

        /// <summary>
        /// Performs the view formatting for the kids meal. 
        /// Displays the entree, side and beverage to the ListBox.
        /// </summary>
        /// <remarks>
        /// NAME: PerformViewFormatting
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void PerformViewFormatting()
        {
            m_kidsAttributes.Clear();
            m_mealView.FillMealDisplay();
            m_kidsAttributes = m_mealView.GetDisplay();
            for (int i = 0; i < m_kidsAttributes.Count; i++)
            {
                if (m_kidsAttributes[i].Equals("~"))
                {
                    m_kidsAttributes.RemoveAt(i);
                }
            }
        }/*private void PerformViewFormatting()*/

        /// <summary>
        /// Completes the background image for each item that is included
        /// in the kids meal.
        /// </summary>
        /// <remarks>
        /// NAME: CompleteButtonDisplay
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void CompleteButtonDisplay()
        {
            foreach (Button button in m_buttonAttributes)
            {
                if (button.Tag.Equals(DefaultSide) || button.Tag.Equals(DefaultBev))
                {
                    button.BackgroundImage = Resources.green_check_icon_png_4;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        } /*private void CompleteButtonDisplay()*/

        /// <summary>
        /// Sorts the attribute buttons into thier respective lists
        /// and sets the properties approriate for an background image. 
        /// </summary>
        /// <remarks>
        /// NAME: GenerateAttributeButtonListAndSetProperties
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void GenerateAttributeButtonListAndSetProperties()
        {
            m_buttonAttributes = new List<Button>
            {
                btnNoBun, btnClassicBun, btnGlutenFree, btnRegFry, btnTots,
                btnSweetFry, btnRegSmashFry, btnSmashTots, btnRegSweetSmashFry,
                btnBrussels, btnSideSalad, btnKidsSoda, btnKidsMilk, btnKidsChocMilk,
                btnKidsApple, btnPButterShake, btnVanillaShake, btnChocolateShake,
                btnStrawberryShake, btnOreoShake, btnSaltedCaramelShake
            };

            m_SideButtons = new List<Button>
            {
                btnRegFry, btnTots, btnSweetFry, btnRegSmashFry,
                btnSmashTots, btnRegSweetSmashFry, btnBrussels, btnSideSalad,
            };

            m_BevButtons = new List<Button>
            {
                btnKidsSoda, btnKidsMilk, btnKidsChocMilk,
                btnKidsApple, btnPButterShake, btnVanillaShake, btnChocolateShake,
                btnStrawberryShake, btnOreoShake, btnSaltedCaramelShake
            };
            foreach (Button button in m_buttonAttributes)
            {
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
        } /*private void GenerateAttributeButtonListAndSetProperties()*/

        /// <summary>
        /// This event is raised when the user selects a side button.
        /// </summary>
        /// <remarks>
        /// NAME: BtnKidsSide_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="sender">The button that trigger this event - the side buttons. </param>
        /// <param name="e">The EventArgs associated with this event.</param>
        public void BtnKidsSide_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;
            CurrentlyChosenSide = (string)selection.Tag;
            m_kidsMealFromMain.UpdateKidsMealSide(CurrentlyChosenSide);
            PerformViewFormatting();

            ClearAllSideBackgroundImages();
            ReinitializeSideButtonBackgroundImages();

            m_kidsMealFromMain.KidsMealSide = (string)selection.Tag;
        } /*public void BtnKidsSide_Click(object sender, EventArgs e)*/


        /// <summary>
        /// This event is raised whenever the user selects a beverage button.
        /// </summary>
        /// <remarks>
        /// NAME: BtnKidsBeverage_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the beverage buttons.</param>
        /// <param name="e">The EventArgs asssociated with this event. </param>
        public void BtnKidsBeverage_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;
            CurrentlyChosenBev = (string)selection.Tag;
            m_kidsMealFromMain.UpdateKidsMealBeverage(CurrentlyChosenBev);
            PerformViewFormatting();

            ClearAllBevBackgroundImages();
            ReinitializeBeverageButtonBackgroundImages();

            m_kidsMealFromMain.KidsMealBeverage = (string)selection.Tag;
        } /*public void BtnKidsBeverage_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This method formats the text in the view ListBox for clarity.
        /// </summary>
        /// <remarks>
        /// NAME: LbCustomerCheck_DrawItem
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="sender">The object that triggered this event - the ListBox</param>
        /// <param name="e">The DrawItemEventArgs associated with this event.</param>
        private void LbCustomerCheck_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(lbModifiedKidsMeal.Items[e.Index].ToString(),
                                    new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }/*private void LbCustomerCheck_DrawItem(object sender, DrawItemEventArgs e)*/

        /// <summary>
        /// Clears the background images of each side button.
        /// </summary>
        /// <remarks>
        /// NAME: ClearAllSideBackgroundImages
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void ClearAllSideBackgroundImages()
        {
            foreach (Button button in m_SideButtons)
            {
                button.BackgroundImage = null;
            }
        }/*private void ClearAllSideBackgroundImages()*/

        /// <summary>
        /// Clears the background images of all the beverage buttons.
        /// </summary>
        /// <remarks>
        /// NAME: ClearAllBevBackgroundImages
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void ClearAllBevBackgroundImages()
        {
            foreach (Button button in m_BevButtons)
            {
                button.BackgroundImage = null;
            }
        }/*private void ClearAllBevBackgroundImages()*/

        /// <summary>
        /// Depending on the selections and original meal side ,
        /// reinitializes the background images of each of the side buttons.
        /// </summary>
        /// <remarks>
        /// NAME: ReinitializeSideButtonBackgroundImages
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void ReinitializeSideButtonBackgroundImages()
        {
            if (!DefaultSide.Equals(CurrentlyChosenSide))
            {
                Button notDefault = m_SideButtons.Find(x => x.Tag.Equals(DefaultSide));
                notDefault.BackgroundImage = Resources.RedX;

                Button currentlySelected = m_SideButtons.Find(x => x.Tag.Equals(CurrentlyChosenSide));
                currentlySelected.BackgroundImage = Resources.green_check_icon_png_4;
            }
            else
            {
                Button currentlySelected = m_SideButtons.Find(x => x.Tag.Equals(CurrentlyChosenSide));
                currentlySelected.BackgroundImage = Resources.green_check_icon_png_4;
            }
        }/*private void ReinitializeSideButtonBackgroundImages()*/

        /// <summary>
        /// Depending on the selections and original meal beverage,
        /// reinitializes the background images of each of the beverage buttons.
        /// </summary>
        /// <remarks>
        /// NAME: ReinitializeBeverageButtonBackgroundImages
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        private void ReinitializeBeverageButtonBackgroundImages()
        {
            if (!DefaultBev.Equals(CurrentlyChosenBev))
            {
                Button notDefault = m_BevButtons.Find(x => x.Tag.Equals(DefaultBev));
                notDefault.BackgroundImage = Resources.RedX;

                Button currentlySelected = m_BevButtons.Find(x => x.Tag.Equals(CurrentlyChosenBev));
                currentlySelected.BackgroundImage = Resources.green_check_icon_png_4;
            }
            else
            {
                Button currentlySelected = m_BevButtons.Find(x => x.Tag.Equals(CurrentlyChosenBev));
                currentlySelected.BackgroundImage = Resources.green_check_icon_png_4;
            }
        }/*private void ReinitializeBeverageButtonBackgroundImages()*/

        /// <summary>
        /// This click event is raised when the user wishes to close the modification form.
        /// </summary>
        /// <remarks>
        /// NAME: BtnConfirmMods_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/15/2019
        /// </remarks>
        /// <param name="sender">The button associated with this event - the Confirm Mods button</param>
        /// <param name="e">The EventArgs associated with thsi event.</param>
        private void BtnConfirmMods_Click(object sender, EventArgs e)
        {
            Close();
        }/*private void BtnConfirmMods_Click(object sender, EventArgs e)*/
    }
}
