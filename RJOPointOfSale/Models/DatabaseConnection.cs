using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace RJOPointOfSale
{
    public class DatabaseConnection
    {
        private string m_sqlString;
        private string m_strCon;
        private int m_employeeLogin;
        NpgsqlDataReader m_sqlReader;

        /// <summary>
        /// The default constructor for the DatabaseConnection object. Initializes the 
        /// connection string on creation.
        /// </summary>
        /// <remarks>
        /// NAME: DatabaseConnection
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_connString">The connection string that contains the credentials of the database.</param>
        public DatabaseConnection(string a_connString)
        {
            m_strCon = a_connString;
        }/*public DatabaseConnection(string a_connString)*/
        public string Query
        {
            set
            {
                m_sqlString = value;
            }
        }

        public int SetLogin
        {
            set
            {
                m_employeeLogin = value;
            }
        }

        /// <summary>
        /// The method used to query the database for the employee information. 
        /// </summary>
        /// <remarks>
        /// NAME: QueryEmployeeIdentification
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// A list of PostgresDataSet, which contains all the information captured by the database
        /// </returns>
        public List<PostgresDataSet> QueryEmployeeIdentification()
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "enteredCode";
                param.Value = m_employeeLogin;
                param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;

                cmd.Parameters.Add(param);

                m_sqlReader = cmd.ExecuteReader();

                for (int i = 0; m_sqlReader.Read(); i++)
                {
                    PostgresDataSet postgresData = new PostgresDataSet();
                    for (int j = 0; j < m_sqlReader.FieldCount; j++)
                    {
                        postgresData.AddData(m_sqlReader[j].ToString());
                    }
                    retrievedData.Add(postgresData);

                }
            }
            return retrievedData;
        }/*public List<PostgresDataSet> QueryEmployeeIdentification()*/

        /// <summary>
        /// This method is used to query the database for retrieval of entree items.
        /// </summary>
        /// <remarks>
        /// NAME: QuerySignatureRetrieval
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_signature">The string to query the database with.</param>
        /// <returns>
        /// A list of PostgresDataSet, which contains all the information captured from the database.
        /// </returns>
        public List<PostgresDataSet> QuerySignatureRetrieval(string a_signature)
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "signatureName";
                param.Value = a_signature;
                param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;

                cmd.Parameters.Add(param);

                m_sqlReader = cmd.ExecuteReader();

                for (int i = 0; m_sqlReader.Read(); i++)
                {
                    PostgresDataSet postgresData = new PostgresDataSet();
                    for (int j = 0; j < m_sqlReader.FieldCount; j++)
                    {
                        postgresData.AddData(m_sqlReader[j].ToString());
                    }
                    retrievedData.Add(postgresData);

                }
            }
            return retrievedData;
        }/* public List<PostgresDataSet> QuerySignatureRetrieval(string a_signature)*/

        /// <summary>
        /// This method is used to query the database for data on a specified side menu item.
        /// </summary>
        /// <remarks>
        /// NAME: QuerySideRetrieval
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <param name="a_side">The specified side stored in a string.</param>
        /// <returns>
        /// A list of PostgresDataSet objects, which contains the information retrieved from the database.
        /// </returns>
        public List<PostgresDataSet> QuerySideRetrieval(string a_side)
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "sideName";
                param.Value = a_side;
                param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;

                cmd.Parameters.Add(param);

                m_sqlReader = cmd.ExecuteReader();

                for (int i = 0; m_sqlReader.Read(); i++)
                {
                    PostgresDataSet postgresData = new PostgresDataSet();
                    for (int j = 0; j < m_sqlReader.FieldCount; j++)
                    {
                        postgresData.AddData(m_sqlReader[j].ToString());
                    }
                    retrievedData.Add(postgresData);

                }
            }
            return retrievedData;
        }/*public List<PostgresDataSet> QuerySideRetrieval(string a_side)*/

        /// <summary>
        /// This method queries the database for a specified beverage menu item.
        /// </summary>
        /// <remarks>
        /// NAME: QueryBeverageRetrieval
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/14/2019
        /// </remarks>
        /// <returns>
        /// <param name="a_beverage">The specified beverage.</param>
        /// </returns>
        public List<PostgresDataSet> QueryBeverageRetrieval(string a_beverage)
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "beverageName";
                param.Value = a_beverage;
                param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;

                cmd.Parameters.Add(param);

                m_sqlReader = cmd.ExecuteReader();

                for (int i = 0; m_sqlReader.Read(); i++)
                {
                    PostgresDataSet postgresData = new PostgresDataSet();
                    for (int j = 0; j < m_sqlReader.FieldCount; j++)
                    {
                        postgresData.AddData(m_sqlReader[j].ToString());
                    }
                    retrievedData.Add(postgresData);

                }
            }
            return retrievedData;
        }/* public List<PostgresDataSet> QueryBeverageRetrieval(string a_beverage)*/
    }
}

