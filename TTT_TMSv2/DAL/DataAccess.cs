using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;
using TMSv2_Logging;
using System.Security;

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

            string query = @"SELECT C.CarrierID, C.CarrierName, CI.CarrierInfoID, CI.DestinationCity, CI.FTLAvailability, CI.LTLAvailability, CI.FTLRate, CI.LTLRate, CI.ReefCharge
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
            adapter.Fill(data, "Carriers");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        ///
        /// \fn UpdateCarriers(int cID, string cName, int ciID, string cDestCity, int FTLA, int LTLA, double FTLRate, double LTLRate, double reefCharge)
        /// 
        /// \brief Updates the Carriers and CarrierInfo table
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database and updates the data in the Carrier and CarrierInfo
        /// tables according to the data passed by the parameters
        ///
        /// \param cID <b>int</b> - The CarrierID
        /// \param cName <b>string</b> - The CarrierName
        /// \param ciID <b>int</b> - The CarrierInfoID
        /// \param cDestCity <b>string</b> - The CarrierInfo's DestinationCity
        /// \param FTLA <b>int</b> - The CarrierInfo's FTLAvailability
        /// \param LTLA <b>int</b> - The CarrierInfo's FTLAvailability
        /// \param FTLRate <b>double</b> - The CarrierInfo's FTLRate
        /// \param LTLRate <b>double</b> - The CarrierInfo's LTLRate
        /// \param reefCharge <b>double</b> - The CarrierInfo's reefCharge
        ///
        /// \return bool Returns Success or Failure
        /// 
        /// \sa Carrier
        ///
        public bool UpdateCarriers(int cID, string cName, int ciID, string cDestCity, int FTLA, int LTLA, double FTLRate, double LTLRate, double reefCharge)
        {
            //Connect to variabled database
            ConnectToDatabase();

            //Create query string
            string query = @"UPDATE CarrierLine CL
                                    INNER JOIN
                                Carriers C ON C.CarrierID = CL.CarrierID
                                    INNER JOIN
                                CarrierInfo CI ON CI.CarrierInfoID = CL.CarrierInfoID 
                            SET 
                                C.CarrierName = @CarrierName,
                                CI.DestinationCity = @DestinationCity,
                                CI.FTLAvailability = @FTLAvailability,
                                CI.LTLAvailability = @LTLAvailability,
                                CI.FTLRate = @FTLRate,
                                CI.LTLRate = @LTLRate,
                                CI.ReefCharge = @ReefCharge WHERE C.CarrierID = @CarrierID AND CI.CarrierInfoID = @CarrierInfoID; ";

            //Create command
            var command = new MySqlCommand(query, _Connection);

            //Load command with parameters
            command.Parameters.AddWithValue("@CarrierID", cID);
            command.Parameters.AddWithValue("@CarrierName", cName);
            command.Parameters.AddWithValue("@CarrierInfoID", ciID);
            command.Parameters.AddWithValue("@DestinationCity", cDestCity);
            command.Parameters.AddWithValue("@FTLAvailability", FTLA);
            command.Parameters.AddWithValue("@LTLAvailability", LTLA);
            command.Parameters.AddWithValue("@FTLRate", FTLRate);
            command.Parameters.AddWithValue("@LTLRate", LTLRate);
            command.Parameters.AddWithValue("@ReefCharge", reefCharge);

            //Check if the command executed Properly; close and return failure if not
            if (0 == command.ExecuteNonQuery())
            {
                CloseConnection();
                return false;
            }

            //Close and return success
            CloseConnection();
            return true;
        }

        ///
        /// \fn AddDepotToCarriers(int cID, string cDestCity, int FTLA, int LTLA, double FTLRate, double LTLRate, double reefCharge)
        /// 
        /// \brief Adds Depot to the CarrierInfo Table and the associates it with the designated Carrier in CArrierLine
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database and add the data from the depot to the carrierInfo Table
        /// with the appropriate CarrierID in CarrierLine
        ///
        /// \param cID <b>int</b> - The CarrierID for the CarrierLine Table
        /// \param cDestCity <b>string</b> - The CarrierInfo's DestinationCity
        /// \param FTLA <b>int</b> - The CarrierInfo's FTLAvailability
        /// \param LTLA <b>int</b> - The CarrierInfo's FTLAvailability
        /// \param FTLRate <b>double</b> - The CarrierInfo's FTLRate
        /// \param LTLRate <b>double</b> - The CarrierInfo's LTLRate
        /// \param reefCharge <b>double</b> - The CarrierInfo's reefCharge
        ///
        /// \return bool Returns Success or Failure
        /// 
        /// \sa Carrier
        ///
        public bool AddDepotToCarriers(int cID, string cDestCity, int FTLA, int LTLA, double FTLRate, double LTLRate, double reefCharge)
        {
            //Connect to variabled database
            ConnectToDatabase();

            //Create command
            var command = new MySqlCommand();
            MySqlTransaction myTrans;

            //Begin the transaction
            myTrans = _Connection.BeginTransaction();

            //Connect the connection and the transaction to the command
            command.Connection = _Connection;
            command.Transaction = myTrans;

            try
            {
                //Create command from command text
                command.CommandText = @"INSERT INTO CarrierInfo(DestinationCity, FTLAvailability, LTLAvailability, FTLRate, LTLRate, ReefCharge) VALUES
                                        (@DestCity, @FTLA, @LTLA, @FTLR, @LTLR, @reefCharge);";

                //Load with appropriate parameters
                command.Parameters.AddWithValue("@DestCity", cDestCity);
                command.Parameters.AddWithValue("@FTLA", FTLA);
                command.Parameters.AddWithValue("@LTLA", LTLA);
                command.Parameters.AddWithValue("@FTLR", FTLRate);
                command.Parameters.AddWithValue("@LTLR", LTLRate);
                command.Parameters.AddWithValue("@reefCharge", reefCharge);

                //Execute the command
                command.ExecuteNonQuery();

                //Create command from command text
                command.CommandText = @"INSERT INTO CarrierLine(CarrierID, CarrierInfoID) VALUES
                                        (@CarrierID, (SELECT CarrierInfoID FROM CarrierInfo WHERE DestinationCity = @DestinationCity AND FTLAvailability = @FTLAv AND LTLAvailability = @LTLAv AND FTLRate = @FTLRa AND LTLRate = @LTLRa AND ReefCharge = @reeferCharge));";

                //Load command with parameters
                command.Parameters.AddWithValue("@CarrierID", cID);
                command.Parameters.AddWithValue("@DestinationCity", cDestCity);
                command.Parameters.AddWithValue("@FTLAv", FTLA);
                command.Parameters.AddWithValue("@LTLAv", LTLA);
                command.Parameters.AddWithValue("@FTLRa", FTLRate);
                command.Parameters.AddWithValue("@LTLRa", LTLRate);
                command.Parameters.AddWithValue("@reeferCharge", reefCharge);

                //Execute command
                command.ExecuteNonQuery();

                //Do final commit to run commands
                myTrans.Commit();

                //For Propserity, Log success
                Logger.LogToFile("Both records are written to database.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {
                        Logger.LogToFile("An exception of type " + ex.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }
                }

                Logger.LogToFile("An exception of type " + e.GetType() +
                " was encountered while inserting the data.");
                Logger.LogToFile("Neither record was written to database.");

                //Close the connection and return failure
                CloseConnection();
                return false;
            }
            finally
            {
                //Close connection
                CloseConnection();
            }

            //Return Success
            return true;
        }

        ///
        /// \fn DeleteFromCarriers(int ciID)
        /// 
        /// \brief Deletes a Depot from the database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database and deletes the depot information from the carrierInfo table
        /// and deletes from the carrierInfo and CarrierLine based on the carrierInfoID
        ///
        /// \param ciID <b>int</b> - The CarrierInfoID from the depot class
        ///
        /// \return bool Returns Success or Failure
        /// 
        /// \sa Carrier
        ///
        public bool DeleteFromCarriers(int ciID)
        {
            //Connect to variabled database
            ConnectToDatabase();

            //Create query string
            string query = @"DELETE FROM CarrierLine WHERE CarrierInfoID = @CarrierInfoID; ";

            //Create command
            var command = new MySqlCommand(query, _Connection);

            //Load command with parameters
            command.Parameters.AddWithValue("@CarrierInfoID", ciID);

            //Check if the command executed Properly; close and return failure if not
            if (0 == command.ExecuteNonQuery())
            {
                CloseConnection();
                return false;
            }

            //Load the new command
            command.CommandText = @"DELETE FROM CarrierInfo WHERE CarrierInfoID = @ciID; ";

            //Load command with parameters
            command.Parameters.AddWithValue("@ciID", ciID);

            //Check if the command executed Properly; close and return false if not
            if (0 == command.ExecuteNonQuery())
            {
                CloseConnection();
                return false;
            }

            //Close and return success
            CloseConnection();
            return true;
        }

        ///
        /// \fn GetRouteTable()
        /// 
        /// \brief Gets the Data from the routes table in  the database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database and extracts the data from the Routes table in the
        /// database
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet Returns the DataSet of the information extracted from the Database's Routes Table
        /// 
        /// \sa Carrier
        ///
        public DataSet GetRouteTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT ID, City, DistanceKm, DistanceHours, WestDestination, EastDestination FROM Routes; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Routes");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        public bool UpdateRoutes(int ID, string city, int KM, TimeSpan hrs, string westDest, string eastDest)
        {
            //Connect to variabled database
            ConnectToDatabase();

            //Create query string
            string query = @"UPDATE Routes
                            SET 
                                ID = @ID,
                                City = @City,
                                DistanceKm = @km,
                                DistanceHours = @Hours,
                                WestDestination = @wDest,
                                EastDestination = @eDest WHERE ID = @routeID; ";

            //Create command
            var command = new MySqlCommand(query, _Connection);

            //Load command with parameters
            command.Parameters.AddWithValue("@ID", ID);
            command.Parameters.AddWithValue("@City", city);
            command.Parameters.AddWithValue("@km", KM);
            command.Parameters.AddWithValue("@Hours", hrs);
            command.Parameters.AddWithValue("@wDest", westDest);
            command.Parameters.AddWithValue("@eDest", eastDest);
            command.Parameters.AddWithValue("@routeID", ID);

            //Check if the command executed Properly; close and return failure if not
            if (0 == command.ExecuteNonQuery())
            {
                CloseConnection();
                return false;
            }

            //Close and return success
            CloseConnection();
            return true;
        }


        public DataSet GetRatesTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            //Create query string
            string query = @"SELECT FTLRate, LTLRate FROM OSHTRates; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Rates");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        public bool UpdateRatesTable(double FTLRate, double LTLRate)
        {
            //Connect to variabled database
            ConnectToDatabase();

            //Create query string
            string query = @"UPDATE OSHTRates
                            SET 
                                FTLRate = @FTLR,
                                LTLRate = @LTLR  WHERE ID = 1; ";

            //Create command
            var command = new MySqlCommand(query, _Connection);

            //Load command with parameters
            command.Parameters.AddWithValue("@FTLR", FTLRate);
            command.Parameters.AddWithValue("@LTLR", LTLRate);


            //Check if the command executed Properly; close and return failure if not
            if (0 == command.ExecuteNonQuery())
            {
                CloseConnection();
                return false;
            }

            //Close and return success
            CloseConnection();
            return true;
        }


        public bool FullDatabaseBackup()
        {
            //Create a file stream for each table in the database
            DataSet data = new DataSet();

            try
            {
                //Get Data From the Route Table
                data = GetRouteTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName3"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Order Table
                data = GetOrdersTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName1"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Users Table
                data = GetUsersTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName2"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the OSHTRates Table
                data = GetRatesTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName4"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Trips Table
                data = GetTripsTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName5"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Carriers Table
                data = GetCarriersTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName6"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the CarrierLine Table
                data = GetCarrierLineTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName7"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the CarrierInfo Table
                data = GetCarrierInfoTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName8"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Contracts Table
                data = GetContractsTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName9"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Invoices Table
                data = GetInvoicesTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName10"]));

                //Clear previous dataset
                data = new DataSet();
                //Get Data From the Invoices Table
                data = GetCustomersTable();
                //Write Data To file to save it
                data.WriteXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName11"]));
            }
            catch(SecurityException e)
            {
                //Log information about the failure to the log file and return false failure
                Logger.LogToFile("There was an error in backup the database of type " + e.GetType() + ". From the source: " + e.Source + " with the message '" + e.Message + "'.");

                return false;
            }

            return true;
        }


        private DataSet GetUsersTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT UserID, Password, Permission FROM Users; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Users");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetOrdersTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT OrderID, ContractID, Cities, HoursTaken, TotalKm, TotalCost, Completed, IsActive, IsMerged FROM Orders; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Orders");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetTripsTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT TripID, CarrierID, OrderID, Origin, Destination, TotalKm, HoursTaken, Completed FROM Trips; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Trips");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetCarriersTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT CarrierID, CarrierName FROM Carriers; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Carriers");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetCarrierInfoTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT CarrierInfoID, DestinationCity, FTLAvailability, LTLAvailability, FTLRate, LTLRate, reefCharge FROM CarrierInfo; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "CarrierInfo");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetCarrierLineTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT CarrierID, CarrierInfoID FROM CarrierLine; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "CarrierLine");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetContractsTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT ContractID, Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type FROM Contracts; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Contracts");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetInvoicesTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT InvoiceID, OrderID, TotalCost FROM Invoices; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Invoices");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }

        private DataSet GetCustomersTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT CustomerID, ContractID, CustomerName FROM Customers; ";

            //Connect to variabled database
            ConnectToDatabase();

            //Create and use command from query string and connection
            MySqlCommand command = new MySqlCommand(query, _Connection);
            adapter.SelectCommand = command;

            //Fill dataset
            adapter.Fill(data, "Customers");

            //Close connection
            CloseConnection();

            //Return dataset
            return data;
        }





    }
}
