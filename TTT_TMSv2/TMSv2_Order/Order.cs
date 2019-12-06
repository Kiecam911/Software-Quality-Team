using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Contracts;
using TMSv2_TripPlanner;

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
    /// \var data member _OrderID <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    /// \var data member Cities <i>List string</i> - <i>public<i> data member that holds the cities that the order is associated with
    /// \var data member Trips <i>List Trip</i> - <i>public<i> data member that holds the trips that the Order has, must, or will undertake
    /// \var data member OrderContract <i>Contract</i> - <i>public<i> data member that holds the order's contract
    /// \var data member _TotalKm <i>int</i> - <i>private<i> data member that records the calculated total distance that must be traveled
    /// \var data member _HoursTaken <i>TimeSpan</i> - <i>private<i> data member that records the elapsed time for the order
    /// \var data member IsCompleted <i>bool</i> - <i>public<i> data member that holds the order's completion state (completed or not)
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Contract
    /// \sa Trip
    ///
    public class Order
    {
        // Private Data members
        private int _OrderID;                               /// Order's Identification number
        public int OrderID                                  /// Public reference to _OrderID for safety
        {
            get { return _OrderID; }
            set
            {
                if(value >= 0)
                {
                    _OrderID = value;
                }
            }
        }
        public Contract OrderContract { get; set; }
        public List<string> Cities { get; set; }            /// List of Cities the Order is associated with
        public List<Trip> Trips { get; set; }               /// The list of trips the order has, must, or will undertake
        private int _TotalKm;                               /// The calculated total distance that must be traveled
        public int TotalKm                                  /// Public accessor to the private _TotalKm for safety
        {
            get { return _TotalKm; }
            set
            {
                if (value >= 0)
                {
                    _TotalKm = value;
                }
            }
        }
        private TimeSpan _HoursTaken;                       /// The elapsed time for the order
        public TimeSpan HoursTaken                          /// Public accessor to the private _HoursTaken for safety
        {
            get { return _HoursTaken; }
            set
            {
                if(value >= TimeSpan.FromHours(0.0))
                {
                    _HoursTaken = value;
                }
            }
        }
        public bool IsCompleted { get; set; }               /// The Completion status

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
            _OrderID = 0;
            OrderContract = null;
            Cities = null;
            Trips = null;
            _TotalKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;
        }
        
    }
}
