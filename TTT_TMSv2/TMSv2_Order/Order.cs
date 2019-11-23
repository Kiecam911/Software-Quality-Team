using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Order
{
    /// 
    /// \class Order
    ///
    /// \brief The purpose of this class is to contain the data associated with an order.
    /// \details <b>Details</b>
    ///
    /// This class represents the base order with all the characteristics that it should contain as well as data checking for private data members
    ///
    /// \var data member orderId <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    /// \var data member cities <i>List string</i> - <i>private<i> data member that holds the order's cities
    /// \var data member totalCost <i>double</i> - <i>private<i> data member that holds the order's total cost
    /// \var data member usesFTL <i>bool</i> - <i>public<i> data member that 
    /// \var data member isCompleted <i>bool</i> - <i>public<i> data member that holds the order's completion state (completed or not)
    /// \var data member requiresReefer <i>bool</i> - <i>public<i> data member that
    ///
    /// \author <i>TeamTeamTeam</i>
    ///
    public class Order
    {
        // Private Data members
        private int orderID;                                /// Order's Identification number
        private double totalCost;
        private List<string> cities;                        /// Order's Cities
                                  /// Total cost for the order
        // Public Data members
        public bool usesFTL { get; set; }                   /// 
        public bool isCompleted { get; set; }               /// Completion state of the order
        public bool requiresReefer { get; set; }            /// 

        ///
        /// \fn Order()
        /// 
        /// \brief To instantiate a new Order object
        /// \details <b>Details</b>
        ///
        /// Instantiates the Order's data members to -1, null, or false as default placeholders for data
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Order class, nothing is returned
        ///
        public Order()
        {
            orderID = 0;
            cities = null;
            totalCost = 0;
            usesFTL = false;
            isCompleted = false;
            requiresReefer = false;
        }

        //Setters & Getters///////////////////////////
        ///
        /// \fn setOrderID(int newID)
        /// 
        /// \brief Sets the OrderID
        /// \details <b>Details</b>
        ///
        /// Checks if newID is greater than or equal to 0 and sets OrderID to newID
        ///
        /// \param newID <b>int</b> - The new OrderID
        ///
        /// \return Nothing is returned
        ///
        public void setOrderID(int newID)
        {
            if (newID >= 0)
            {
                orderID = newID;
            }
            else orderID = 0;
        }

        ///
        /// \fn setTotalCost(double newCost)
        /// 
        /// \brief Sets the totalCost
        /// \details <b>Details</b>
        ///
        /// Checks if newCost is greater than or equal to 0 and sets totalCost to newCost
        ///
        /// \param newCost <b>double</b> - The new totalCost
        ///
        /// \return Nothing is returned
        ///
        public void setTotalCost(double newCost)
        {
            if (totalCost >= 0)
            {
                totalCost = newCost;
            }
            else totalCost = 0;
        }

        ///
        /// \fn getOrderID()
        /// 
        /// \brief Gets OrderID
        /// \details <b>Details</b>
        ///
        /// Returns the OrderID
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this method
        ///
        /// \return Returns an int (the OrderID)
        ///
        public int getOrderID()
        {
            return orderID;
        }

        ///
        /// \fn getTotalCost()
        /// 
        /// \brief Gets totalCost
        /// \details <b>Details</b>
        ///
        /// Returns the totalCost
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this method
        ///
        /// \return Returns an int (the totalCost)
        ///
        public double getTotalCost()
        {
            return totalCost;
        }
        //////////////////////////////////////////////
    }
}
