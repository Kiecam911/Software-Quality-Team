using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Contracts;
using TMSv2_TripPlanner;
using System.Data;
using TMSv2_DAL;
using TMSv2_Carriers;

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
        // constants
        private const double kFTLMarkup = 1.08;
        private const double kLTLMarkup = 1.05;

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

        private int _DaysRequired;                               /// Order's final price for the company
        public int DaysRequired
        {
            get { return _DaysRequired; }
            set
            {
                if (value >= 0)
                {
                    _DaysRequired = value;
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

            Trips = new List<Trip>();
        }



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
        public Order(Contract c)
        {
            _OrderID = 0;
            OrderContract = c;
            Cities = null;
            Trips = null;
            _TotalKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;

            Trips = new List<Trip>();
        }



        ///
        /// \fn CalculateTotalCost()
        /// 
        /// \brief Calculates the profit and expense of an entire order
        /// \details <b>Details</b>
        ///
        /// Given job type, van type, and quantity, this method calculates how much money
        /// a order costs to carry out for the company, as well as how much the customer should be charged.
        ///
        /// \param int isLTL - 0 for FTL, LTL otherwise
        /// \param int isLTL - 0 for FTL, LTL otherwise
        /// \param int quantity - quantity of pallets being transported for LTL
        ///
        public void CalculateTotalCost(int isLTL, int isReefer, int quantity)
        {
            double carrierRatePerKM = 0;
            double customerRatePerKM = 0;
            Trip currentTrip = Trips[0];
            Depot currentDepotRates = currentTrip.DepotRates;

            if (isLTL == 0)
            {
                // if not LTL, assign FTL rates
                carrierRatePerKM = currentDepotRates.FTLRate;
                customerRatePerKM = carrierRatePerKM * kFTLMarkup;
            }
            else
            {
                // if LTL, assign LTL rates
                carrierRatePerKM = currentDepotRates.LTLRate * quantity;
                customerRatePerKM = carrierRatePerKM * kLTLMarkup;
            }
            if (isReefer != 0)
            {
                //if truck is reefer, apply reefer multipler
                carrierRatePerKM *= (1 + currentTrip.DepotRates.ReefCharge);
                customerRatePerKM *= (1 + currentTrip.DepotRates.ReefCharge);
            }

            //use fomrula to calculate costs
            TotalKm = Trips[0].TotalDistanceKm;
            double totalDriveHours = Trips[0].TotalDrivingHours.TotalHours;
            double totalWorkHours = Trips[0].TotalDistanceHours.TotalHours;
            int extraDays = GetTripDaysFromHours(totalDriveHours, totalWorkHours);
            DaysRequired = extraDays + 1;

            // assign final costs to data members
            FinalCustomerPrice = customerRatePerKM * TotalKm + 150 * extraDays;
            FinalCarrierPrice = carrierRatePerKM * TotalKm + 150 * extraDays;

            // update order in DB
            DataAccess dal = new DataAccess();
            dal.UpdateOrderTotals(OrderID, DaysRequired, TotalKm, FinalCustomerPrice, FinalCarrierPrice);
        }



        ///
        /// \fn GetTripDaysFromHours()
        /// 
        /// \brief Calculates the amount of extra days taken by a trip
        /// \details <b>Details</b>
        ///
        /// Returns the amount of extra days a trip needs to complete over 1
        ///
        /// \return int - integer representing the amount of extra days needed
        ///
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
        /// \fn AddCitiesToOrder()
        /// 
        /// \brief To add a list of cities to an order
        /// \details <b>Details</b>
        ///
        /// Assigns the newCities parameter to the Cities data member
        ///
        /// \param List<string> newCities - the list of cities
        ///
        public void AddCitiesToOrder(List<string> newCities)
        {
            Cities = newCities;
        }


        ///
        /// \fn AddTripsToOrder()
        /// 
        /// \brief To add a list of trips to an order
        /// \details <b>Details</b>
        ///
        /// Assigns the newTrips parameter to the Trips data member
        ///
        /// \param List<string> newCities - the list of cities
        ///
        public void AddTripsToOrder(List<Trip> newTrips)
        {
            Trips = newTrips;
        }



        ///
        /// \fn GetActiveOrders()
        /// 
        /// \brief Gets the list of active orders
        /// \details <b>Details</b>
        ///
        /// Returns the last of active orders obtained from the DAL
        ///
        /// \return List<Order> - the last of active orders
        ///
        public List<Order> GetActiveOrders()
        {
            // create objects to hold information from DAL
            DataTable routesTable = TMSv2_DAL.DataAccess.GetActiveOrders().Tables[0];
            List<Order> routesList = new List<Order>();
            Order tempOrder = null;

            DataRowCollection routeRows = routesTable.Rows;

            // loop through each row of data, creating contract and assigning values to it
            foreach (DataRow currentRow in routeRows)
            {
                //Instantiate a new Destination
                tempOrder = new Order();

                //Fill destination
                tempOrder.OrderID = currentRow.Field<int>(0);
                tempOrder.TotalKm = currentRow.Field<int>(1);
                tempOrder.HoursTaken = currentRow.Field<TimeSpan>(2);
                tempOrder.FinalCarrierPrice = currentRow.Field<int>(3);
               

                //Add to list
                routesList.Add(tempOrder);
            }
            return routesList;
        }


        ///
        /// \fn GetUnassignedTripOrders()
        /// 
        /// \brief Gets the list of unassigned orders
        /// \details <b>Details</b>
        ///
        /// Returns the last of unassigned orders obtained from the DAL
        ///
        /// \return List<Order> - the last of unassigned orders
        ///
        public List<Order> GetUnassignedTripOrders()
        {
            // create objects to hold information from DAL
            DataTable routesTable = TMSv2_DAL.DataAccess.GetUnassignedTripOrders().Tables[0];
            List<Order> routesList = new List<Order>();
            Order tempOrder = null;

            DataRowCollection routeRows = routesTable.Rows;

            // loop through each row of data, creating contract and assigning values to it
            foreach (DataRow currentRow in routeRows)
            {
                //Instantiate a new Destination
                tempOrder = new Order();

                //Fill destination
                tempOrder.OrderID = currentRow.Field<int>(0);
                tempOrder.TotalKm = currentRow.Field<int>(1);
                tempOrder.HoursTaken = currentRow.Field<TimeSpan>(2);
                tempOrder.FinalCarrierPrice = currentRow.Field<int>(3);


                //Add to list
                routesList.Add(tempOrder);
            }
            return routesList;
        }
    }
}
