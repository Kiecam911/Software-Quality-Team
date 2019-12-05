using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using TMSv2_DAL;

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
    /// \var data member _ContractID <i>int</i> - <i>private<i> int containing unique identifier for the customer
    /// \var data member ClientName <i>string</i> - <i>public<i> string to hold the client_name value from the contract marketplace
    /// \var data member JobType <i>int</i> - <i>public<i> int to hold the job_type value from the contract marketplace
    /// \var data member Quantity <i>int</i> - <i>public<i> int to hold the Quantity value from the contract marketplace
    /// \var data member Origin <i>string</i> - <i>public<i> string to hold the Origin value from the contract marketplace
    /// \var data member Destination <i>string</i> - <i>public<i> string to hold the Destination value from the contract marketplace
    /// \var data member Vantype <i>int</i> - <i>public<i> integer to hold the van_type value from the contract marketplace
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// 
    ///
    public class Contract
    {
        // data members
        private int _ContractID;                                /// The identification number for the contract
        public int ContractID                                   /// The public accessor for the _ContractID
        {
            get { return _ContractID; }
            set
            {
                if(value >= 0)
                {
                    _ContractID = value;
                }
            }
        }
        public string ClientName;                               /// string to hold the client_name value from the contract marketplace
        public int JobType { get; set; }                        /// integer to hold the job_type value from the contract marketplace
        public int Quantity { get; set; }                       /// integer to hold the Quantity value from the contract marketplace
        public string Origin { get; set; }                      /// string to hold the Origin value from the contract marketplace
        public string Destination { get; set; }                 /// string to hold the Destination value from the contract marketplace
        public int VanType { get; set; }                        /// integer to hold the van_type value from the contract marketplace

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
