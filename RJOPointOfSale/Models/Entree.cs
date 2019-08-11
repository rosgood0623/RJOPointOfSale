using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RJOPointOfSale
{
    public class Entree : MenuItem
    {
        /**
         * TODO: Sandwich has been changed to Entree. -> Add a Salad column to the database.
         * TODO: If Salad, Disable (Enabled Property = false) Attributes in modification form
         * TODO: Disable BUNS buttons 
         * 
         */

        private readonly int[] m_attributes = new int[MenuItemAttributes.NumOfAttributes];
        private readonly int[] m_originalAttributes = new int[MenuItemAttributes.NumOfAttributes];
        private readonly List<string> m_actionModifications = new List<string>();
        public string EntreeIdentifier { get; private set; }


        public override int CalculatePrice()
        {
            PriceCalculator pc = new PriceCalculator(this);
            pc.Calculate();
            return 0;
        }

        public void RetrieveAttributesFromDb(string a_productName)
        {
            try
            {
                conn = new DatabaseConnection(Properties.Settings.Default.ElephantSQLConnection)
                {
                    Query = @"SELECT * FROM public.tbl_signatures_sandwich WHERE tbl_signatures_sandwich.name = :signatureName"
                };

                List<PostgresDataSet> data_set = new List<PostgresDataSet>();
                data_set = conn.QuerySignatureRetrieval(a_productName);
                numOfRetrievedRows = data_set.Count;

                if(numOfRetrievedRows > 0)
                {
                    FillAttributes(data_set[0]);
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        public void FillAttributes(PostgresDataSet a_postgresDataSet)
        {
            m_attributes[MenuItemAttributes.Initialized] = hasAttributeFlag;
            EntreeIdentifier = a_postgresDataSet.GetAtIndex(1);
            for(int i = MenuItemAttributes.SandwichNoBun; i < MenuItemAttributes.SandwichBeefSingle; i++)
            {
                if(a_postgresDataSet.GetAtIndex(i) == "True")
                {
                    m_attributes[i-1] = hasAttributeFlag;
                    m_originalAttributes[i - 1] = hasAttributeFlag;
                }               
            }
        }

        public int[] DecomposeAttributes()
        {
            return m_attributes;
        }

        public int[] DecomposeOriginalAttributes()
        {
            return m_originalAttributes;
        }

        public void SetProteinType(string a_type)
        {
            switch (a_type)
            {
                case "Single":
                    m_attributes[MenuItemAttributes.SandwichBeefSingle] = hasAttributeFlag;
                    EntreeIdentifier += " Single";
                    break;
                case "Double":
                    m_attributes[MenuItemAttributes.SandwichBeefDouble] = hasAttributeFlag;
                    EntreeIdentifier += " Double";
                    break;
                case "Grilled":
                    m_attributes[MenuItemAttributes.SandwichGrilledChicken] = hasAttributeFlag;
                    EntreeIdentifier += " Grilled";
                    break;
                case "Crispy":
                    m_attributes[MenuItemAttributes.SandwichCrispyChicken] = hasAttributeFlag;
                    EntreeIdentifier += " Crispy";
                    break;
                case "Black Bean":
                    m_attributes[MenuItemAttributes.SandwichBlackBean] = hasAttributeFlag;
                    EntreeIdentifier += " BB";
                    break;
                default:
                    break;
            }
        }

        public int GetOriginalAttrAtIndex(int a_index)
        {
            return m_originalAttributes[a_index];
        }

        public void AddActionMod(string a_action)
        {
            if (m_actionModifications.Contains(a_action))
            {
                m_actionModifications.Remove(a_action);
                return;
            }

            m_actionModifications.Add(a_action);
        }

        public bool IsEmptyActionMods()
        {
            return m_actionModifications.Count == 0;
        }

        public int ActionModsCount()
        {
            return m_actionModifications.Count;
        }

        public string GetActionModAtIndex(int a_index)
        {
            return m_actionModifications.ElementAt(a_index);
        }

    }
}
