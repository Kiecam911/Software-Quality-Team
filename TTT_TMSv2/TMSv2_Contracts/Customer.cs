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
        private List<int> associatedContracts;
        private string customerName;
        private int customerID;

        ///
        /// \brief Sets customer name
        /// \details <b>Details</b>
        ///
        /// Sets customer name to new value
        /// 
        /// \param Customer
        ///
        /// \return Creates/edits customers
        ///
        public void setCustomerName(string newName)
        {
            if (newName != "")
            {
                customerName = newName;
            }
            else customerName = "none";
        }

        ///
        /// \brief Sets customer ID
        /// \details <b>Details</b>
        ///
        /// Sets customerID to new value
        /// 
        /// \param Customer
        ///
        /// \return Creates/edits customers
        ///
        public void setCustomerID(int newID)
        {
            if (newID >= 0)
            {
                customerID = newID;
            }
            else customerID = 0;
        }

        public string getCxName()
        {
            return customerName;
        }

        public int getCxID()
        {
            return customerID;
        }

        ///
        /// \brief Returns all associated contracts
        /// \details <b>Details</b>
        ///
        /// 
        /// This method returns a list of integers corresponding to the IDs of 
        /// all contracts that are owned by the respective customer
        ///
        /// \param <b>void</b> - None
        ///
        /// \return List<int> containing the contract IDs
        ///
        public List<int> GetAssociatedContracts()
        {
            return null;
        }



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