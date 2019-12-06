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
    /// 
    /// \class DataAccess
    ///
    /// \brief The purpose of this class is to model a contract within the system.
    /// \details <b>Details</b>
    ///
    /// This class contains the methods to connect to the databases that are used in this project
    /// 
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public class DataAccess
    {
        //Data member
        private MySqlConnection _Connection = null;
        public MySqlConnection Connection
        {
            //Return connection stream to database
            get { return _Connection; }
        }

        private static DataAccess _instance = null;
        public static DataAccess Instance()
        {
            //If this instance is null then create a new one and return it
            if (_instance == null)
                _instance = new DataAccess();
            return _instance;
        }

        ///
        /// \brief To retrieve the contracts from the Contract Marketplace
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the contract marketplace database and returns the dataset
        ///
        /// \param <b>void</b> - None
        ///
        /// \return DataSet Returns the contracts in the contract marketplace
        ///
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

        public bool ConnectToDatabase()
        {
            //Create connection from compound connection string with config data
            _Connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));

            try
            {
                _Connection.Open();
            }
            catch (MySqlException sqlE)
            {
                return false;
            }
            catch (InvalidOperationException opE)
            {
                return false;
            }

            return true;
        }

        public void CloseConnection()
        {
            _Connection.Close();
        }
    }
}
