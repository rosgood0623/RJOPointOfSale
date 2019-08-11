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

        public KidsMealModification()
        {
            InitializeComponent();

        }

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
        }

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
        }

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
        }

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
        }


        public void BtnKidsSide_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;
            CurrentlyChosenSide = (string)selection.Tag;
            m_kidsMealFromMain.UpdateKidsMealSide(CurrentlyChosenSide);
            PerformViewFormatting();

            ClearAllSideBackgroundImages();
            ReinitializeSideButtonBackgroundImages();

            m_kidsMealFromMain.KidsMealSide = (string) selection.Tag;
        }

        

        public void BtnKidsBeverage_Click(object sender, EventArgs e)
        {
            Button selection = sender as Button;
            CurrentlyChosenBev = (string)selection.Tag;
            m_kidsMealFromMain.UpdateKidsMealBeverage(CurrentlyChosenBev);
            PerformViewFormatting();

            ClearAllBevBackgroundImages();
            ReinitializeBeverageButtonBackgroundImages();

            m_kidsMealFromMain.KidsMealBeverage = (string)selection.Tag;
        }

        private void LbCustomerCheck_DrawItem(object sender, DrawItemEventArgs e)
        {
            //INTERESTING..........COULD POSSIBLY COLOR ITEMS SENT TO THE KITCHEN A DIFFERENT COLOR IN THE FUTURE
            e.DrawBackground();
            e.Graphics.DrawString(lbModifiedKidsMeal.Items[e.Index].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }

        private void ClearAllSideBackgroundImages()
        {
            foreach (Button button in m_SideButtons)
            {
                button.BackgroundImage = null;
            }
        }

        private void ClearAllBevBackgroundImages()
        {
            foreach (Button button in m_BevButtons)
            {
                button.BackgroundImage = null;
            }
        }

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
        }

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
        }

        private void BtnConfirmMods_Click(object sender, EventArgs e)
        {
            //m_kidsMealFromMain.UpdateKidsMealSide(CurrentlyChosenSide);
            //m_kidsMealFromMain.UpdateKidsMealBeverage(CurrentlyChosenBev);
            Close();
        }
    }
}
