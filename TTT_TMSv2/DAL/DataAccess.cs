﻿using System;
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
using System.Windows.Forms;
using System.Xml;

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
        /// \fn GetContracts()
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
        /// \fn ConnectToDatabase()
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
        /// \fn CloseConnection()
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


        ///
        /// \fn GetCarrierData()
        /// 
        /// \brief Get All Relevant Data to the Carrier from the Database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get all relevant <b>Carrier</b> Data (To the <b>Carrier</b> Class)
        /// and stores the data in a dataset to be returned and unpacked by the <b>Carrier</b> class
        ///
        /// \param none <b>void</b> - None
        ///
        /// \return DataSet - Returns the information for the <b>Carrier</b> class
        /// 
        ///
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
        /// \brief Update all Relevant database tables with New Carrier Data
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to update all relevant tables within the database that are related to the <b>Carrier</b>
        /// class using the specified function parameters as the information to update the database with
        ///
        /// \param cID <b>int</b> - Carrier's ID
        /// \param cName <b>string</b> - Carrier's name
        /// \param ciID <b>int</b> - Carrier's Depot ID (The Database's CarrierInfoID)
        /// \param cDestCity <b>string</b> - Carrier's City
        /// \param FTLA <b>int</b> - Carrier's Full Truck Load (FTL) Availability
        /// \param LTLA <b>int</b> - Carrier's Less Than Load (LTL) Availability
        /// \param FTLRate <b>double</b> - Carrier's Full Truck Load (FTL) Rate
        /// \param LTLRate <b>double</b> - Carrier's Less Than Load (LTL) Rate
        /// \param reefCharge <b>double</b> - Carrier's Refrigirated Cargo charge
        ///
        /// \return bool - Returns the success or failure status
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
        /// \brief Add new depot data to relevant database tables
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to add new depot data to relevant tables within the database that are related 
        /// to the <b>Carrier</b> class's Depots using the specified function parameters as the information to update the database with
        ///
        /// \param cID <b>int</b> - Carrier's ID
        /// \param cDestCity <b>string</b> - Carrier's City
        /// \param FTLA <b>int</b> - Carrier's Full Truck Load (FTL) Availability
        /// \param LTLA <b>int</b> - Carrier's Less Than Load (LTL) Availability
        /// \param FTLRate <b>double</b> - Carrier's Full Truck Load (FTL) Rate
        /// \param LTLRate <b>double</b> - Carrier's Less Than Load (LTL) Rate
        /// \param reefCharge <b>double</b> - Carrier's Refrigirated Cargo charge
        ///
        /// \return bool - Returns the success or failure status
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
        /// \brief Add new depot data to relevant database tables
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to add new depot data to relevant tables within the database that are related 
        /// to the <b>Carrier</b> class's Depots using the specified function parameters as the information to update the database with
        ///
        /// \param ciID <b>int</b> - Carrier's Depot ID (The Database's CarrierInfoID)
        ///
        /// \return bool - Returns the success or failure status
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
        /// \brief Get All Relevant Data to the Routes class from the Database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get all relevant <b>Routes</b> Data (To the <b>Routes</b> Class)
        /// and stores the data in a dataset to be returned and unpacked by the <b>Routes</b> class
        ///
        /// \param none <b>void</b> - None
        ///
        /// \return DataSet - Returns the information for the <b>Routes</b> class
        /// 
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

        ///
        /// \fn GetRatesTable()
        /// 
        /// \brief Get All Relevant Data to the OSHTRates class from the Database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get all relevant <b>OSHTRates</b> Data (To the <b>OSHTRates</b> Class)
        /// and stores the data in a dataset to be returned and unpacked by the <b>OSHTRates</b> class
        ///
        /// \param none <b>void</b> - None
        ///
        /// \return DataSet - Returns the information for the <b>OSHTRates</b> class
        /// 
        ///
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

        ///
        /// \fn UpdateRatesTable(double FTLRate, double LTLRate)
        /// 
        /// \brief Updates the Rates Table based on information inputed via the parameters
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to update the Rates table with new values
        ///
        /// \param FTLRate <b>double</b> - The OSHT's new additional FTL markup rate
        /// \param LTLRate <b>double</b> - The OSHT's new additional LTL markup rate
        ///
        /// \return Bool - Returns the success or failure result
        ///
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

        ///
        /// \fn FullDatabaseBackup()
        /// 
        /// \brief Gets all data from the database and saves them into xml files
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get all current data and save it to xml files for later restoration
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return Bool - Returns the success or failure result
        ///
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
            catch (SecurityException e)
            {
                //Log information about the failure to the log file and return false failure
                Logger.LogToFile("There was an error in backup the database of type " + e.GetType() + ". From the source: " + e.Source + " with the message '" + e.Message + "'.");

                return false;
            }

            return true;
        }

        ///
        /// \fn FullDatabaseRestore()
        /// 
        /// \brief Gets all data from the stored xml files and inserts them into the database (Database must be empty)
        /// \details <b>Details</b>
        ///
        /// This method takes all data from the saved database xml files and restores them to the empty database
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return Bool - Returns the success or failure result
        ///
        public bool FullDatabaseRestore()
        {
            //Create a file stream for each table in the database
            DataSet data = new DataSet();

            try
            {
                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName1"]));
                //Update Database With Stored Data
                if (!RestoreOrdersTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName2"]));
                if (!RestoreUsersTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName3"]));
                if (!RestoreRoutesTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName4"]));
                if (!RestoreRatesTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName5"]));
                if (!RestoreTripsTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName6"]));
                if (!RestoreCarrierTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName8"]));
                if (!RestoreCarrierInfoTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName7"]));
                if (!RestoreCarrierLineTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName9"]));
                if (!RestoreContractTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName10"]));
                if (!RestoreInvoiceTable(data))
                {
                    return false;
                }

                //Clear previous dataset
                data = new DataSet();
                //Write Data To file to save it
                data.ReadXml((ConfigurationManager.AppSettings["DatabaseBackupDirectory"] + ConfigurationManager.AppSettings["DBBackupFileName11"]));
                if (!RestoreCustomerTable(data))
                {
                    return false;
                }
            }
            catch (SecurityException e)
            {
                //Log information about the failure to the log file and return false failure
                Logger.LogToFile("There was an error in backup the database of type " + e.GetType() + ". From the source: " + e.Source + " with the message '" + e.Message + "'.");

                return false;
            }

            return true;
        }

        ///
        /// \fn GetUsersTable()
        /// 
        /// \brief Gets the user table information and returns it in a dataset
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get user information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Users Table in the database
        ///
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

        ///
        /// \fn RestoreUsersTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreUsersTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Users(UserID, Password, Permission) VALUES
                            (@ID, @password, @permission); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@ID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@password", row.Field<string>(1));
                    command.Parameters.AddWithValue("@permission", XmlConvert.ToChar(row.Field<string>(2)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    //Close and return success
                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn GetOrdersTable()
        /// 
        /// \brief Gets all data in the Database's Orders table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get Orders information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Trips Table in the database
        ///
        private DataSet GetOrdersTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT OrderID, ContractID, Cities, HoursTaken, DaysRequired, TotalKm, TotalIncome, TotalExpense, Completed, IsActive, IsMerged, hasTrip FROM Orders; ";

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

        ///
        /// \fn RestoreOrdersTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreOrdersTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Orders(OrderID, ContractID, Cities, HoursTaken, DaysRequired TotalKm, TotalIncome, TotalExpense, Completed, IsActive, IsMerged, hasTrip) VALUES
                            (@ID, @cID, @cities, @hrs, @days @km, @income, @expense, @completed, @active, @merged, @hasTrip); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@ID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@cID", XmlConvert.ToInt32(row.Field<string>(1)));
                    command.Parameters.AddWithValue("@cities", row.Field<string>(2));
                    command.Parameters.AddWithValue("@hrs", XmlConvert.ToTimeSpan(row.Field<string>(3)));
                    command.Parameters.AddWithValue("@days", XmlConvert.ToInt32(row.Field<string>(4)));
                    command.Parameters.AddWithValue("@km", XmlConvert.ToInt32(row.Field<string>(5)));
                    command.Parameters.AddWithValue("@income", XmlConvert.ToDouble(row.Field<string>(6)));
                    command.Parameters.AddWithValue("@expense", XmlConvert.ToDouble(row.Field<string>(7)));
                    command.Parameters.AddWithValue("@completed", XmlConvert.ToBoolean(row.Field<string>(8)));
                    command.Parameters.AddWithValue("@active", XmlConvert.ToBoolean(row.Field<string>(9)));
                    command.Parameters.AddWithValue("@merged", XmlConvert.ToBoolean(row.Field<string>(10)));
                    command.Parameters.AddWithValue("@hasTrip", XmlConvert.ToBoolean(row.Field<string>(11)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    //Close and return success
                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreOrdersTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreTripsTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Trips(TripID, CarrierID, OrderID, Origin, Destination, TotalKm, HoursTaken, Completed) VALUES
                            (@ID, @cID, @oID, @Origin, @dest, @Km, @hrs, @completed); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@ID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@cID", XmlConvert.ToInt32(row.Field<string>(1)));
                    command.Parameters.AddWithValue("@oID", XmlConvert.ToInt32(row.Field<string>(2)));
                    command.Parameters.AddWithValue("@Origin", row.Field<string>(3));
                    command.Parameters.AddWithValue("@dest", row.Field<string>(4));
                    command.Parameters.AddWithValue("@Km", XmlConvert.ToInt32(row.Field<string>(5)));
                    command.Parameters.AddWithValue("@hrs", XmlConvert.ToTimeSpan(row.Field<string>(6)));
                    command.Parameters.AddWithValue("@completed", XmlConvert.ToBoolean(row.Field<string>(7)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    //Close and return success
                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreRoutesTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreRoutesTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Routes(City, DistanceKm, DistanceHours, WestDestination, EastDestination) VALUES
                            (@City, @DistanceKm, @DistanceHours, @WestDest, @EastDest); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    //command.Parameters.AddWithValue("@ID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@City", row.Field<string>(1));
                    command.Parameters.AddWithValue("@DistanceKm", XmlConvert.ToInt32(row.Field<string>(2)));
                    command.Parameters.AddWithValue("@DistanceHours", XmlConvert.ToTimeSpan(row.Field<string>(3)));
                    command.Parameters.AddWithValue("@WestDest", row.Field<string>(4));
                    command.Parameters.AddWithValue("@EastDest", row.Field<string>(5));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreRatesTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreRatesTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO OSHTRates(FTLRate, LTLRate) VALUES
                            (@FTLRate, @LTLRate); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@FTLRate", XmlConvert.ToDouble(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@LTLRate", XmlConvert.ToDouble(row.Field<string>(1)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreCarrierTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreCarrierTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Carriers(CarrierID, CarrierName) VALUES
                            (@CarrierID, @CarrierName); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@CarrierID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@CarrierName", row.Field<string>(1));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreCarrierInfoTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreCarrierInfoTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO CarrierInfo(CarrierID, CarrierInfoID, DestinationCity, FTLAvailability, LTLAvailability, FTLRate, LTLRate, reefCharge) VALUES
                            (@CarrierID, @CID, @DestCity, @FTLA, @LTLA, @FTLRate, @LTLRate, @reef); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@CarrierID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@CID", XmlConvert.ToInt32(row.Field<string>(1)));
                    command.Parameters.AddWithValue("@DestCity", row.Field<string>(2));
                    command.Parameters.AddWithValue("@FTLA", XmlConvert.ToInt32(row.Field<string>(3)));
                    command.Parameters.AddWithValue("@LTLA", XmlConvert.ToInt32(row.Field<string>(4)));
                    command.Parameters.AddWithValue("@FTLRate", XmlConvert.ToDouble(row.Field<string>(5)));
                    command.Parameters.AddWithValue("@LTLRate", XmlConvert.ToDouble(row.Field<string>(6)));
                    command.Parameters.AddWithValue("@reef", XmlConvert.ToDouble(row.Field<string>(7)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreCarrierLineTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreCarrierLineTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO CarrierLine(CarrierID, CarrierInfoID) VALUES
                            (@CID, @CIID); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@CID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@CIID", XmlConvert.ToInt32(row.Field<string>(1)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreContractTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreContractTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Contracts(ContractID, Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type) VALUES
                            (@CID, @name, @JType, @Quantity, @Origin, @Dest, @VType); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@CID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@name", row.Field<string>(1));
                    command.Parameters.AddWithValue("@JType", XmlConvert.ToInt32(row.Field<string>(2)));
                    command.Parameters.AddWithValue("@Quantity", XmlConvert.ToInt32(row.Field<string>(3)));
                    command.Parameters.AddWithValue("@Origin", row.Field<string>(4));
                    command.Parameters.AddWithValue("@Dest", row.Field<string>(5));
                    command.Parameters.AddWithValue("@VType", XmlConvert.ToInt32(row.Field<string>(6)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreInvoiceTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreInvoiceTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Invoices(InvoiceID, OrderID, TotalCost) VALUES
                            (@ID, @OID, @TotalCost); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@ID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@OID", XmlConvert.ToInt32(row.Field<string>(1)));
                    command.Parameters.AddWithValue("@TotalCost", XmlConvert.ToDouble(row.Field<string>(2)));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn RestoreCustomerTable(DataSet data)
        /// 
        /// \brief Meant to store all data from the saved xml file
        /// \details <b>Details</b>
        ///
        /// This method extracts all data from the saved database xml file and inserts that data into the database
        ///
        /// \param data <b>Dataset</b> - The dataset that will be restored to the database
        ///
        /// \return Bool - Returns the success or failure result
        ///
        private bool RestoreCustomerTable(DataSet data)
        {
            //Variables
            if (data.Tables.Count != 0)
            {
                DataTable dataTable = data.Tables[0];
                DataRowCollection dataRows = dataTable.Rows;

                foreach (DataRow row in dataRows)
                {
                    //Create query string
                    string query = @"INSERT INTO Customers(CustomerID, ContractID, CustomerName) VALUES
                            (@CustID, @ContID, @name); ";

                    //Connect to variabled database
                    ConnectToDatabase();

                    //Create command
                    var command = new MySqlCommand(query, _Connection);

                    //Load command with parameters
                    command.Parameters.AddWithValue("@CustID", XmlConvert.ToInt32(row.Field<string>(0)));
                    command.Parameters.AddWithValue("@ContID", XmlConvert.ToInt32(row.Field<string>(1)));
                    command.Parameters.AddWithValue("@name", row.Field<string>(2));

                    //Check if the command executed Properly; close and return failure if not
                    if (0 == command.ExecuteNonQuery())
                    {
                        CloseConnection();
                        return false;
                    }

                    CloseConnection();
                }
            }

            return true;
        }

        ///
        /// \fn GetTripsTable()
        /// 
        /// \brief Gets all data in the Database's Trips table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get Trips information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Trips Table in the database
        ///
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

        ///
        /// \fn GetCarriersTable()
        /// 
        /// \brief Gets all data in the Database's Carriers table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get Carriers information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Carriers Table in the database
        ///
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

        ///
        /// \fn GetCarrierInfoTable()
        /// 
        /// \brief Gets all data in the Database's CarrierInfo table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get CarrierInfo information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the CarrierInfo Table in the database
        ///
        private DataSet GetCarrierInfoTable()
        {
            //Variables
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet data = new DataSet();

            string query = @"SELECT CarrierID, CarrierInfoID, DestinationCity, FTLAvailability, LTLAvailability, FTLRate, LTLRate, reefCharge FROM CarrierInfo; ";

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

        ///
        /// \fn GetCarrierLineTable()
        /// 
        /// \brief Gets all data in the Database's CarrierLine table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get CarrierLine information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the CarrierLine Table in the database
        ///
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

        ///
        /// \fn GetContractsTable()
        /// 
        /// \brief Gets all data in the Database's Contracts table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get Contracts information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Contracts Table in the database
        ///
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

        ///
        /// \fn GetInvoicesTable()
        /// 
        /// \brief Gets all data in the Database's Invoices table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get Invoices information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Invoices Table in the database
        ///
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

        ///
        /// \fn GetCustomersTable()
        /// 
        /// \brief Gets all data in the Database's Customers table and saves it to the xml file
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get Customers information and returns it in a dataset for
        /// further data extraction in the calling function
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the stored data from the Customers Table in the database
        ///
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

        ///
        /// \fn GetActiveOrders()
        /// 
        /// \brief Gets all active orders from the database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get all orders in the database with the IsActive boolean
        /// set to true
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the dataset of all active orders
        ///
        public static DataSet GetActiveOrders()
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));
                string sqlCommand = "SELECT OrderID, Contracts.ContractID, TotalKm, HoursTaken FROM Orders INNER JOIN Contracts WHERE Orders.IsActive = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Orders");
                
                connection.Close();
                return ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        ///
        /// \fn GetUnassignedTripOrders()
        /// 
        /// \brief Gets all Trips that do not have an order
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get all Trips that do not have an order
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return DataSet - Returns the dataset of all Trips that do not have an order
        ///
        public static DataSet GetUnassignedTripOrders()
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));
                string sqlCommand = "SELECT OrderID, TotalKm, HoursTaken FROM Orders INNER JOIN Trips WHERE Trips.OrderID = NULL";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "*");

                connection.Close();
                return ds;
            }
            catch
            {
                return null;
            }
        }

        ///
        /// \fn InsertNewContract(string clientName, int jobType, int quantity, string origin, string destination, int vanType)
        /// 
        /// \brief Inserts a new trip into the database based on function parameters
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to insert a new contract with the function's parameters
        ///
        /// \param clientname <b>string</b> - The Client's name
        /// \param jobType <b>string</b> - The Contract's jobType
        /// \param quantity <b>string</b> - The Contract's order quantity
        /// \param origin <b>string</b> - The Contract's origin city
        /// \param destination <b>string</b> - The Contract's destination city
        /// \param vanType <b>string</b> - The Contract's vanType (reefer or no)
        ///
        /// \return int - Returns the contractID of the newly inserted Contract
        ///
        public int InsertNewContract(string clientName, int jobType, int quantity, string origin, string destination, int vanType)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet id = new DataSet();

            int contractID = 0;

            string cmd1 = String.Format(@"INSERT INTO Contracts (Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type)
                            VALUES (""{0}"", {1}, {2}, ""{3}"", ""{4}"", {5});", clientName, jobType, quantity, origin, destination, vanType);
            string cmd2 = @"SELECT LAST_INSERT_ID();";

            //Connect to variabled database
            ConnectToDatabase();

            // execute commands
            MySqlCommand command = new MySqlCommand(cmd1, _Connection);
            int result = command.ExecuteNonQuery();
            command.CommandText = cmd2;
            ulong temp = (ulong)command.ExecuteScalar();
            contractID = (int)temp;

            //Close connection
            CloseConnection();

            return contractID;
        }

        ///
        /// \fn InsertNewOrder(int contractID)
        /// 
        /// \brief Inserts a new Order based on the ContractID
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to insert a new order with the associated ContractID
        ///
        /// \param contractID <b>int</b> - The Client's ID
        ///
        /// \return void - Returns nothing
        ///
        public void InsertNewOrder(int contractID)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string cmdText = String.Format(@"INSERT INTO Orders (ContractID, IsActive) VALUES ({0}, true);", contractID);

            //Connect to variabled database
            ConnectToDatabase();

            // execute commands
            MySqlCommand command = new MySqlCommand(cmdText, _Connection);
            int result = command.ExecuteNonQuery();
            MessageBox.Show("Order added");
            //Close connection
            CloseConnection();
        }

        ///
        /// \fn UpdateOrderTotals(int orderID, int daysRequired, int totalKM, double totalIncome, double totalExpense)
        /// 
        /// \brief Updates the Order (based on orderID) with the function's parameters
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to update the specified order (based on the functin parameter OrderID) with the
        /// associated function's parameters
        ///
        /// \param orderID <b>int</b> - The Order's ID
        /// \param daysRequired <b>int</b> - The Order's new daysRequired
        /// \param totalKM <b>int</b> - The Order's new totalKM
        /// \param totalIncome <b>int</b> - The Order's new totalIncome
        /// \param totalExpense <b>int</b> - The Order's totalExpense
        ///
        /// \return void - Returns nothing
        ///
        public void UpdateOrderTotals(int orderID, int daysRequired, int totalKM, double totalIncome, double totalExpense)
        {
            string cmdText = String.Format(@"UPDATE Orders SET DaysRequired = {0}, TotalKm = {1}, TotalIncome = {2}, TotalExpense = {3},  hasTrip = true WHERE OrderID = {4};", daysRequired, totalKM, totalIncome, totalExpense, orderID);

            //Connect to variabled database
            ConnectToDatabase();

            // execute commands
            MySqlCommand command = new MySqlCommand(cmdText, _Connection);
            int result = command.ExecuteNonQuery();
            //Close connection
            CloseConnection();
        }

        ///
        /// \fn GetOrderByID(int orderID)
        /// 
        /// \brief Gets the order from the database based on the OrderID
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get the requrested Order based on the OrderID in the function's parameters
        ///
        /// \param orderID <b>int</b> - The Order's ID
        ///
        /// \return DataRow - Returns data for the requested Order
        ///
        public DataRow GetOrderByID(int orderID)
        {
            try
            {
                string sqlCommand = String.Format(@"SELECT * FROM Orders WHERE OrderID = {0};", orderID);


                ConnectToDatabase();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, _Connection);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                CloseConnection();
                return ds.Tables[0].Rows[0];
            }
            catch(Exception ex)
            {
                Logger.LogToFile(ex.Message);
                return null;
            }
        }

        ///
        /// \fn GetTripByID(int tripID)
        /// 
        /// \brief Gets the Trip based on the TripID in the function's parameters
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get the Trip from the Trip ID based on the function's parameter
        ///
        /// \param tripID <b>int</b> - The Trip's ID
        ///
        /// \return DataRow - Returns data for the requested Trip
        ///
        public DataRow GetTripByID(int tripID)
        {
            try
            {
                string sqlCommand = String.Format(@"SELECT * FROM Trips WHERE TripID = {0};", tripID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, _Connection);


                ConnectToDatabase();

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                CloseConnection();
                return ds.Tables[0].Rows[0];
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message);
                return null;
            }
        }

        ///
        /// \fn GetCarrierInfoByID(int carrierID)
        /// 
        /// \brief Gets the Carrier based on the CarrierID in the function's parameters
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get the Carrier from the Carrier ID based on the function's parameter
        ///
        /// \param carrierID <b>int</b> - The Carrier's ID
        ///
        /// \return DataRow - Returns data for the requested Carrier
        ///
        public DataRow GetCarrierInfoByID(int carrierID)
        {
            try
            {
                string sqlCommand = String.Format(@"SELECT * FROM CarrierInfo WHERE CarrierID = {0};", carrierID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, _Connection);


                ConnectToDatabase();

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                CloseConnection();
                return ds.Tables[0].Rows[0];
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message);
                return null;
            }
        }

        ///
        /// \fn GetContractByID(int contractID)
        /// 
        /// \brief Gets the Contracts based on the ContractID in the function's parameters
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to get the Contracts from the Contracts ID based on the function's parameter
        ///
        /// \param contractID <b>int</b> - The Contracts's ID
        ///
        /// \return DataRow - Returns data for the requested Contract
        ///
        public DataRow GetContractByID(int contractID)
        {
            try
            {
                string sqlCommand = String.Format(@"SELECT * FROM Contracts WHERE ContractID = {0};", contractID);


                ConnectToDatabase();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, _Connection);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                CloseConnection();
                return ds.Tables[0].Rows[0];
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message);
                return null;
            }
        }

        ///
        /// \fn DayPassed()
        /// 
        /// \brief Increments the DaysPassed in the Orders data based
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the Team-made database to increment the DaysPassed in the Orders table by one day
        ///
        /// \param none <b>nothing</b> - none
        ///
        /// \return Void - Returns nothing
        ///
        public void DayPassed()
        {
            string cmdText = @"UPDATE Orders SET DaysRequired = DaysRequired - 1 WHERE Orders.hasTrip = 1;";

            //Connect to variabled database
            ConnectToDatabase();

            // execute commands
            MySqlCommand command = new MySqlCommand(cmdText, _Connection);
            int result = command.ExecuteNonQuery();
            //Close connection
            CloseConnection();
        }
    }
}
