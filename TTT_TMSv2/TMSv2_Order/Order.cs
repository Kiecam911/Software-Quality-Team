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

        private double _FinalCustomerPrice;                               /// Order's final price for customer
        public double FinalCustomerPrice                               
        {
            get { return _FinalCustomerPrice; }
            set
            {
                if (value >= 0)
                {
                    _FinalCustomerPrice = value;
                }
            }
        }

        private double _FinalCarrierPrice;                               /// Order's final price for the company
        public double FinalCarrierPrice
        {
            get { return _FinalCarrierPrice; }
            set
            {
                if (value >= 0)
                {
                    _FinalCarrierPrice = value;
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
        public bool IsActive { get; set; }                  /// The Active status
        public bool IsMerged { get; set; }                  /// The Merged status

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



        public Order(Contract c)
        {
            _OrderID = 0;
            OrderContract = c;
            Cities = null;
            Trips = null;
            _TotalKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;
        }

        public void CalculateTotalCost(bool isFTL)
        {
            double carrierRatePerKM = 0;
            double customerRatePerKM = 0;
            if (isFTL)
            {
                // to do: get rate of selected carrier from planner
                carrierRatePerKM = 4.985;
                customerRatePerKM = carrierRatePerKM * 1.08;
            }
            else
            {
                carrierRatePerKM = 0.2995 * OrderContract.Quantity;
                customerRatePerKM = carrierRatePerKM * 1.05;
            }

            //use fomrula to calculate costs
            int totalKM = Trips[0].TotalDistanceKm;
            double totalDriveHours = Trips[0].TotalDrivingHours.TotalHours;
            double totalWorkHours = Trips[0].TotalDistanceHours.TotalHours;
            int extraDays = GetTripDaysFromHours(totalDriveHours, totalWorkHours);

            FinalCustomerPrice = customerRatePerKM * totalKM + 150 * extraDays;
            FinalCarrierPrice = carrierRatePerKM * totalKM + 150 * extraDays;
        }



        private int GetTripDaysFromHours(double drivingHours, double workingHours)
        {
           // get amount of days taken as an integer rounded down (since only days after day 1 cost extra)
           int days1 = (int)(workingHours /= 12);
           int days2 = (int)(drivingHours /= 8);

            // return the greater of the 2
            if (days1 >= days2)
            {
                return days1;
            }
            return days2;
        }



        ///
        /// \fn CreateOrderFromContract()
        /// 
        /// \brief To instantiate an order based on a contract
        /// \details <b>Details</b>
        ///
        /// Instantiates the Order's data members related to contracts to create a new order
        /// from the  corresponding contract to be carried out.
        ///
        /// \param contract <b>Contract</b> - The contract that the order is based on
        ///
        /// \return nothing <i>void</i> this method returns void
        ///
        public void CreateOrderFromContract(Contract contract)
        {
            //Order newOrder = new Order();
            //newOrder.OrderID = contract.ContractID;
            //newOrder.IsCompleted = false;
            //newOrder.IsActive = false;
        }



        public void AddCitiesToOrder(List<string> newCities)
        {
            Cities = newCities;
        }



        public void AddTripsToOrder(List<Trip> newTrips)
        {
            Trips = newTrips;
        }



        public void CalculateTripTotals()
        {
            //TotalKm = 0;
            //HoursTaken = TimeSpan.FromHours(0);

            //foreach (Trip t in Trips)
            //{
            //    TotalKm += t.TotalDistanceKm;
            //    HoursTaken += t.TotalDistanceHours;
            //}
        }
    }
}
