using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Carriers;
using System.Text.RegularExpressions;

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
        public string Origin { get; set; }                  /// The city of origin of the Contract
        public string Destination { get; set; }             /// The destination city of the Contract
        private int _DistanceKm;                            /// The calculated total distance that must be traveled
        public int DistanceKm                               /// Public accessor to the private _TotalKm for safety
        {
            get { return _DistanceKm; }
            set
            {
                if (value >= 0)
                {
                    _DistanceKm = value;
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
            Origin = "";
            Destination = "";
            _DistanceKm = 0;
            _HoursTaken = TimeSpan.FromHours(0.0);
            IsCompleted = false;
        }

    }
}
