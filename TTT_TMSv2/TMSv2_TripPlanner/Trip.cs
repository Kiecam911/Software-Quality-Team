using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Carriers;
using System.Text.RegularExpressions;
using TMSv2_Logging;

namespace TMSv2_TripPlanner
{
    /// 
    /// \class Trip
    ///
    /// \brief The purpose of this class is to 
    /// 
    /// \details <b>Details</b>
    ///
    /// This class represents the Trips that are assigned to the <b>Carrier</b> for each <b>Order</b>
    /// 
    /// \var data member _TripID <i>int</i> - <i>private<i> the identification number of the <b>Trip</b>
    /// \var data member TripCarrier <i>Carrier</i> - <i>public<i> the carrier for the <b>Trip</b>
    /// \var data member Origin <i>string</i> - <i>private<i> the city of origin that the <b>Contract</b> starts at
    /// \var data member Destination <i>string</i> - <i>private<i> the Destination city that the <b>Contract</b> ends at
    /// \var data member _DaysTaken <i>int</i> - <i>private<i> the elasped time take for the <b>Trip</b> to complete
    /// \var data member IsCompleted <i>bool</i> - <i>public<i> The completion status of the <b>Trip</b>
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    ///
    public class Trip
    {
        // constants
        private const int kGoingWest = 0;
        private const int kGoingEast = 1;

        // Private Data members
        private int _TripID;                                /// The Identification number for the trip
        public int TripID                                   /// The public accessor for the _TripID for safety
        {
            get { return _TripID; }
            set
            {
                if(value >= 0)
                {
                    _TripID = value;
                }
            }
        }
        public Carrier TripCarrier { get; set; }            /// The Carrier that will carryout the trip
        public Depot DepotRates { get; set; }                   /// depot containing the carrier's pricing rates
        public Routes OriginPointer { get; set; }                  /// The city of origin of the Contract
        public Routes DestinationPointer { get; set; }             /// The destination city of the Contract
        public string Origin { get; set; }                  /// The city of origin of the Contract
        public string Destination { get; set; }             /// The destination city of the Contract
        private int _TotalDistanceKm;                            /// The calculated total distance that must be traveled
        public int TotalDistanceKm                               /// Public accessor to the private _TotalKm for safety
        {
            get { return _TotalDistanceKm; }
            set
            {
                if (value >= 0)
                {
                    _TotalDistanceKm = value;
                }
            }
        }
        public TimeSpan TotalDrivingHours;

        private TimeSpan _TotalDistanceHours;
        public TimeSpan TotalDistanceHours                               /// Public accessor to the private for safety
        {
            get { return _TotalDistanceHours; }
            set
            {
                if (value >= TimeSpan.FromHours(0.0))
                {
                    _TotalDistanceHours = value;
                }
            }
        }

        private TimeSpan _HoursTaken;                       /// The elapsed time for the order
        public TimeSpan HoursTaken                          /// Public accessor to the private _HoursTaken for safety
        {
            get { return _HoursTaken; }
            set
            {
                if (value >= TimeSpan.FromHours(0.0))
                {
                    _HoursTaken = value;
                }
            }
        }
        public bool IsCompleted { get; set; }               /// The completion status of the trip

        ///
        /// \fn Trip()
        /// 
        /// \brief To instantiate a new Trip
        /// \details <b>Details</b>
        ///
        /// Instantiates the Trip class by setting the endDestination and kmDistance
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Trip class, nothing is returned
        ///
        public Trip()
        {
            _TripID = 0;
            TripCarrier = null;
            _TotalDistanceKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;
            Origin = "";
            Destination = "";
            OriginPointer = null;
            DestinationPointer = null;
        }

        ///
        /// \fn Trip(string origin, string destination)
        /// 
        /// \brief Constructor with the origin and destination values
        /// \details <b>Details</b>
        ///
        /// This method sets all the default values upon creation of a new object
        ///
        /// \param origin <b>string</b> - The origin city name
        /// \param destination <b>string</b> - The destination city name
        ///
        /// \return none - Returns nothing
        ///
        public Trip(string origin, string destination)
        {
            _TripID = 0;
            TripCarrier = null;
            _TotalDistanceKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;
            OriginPointer = DestinationInfo.GetDestinationByName(origin);
            DestinationPointer = DestinationInfo.GetDestinationByName(destination);
        }

        ///
        /// \fn SetOriginDestination(string origin, string destination)
        /// 
        /// \brief Method to set the destination and origin city Route Pointers
        /// \details <b>Details</b>
        ///
        /// This method sets the origin and destination city pointers to the parameter
        ///
        /// \param origin <b>string</b> - The origin city name
        /// \param destination <b>string</b> - The destination city name
        ///
        /// \return none - Returns nothing
        ///
        public void SetOriginDestination(string origin, string destination)
        {
            //Variables
            Routes tempRoutes = new Routes();
            List<Routes> allRoutes = Routes.GetRoutes();

            // find destination by string name and assign
            foreach (Routes d in allRoutes)
            {
                if (origin == d.City)
                {
                    OriginPointer = d;
                }
                else if (destination == d.City)
                {
                    DestinationPointer = d;
                }
            }
        }


        ///
        /// \fn CalculateTotals(int isLTL)
        /// 
        /// \brief Method to calculates and sets the totals based on the route that must be taken
        /// \details <b>Details</b>
        ///
        /// This method calculates and sets the totals based on the route that must be taken
        ///
        /// \param isLTL <b>int</b> - whether or not the contract is an Ltl
        ///
        /// \return none - Returns nothing
        ///
        public void CalculateTotals(int isLTL)
        {
            // start at the origin
            Routes currentCity = OriginPointer;
            List<Routes> allRoutes = Routes.GetRoutes();           //Gets all routes from route table in database
            int direction = 0;

            if (DestinationPointer.RouteID < OriginPointer.RouteID)
            {
                direction = kGoingWest;
            }
            else if (DestinationPointer.RouteID > OriginPointer.RouteID)
            {
                direction = kGoingEast;
            }
            else
            {
                Logger.LogToFile("Error: matching origin and destination for TripID " + TripID);   
            }

            // loop from origin to destination to determine totals

            if (isLTL == 0)
            {
                // add 2 hours for each origin and destination for FTL
                TotalDistanceHours = TimeSpan.FromHours(4);
            }

            while(true)
            {
                TotalDistanceKm += currentCity.DistanceKm;

                TotalDrivingHours += currentCity.DistanceHours;
                TotalDistanceHours += currentCity.DistanceHours;

                // add 2 hours per intermediary city for LTL
                if (isLTL == 1)
                {
                    TotalDistanceHours += TimeSpan.FromHours(2);
                }

                if (currentCity.City == DestinationPointer.City)
                {
                    // stop looping once destination is found
                    break;
                }
                else if (direction == kGoingWest)
                {
                    // move west 1 city
                    foreach(Routes r in allRoutes)
                    {
                        if(currentCity.WestDestinationName == r.City)
                        {
                            currentCity = r;
                            break;
                        }
                    }
                }
                else if (direction == kGoingEast)
                {
                    // move east 1 city
                    foreach (Routes r in allRoutes)
                    {
                        if (currentCity.EastDestinationName == r.City)
                        {
                            currentCity = r;
                            break;
                        }
                    }
                }
                else if (currentCity == null)
                {
                    Logger.LogToFile("Error: invalid trip - currentCity is null, going direction " + direction + " on TripID " + TripID);   
                }
            }
        }
    }
}
