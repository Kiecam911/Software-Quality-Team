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
    /// \var data member _Connection <i>MySqlConnection</i> - <i>private<i> Holds the connection to the variabled database
    /// \var data member _instance <i>DataAccess</i> - <i>private<i> Holds the static instance of the DataAccess class to prevent multiple Connections to the same database
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

        ///
        /// \brief Open a Connection to the variabled database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database and uses variabled data from the config file
        /// that can be altered by the <b>Admin</b>
        ///
        /// \param <b>void</b> - None
        ///
        /// \return Bool Returns A success or failure to connect boolean (success=true;failure=false)
        /// 
        /// \sa AdminPage
        /// \sa Admin
        ///
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

        ///
        /// \brief Closes an open Connection to the variabled database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database and closes the open connection
        ///
        /// \param <b>void</b> - None
        ///
        /// \return nothing
        /// 
        ///
        public void CloseConnection()
        {
            _Connection.Close();
        }

        public DataSet GetCarrierData()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT C.CarrierID, C.CarrierName, CI.DestinationCity, CI.FTLAvailability, CI.LTLAvailability, CI.FTLRate, CI.LTLRate, CI.ReefCharge
                            FROM
                                CarrierLine CL
                                    INNER JOIN
                                Carriers C ON C.CarrierID = CL.CarrierID
                                    INNER JOIN
                                CarrierInfo CI ON CI.CarrierInfoID = CL.CarrierInfoID; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Contract");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }
    }
}
