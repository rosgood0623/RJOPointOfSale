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
        
        public DatabaseConnection(string a_connString)
        {
            m_strCon = a_connString;
        }

        public string Query
        {
            set
            {
                m_sqlString = value;
            }
        }

        public string ConnectionString
        {
            set
            {
                m_strCon = value;
            }
        }

        public int SetLogin
        {
            set
            {
                m_employeeLogin = value;
            }
        }
        
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
        }

        public List<PostgresDataSet> QuerySignatureRetrieval(string signature)
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "signatureName";
                param.Value = signature;
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
        }

        public List<PostgresDataSet> QuerySideRetrieval(string side)
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "sideName";
                param.Value = side;
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
        }

        public List<PostgresDataSet> QueryBeverageRetrieval(string side)
        {
            List<PostgresDataSet> retrievedData = new List<PostgresDataSet>();

            using (NpgsqlConnection con = new NpgsqlConnection(m_strCon))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(m_sqlString, con);

                var param = cmd.CreateParameter();
                param.ParameterName = "beverageName";
                param.Value = side;
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
        }


    }
}

