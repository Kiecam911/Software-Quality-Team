using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TMSv2_Contracts
{
    /// 
    /// \class Contract
    ///
    /// \brief The purpose of this class is to model a contract within the system.
    /// \details <b>Details</b>
    ///
    /// This class contains the attributes and methods necessary to perform transactions and utilize
    /// contracts within the system. A <b>Contract</b> is instantiated upon retrieving contract data from the
    /// contract marketplace, and the data members of the contract are filled in according to the data received.
    /// 
    /// \var data member ContractID <i>int</i> - <i>private<i> int containing unique identifier for the customer
    /// \var data member associatedCustomerID <i>int</i> - <i>private<i> int containing a unique identifier for the contract
    /// \var data member clientName <i>string</i> - <i>private<i> string to hold the client_name value from the contract marketplace
    /// \var data member jobType <i>int</i> - <i>private<i> int to hold the job_type value from the contract marketplace
    /// \var data member quantity <i>int</i> - <i>private<i> int to hold the Quantity value from the contract marketplace
    /// \var data member origin <i>string</i> - <i>private<i> string to hold the Origin value from the contract marketplace
    /// \var data member destination <i>string</i> - <i>private<i> string to hold the Destination value from the contract marketplace
    /// \var data member vantype <i>int</i> - <i>private<i> integer to hold the van_type value from the contract marketplace
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public class Contract
    {
        // data members
        private int ContractID                      /// integer containing a unique identifier for the contract
        {
            get;
        }
        private int associatedCustomerID;           /// integer containing the ID of the customer who posted the contract

        private string clientName;                  /// string to hold the client_name value from the contract marketplace
        private int jobType;                        /// integer to hold the job_type value from the contract marketplace
        private int quantity;                       /// integer to hold the Quantity value from the contract marketplace
        private string origin;                      /// string to hold the Origin value from the contract marketplace
        private string destination;                 /// string to hold the Destination value from the contract marketplace
        private int vantype;                        /// integer to hold the van_type value from the contract marketplace

        // methods
        ///
        /// \brief To instantiate a new Contract
        /// \details <b>Details</b>
        ///
        /// Instantiates a Contract object
        ///
        /// \param <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Contract class, nothing is returned
        ///
        public Contract()
        {

        }



        ///
        /// \brief To retrieve details for the new contract from the contract marketplace
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the contract marketplace database to populate the variables
        /// related to the contract marketplace. 
        ///
        /// \param <b>void</b> - None
        ///
        /// \return Nothing
        ///
        public string InitializeContract()
        {
            string cmQuery = @"SELECT * FROM Contract";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["contractMarketplace"].ConnectionString;


            SqlDataAdapter customerDA = new SqlDataAdapter();
            DataSet customerDS = new DataSet();

            using (SqlConnection nwindConn = new SqlConnection(connectionString))
            {
                if (nwindConn.State != ConnectionState.Open)
                {
                    nwindConn.Open();
                }
                SqlCommand selectCMD = new SqlCommand(cmQuery, nwindConn);
                customerDA.SelectCommand = selectCMD;

                customerDA.Fill(customerDS, "Customers");
                nwindConn.Close();
            }

            //try
            //{
            //    SqlConnection cmConnection = new SqlConnection(connectionString);
            //    SqlCommand cmCommand = new SqlCommand(cmQuery, cmConnection);

            //    cmConnection.Open();
            //    SqlDataReader cmData = cmCommand.ExecuteReader();

            //    if(cmData.HasRows)
            //    {
            //        return cmData.GetString(0);
            //    }
            //    else
            //    {
            //        return "no data";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}

            return "";
        }



        ///
        /// \brief Sets values to link contract with its respective client
        /// \details <b>Details</b>
        ///
        /// This method sets the id references for the Customer class and Contract class to match each other.
        /// The contract's ID will be included in the respective customer's list of contract IDs, and the 
        /// Contract's associatedCustomerID will reflect its owner. If the customer does not exist,
        /// a new one will be registered.
        ///
        /// \param <b>void</b> - None
        ///
        /// \return Nothing
        ///
        public void LinkToCustomer()
        {
            throw new Exception("Invalid customer");
        }
    }
}
