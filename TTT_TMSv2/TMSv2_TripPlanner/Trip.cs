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
        public Routes Origin { get; set; }                  /// The city of origin of the Contract
        public Routes Destination { get; set; }             /// The destination city of the Contract
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
        public Trip(string origin, string destination)
        {
            _TripID = 0;
            TripCarrier = null;
            _TotalDistanceKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;

            Origin = DestinationInfo.GetDestinationByName(origin);
            Destination = DestinationInfo.GetDestinationByName(destination);
        }




        public void SetOriginDestination(string origin, string destination)
        {
            //Variables
            Routes tempRoutes = new Routes();
            List<Routes> allRoutes = tempRoutes.GetRoutes();

            // find destination by string name and assign
            foreach (Routes d in allRoutes)
            {
                if (origin == d.City)
                {
                    Origin = d;
                }
                else if (destination == d.City)
                {
                    Destination = d;
                }
            }
        }



        public void CalculateTotals(bool isFTL)
        {
            // start at the origin
            Routes currentCity = Origin;
            List<Routes> allRoutes = currentCity.GetRoutes();           //Gets all routes from route table in database
            int direction = 0;

            if (Destination.RouteID < Origin.RouteID)
            {
                direction = kGoingWest;
            }
            else if (Destination.RouteID < Origin.RouteID)
            {
                direction = kGoingEast;
            }
            else
            {
                Logger.LogToFile("Error: matching origin and destination for TripID " + TripID);   
            }

            // loop from origin to destination to determine totals

            if (isFTL)
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
                if (!isFTL)
                {
                    TotalDistanceHours += TimeSpan.FromHours(2);
                }

                if (currentCity == Destination)
                {
                    // stop looping once destination is found
                    break;
                }
                else if (direction == kGoingWest)
                {
                    // move west 1 city
                    currentCity = currentCity.WestDest;
                }
                else if (direction == kGoingEast)
                {
                    // move east 1 city
                    currentCity = currentCity.EastDest;
                }
                else if (currentCity == null)
                {
                    Logger.LogToFile("Error: invalid trip - currentCity is null, going direction " + direction + " on TripID " + TripID);   
                }
            }
        }
    }
}
