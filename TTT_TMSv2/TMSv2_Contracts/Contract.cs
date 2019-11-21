using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public class Contract
    {
        // data members
        int contractID                      /// integer containing a unique identifier for the contract
        {
            get;
        }
        int associatedCustomerID;           /// integer containing the ID of the customer who posted the contract

        string clientName;                  /// string to hold the client_name value from the contract marketplace
        int jobType;                        /// integer to hold the job_type value from the contract marketplace
        int quantity;                       /// integer to hold the Quantity value from the contract marketplace
        string origin;                      /// string to hold the Origin value from the contract marketplace
        string destination;                 /// string to hold the Destination value from the contract marketplace
        int vantype;                        /// integer to hold the van_type value from the contract marketplace

        // methods
        ///
        /// \brief To instantiate a new Contract
        /// \details <b>Details</b>
        ///
        /// Instantiates a Contract object
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
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
        /// \param nothing <b>void</b> - None
        ///
        /// \return Nothing
        ///
        public void InitializeContract()
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
        /// \param nothing <b>void</b> - None
        ///
        /// \return Nothing
        ///
        public void LinkToCustomer()
        {

        }
    }
}
