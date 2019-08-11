using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RJOPointOfSale
{
    public static class MenuItemAttributes
    {
        public const int Initialized = 0;
        public const int SandwichNoBun = 1;
        public const int SandwichEggBun = 2;
        public const int SandwichChipolteBun = 3;
        public const int SandwichMultigrainBun = 4;
        public const int SandwichGlutenFreeBun = 5;
        public const int SandwichBBQ = 6;
        public const int SandwichRanch = 7;
        public const int SandwichChipotleMayo = 8;
        public const int SandwichSmashSauce = 9;
        public const int SandwichKetchup = 10;
        public const int SandwichMustard = 11;
        public const int SandwichTruffleMayo = 12;
        public const int SandwichMayo = 13;
        public const int SandwichBalsamic = 14;
        public const int SandwichGuacamole = 15;
        public const int SandwichLettuce = 16;
        public const int SandwichTomatoes = 17;
        public const int SandwichRedOnions = 18;
        public const int SandwichPickles = 19;
        public const int SandwichGrilledOnions = 20;
        public const int SandwichGrilledMushrooms = 21;
        public const int SandwichBacon = 22;
        public const int SandwichJalapenos = 23;
        public const int SandwichAmerican = 24;
        public const int SandwichPepperJack = 25;
        public const int SandwichSwiss = 26;
        public const int SandwichCheddar = 27;
        public const int SandwichShreddedCheddar = 28;
        public const int SandwichBlueCheese = 29;
        public const int SandwichAvocado = 30;
        public const int SandwichHaystackTopper = 31;
        public const int SandwichFriedEgg = 32;
        public const int SandwichBeefSingle = 33;
        public const int SandwichBeefDouble = 34;
        public const int SandwichGrilledChicken = 35;
        public const int SandwichCrispyChicken = 36;
        public const int SandwichBlackBean = 37;

        public const int SandwichNoMeat = 45;

        public const int SaladSpringMix = 1;
        public const int SaladBBQ = 2;
        public const int SaladRanch = 3;
        public const int SaladChipotleMayo = 4;
        public const int SaladSmashSauce = 5;
        public const int SaladKetchup = 6;
        public const int SaladMustard = 7;
        public const int SaladTruffleMayo = 8;
        public const int SaladMayo = 9;
        public const int SaladBalsamic = 10;
        public const int SaladGuacamole = 11;
        public const int SaladTomatoes = 12;
        public const int SaladRedOnions = 13;
        public const int SaladPickles = 14;
        public const int SaladGrilledOnions = 15;
        public const int SaladGrilledMushrooms = 16;
        public const int SaladBacon = 17;
        public const int SaladJalapenos = 18;
        public const int SaladAmerican = 19;
        public const int SaladPepperJack = 20;
        public const int SaladSwiss = 21;
        public const int SaladCheddar = 22;
        public const int SaladShreddedCheddar = 23;
        public const int SaladBlueCheese = 24;
        public const int SaladAvocado = 25;
        public const int SaladHaystackTopper = 26;
        public const int SaladFriedEgg = 27;
        public const int SaladBeef = 28;
        public const int SaladGrilledChicken = 29;
        public const int SaladCrispyChicken = 30;
        public const int SaladBlackBean = 31;

        public const int ExtraAttribute = 2;
        public const int LightAttribute = 3;
        public const int OnSideAttribute = 4;

        public const string ExtraString = "XT";
        public const string LightString = "LT";
        public const string OnSideString = "onSD";
        public const string NoAttribute = "NO";
        public const string AddAttribute = "ADD";

        public const int NumOfAttributes = 38;

        public static Dictionary<int, string> attributesDictionary = new Dictionary<int, string>
        {
            {SandwichNoBun, "No BUN"},
            {SandwichEggBun, "Egg Bun"},
            {SandwichChipolteBun, "Chipotle Bun" },
            {SandwichMultigrainBun, "Multigrain Bun"},
            {SandwichGlutenFreeBun, "Gluten Free Bun"},
            {SandwichBBQ, "BBQ"},
            {SandwichRanch, "Ranch"},
            {SandwichChipotleMayo, "Chipotle Mayo" },
            {SandwichSmashSauce, "Smash Sauce"},
            {SandwichKetchup, "Ketchup"},
            {SandwichMustard, "Mustard"},
            {SandwichTruffleMayo, "Truffle Mayo"},
            {SandwichMayo, "Mayo"},
            {SandwichBalsamic, "Balsamic Vin"},
            {SandwichGuacamole, "Guacamole"},
            {SandwichLettuce, "Lettuce"},
            {SandwichTomatoes, "Tomatoes"},
            {SandwichRedOnions, "Red Onions"},
            {SandwichPickles, "Pickles"},
            {SandwichGrilledOnions, "Grilled Onions"},
            {SandwichGrilledMushrooms, "Grilled Mush"},
            {SandwichBacon, "Bacon"},
            {SandwichJalapenos, "Jalapenos"},
            {SandwichAmerican, "American"},
            {SandwichPepperJack, "PepperJack"},
            {SandwichSwiss, "Swiss"},
            {SandwichCheddar, "Cheddar"},
            {SandwichShreddedCheddar, "Shredded Cheddar"},
            {SandwichBlueCheese, "Blue Cheese"},
            {SandwichAvocado, "Avocado"},
            {SandwichHaystackTopper, "HaystackTopper"},
            {SandwichFriedEgg, "Fried Egg"},
            {SandwichBeefSingle, "Beef Patty"},
            {SandwichGrilledChicken, "Grilled Chicken" },
            {SandwichCrispyChicken, "Crispy Chicken" },
            {SandwichBlackBean, "Black Bean Patty" },
            {SandwichNoMeat, "NO MEAT" }
        };
    }
}
