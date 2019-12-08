using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Contracts
{
    /// 
    /// \class Customer
    ///
    /// \brief The purpose of this class is to model basic info about each customer within the system.
    /// \details <b>Details</b>
    ///
    /// This class contains the attributes and methods related to a customer. Customers serve to be the owner of
    /// their respective contracts, and contain info related to each customer that has ever had orders taken from
    /// in the system. <b>Customer</b> contains methods to assign contracts as well as get all associated contracts.
    /// 
    /// \var data member associatedContacts <i>List<int></i> - <i>private<i> list containing ContractID of all associated contracts
    /// \var data member CustomerID <i>int</i> - <i>private<i> int containing unique identifier for the customer
    /// \var data member CustomerName <i>string</i> - <i>private<i> strnig containing the name of the customer
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public class Customer
    {
        // data members
        private int _CustomerID;
        public int CustomerID
        {
            get { return _CustomerID; }
            set
            {
                if(value >= 0)
                {
                    _CustomerID = value;
                }
            }
        }
        public string CustomerName { get; set; }
        public List<Contract> _AssociatedContracts { get; set; }


        ///
        /// \brief Assigns a contract to a customer
        /// \details <b>Details</b>
        ///
        /// 
        /// This method takes the contractID parameter and assigns
        /// it to the customer denoted by the customerID parameter.
        ///
        /// \param <b>void</b> - None
        ///
        /// \return List<int> containing the contract IDs
        ///
        public void AssignContract(int contractID, int customerID)
        {
            throw new Exception("Invalid contract");
        }
    }
}
