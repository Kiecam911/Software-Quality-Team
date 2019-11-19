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
    /// \data member orderId <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    /// \data member cities <i>List string</i> - <i>private<i> data member that holds the order's cities
    /// \data member totalCost <i>double</i> - <i>private<i> data member that holds the order's total cost
    /// \data member usesFTL <i>bool</i> - <i>public<i> data member that 
    /// \data member isCompleted <i>bool</i> - <i>public<i> data member that holds the order's completion state (completed or not)
    /// \data member requiresReefer <i>bool</i> - <i>public<i> data member that
    ///
    /// \author <i>TeamTeamTeam</i>
    ///
    public class Order
    {
        // Private Data members
        private int orderID;                                /// Order's Identification number
        private List<string> cities;                        /// Order's Cities
        private double totalCost;                           /// Total cost for the order
        // Public Data members
        public bool usesFTL { get; set; }                   /// 
        public bool isCompleted { get; set; }               /// Completion state of the order
        public bool requiresReefer { get; set; }            /// 

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
        /// \see Order(int ordID, string city, double cost)
        ///
        public Order()
        {
            orderID = -1;
            cities = null;
            totalCost = -1.0;
            usesFTL = false;
            isCompleted = false;
            requiresReefer = false;
        }

        public Order(int ordID, string city, double cost)
        {
            
        }

    }
}
