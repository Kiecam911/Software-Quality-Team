using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;




namespace TMSv2_DAL
{
    public static class DataAccess
    {
        public static DataSet GetContracts()
        {
            // initialize query and connection string
            string cmQuery = @"SELECT * FROM Contract";
            string connectionString = ConfigurationManager.ConnectionStrings["contractMarketplace"].ConnectionString;

            // create mysql objects to use
            MySqlDataAdapter customerDA = new MySqlDataAdapter();
            DataSet customerDS = new DataSet();

            // connect with DB and retrieve dataset containing contracts
            using (MySqlConnection cmConnection = new MySqlConnection(connectionString))
            {
                if (cmConnection.State != ConnectionState.Open)
                {
                    cmConnection.Open();
                }
                MySqlCommand selectCMD = new MySqlCommand(cmQuery, cmConnection);
                customerDA.SelectCommand = selectCMD;

                customerDA.Fill(customerDS, "Contract");
                cmConnection.Close();
                return customerDS;
            }
        }
    }
}
