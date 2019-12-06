using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_TripPlanner
{
    /// 
    /// \class Destination
    ///
    /// \brief The purpose of this class is to 
    /// \details <b>Details</b>
    ///
    /// This class is meant to be the base for a doublely linked list taken from the Routes Table in the SQL Database
    /// 
    /// \var data member City <i>string</i> - <i>private<i> data member that holds the final city the <b>Order</b> will reach
    /// \var data member _DistanceKm <i>double</i> - <i>private<i> data member that holds the distance in KM
    /// \var data member _DistanceHours <i>double</i> - <i>private<i> data member that holds the distance in hours
    /// \var data member _WestDestination <i>Destination</i> - <i>private<i> data member that contains a reference to the destination to the west
    /// \var data member _EastDestination <i>Destination</i> - <i>private<i> data member that contains a reference to the destination to the east
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \see Trip
    ///
    public class Destination
    {
        // Data members
        private string City { get; set; }
        private double _DistanceKm;
        public double DistanceKm
        { 
            get { return _DistanceKm; }
            set
            {
                if(value >= 0.0)
                {
                    _DistanceKm = value;
                }
            }
        }
        private float _DistanceHours;
        public float DistanceHours
        {
            get { return _DistanceHours; }
            set
            {
                if (value >= 0.0)
                {
                    _DistanceHours = value;
                }
            }
        }
        private Destination _WestDestination;
        public Destination WestDest { get { return _WestDestination; } }
        private Destination _EastDestination;
        public Destination EastDest { get { return _EastDestination; } }

        public Destination()
        {
            City = "";
            _DistanceHours = -1.0f;
            _DistanceKm = -1.0f;
            _WestDestination = null;
            _EastDestination = null;
        }

        ///
        /// \fn SetWestDestination(Destination d)
        /// 
        /// \brief Sets d to the WestDestination
        /// \details <b>Details</b>
        ///
        /// Sets Destination d to the West destination so that getting the West Destination does not
        /// allow for possible accidental overwritting of the WestDestination
        ///
        /// \param d <b>Destination</b> - The Destination that will be set
        ///
        /// \return Nothing is returned
        ///
        public void SetWestDestination(Destination d)
        {
            _WestDestination = d;
        }

        ///
        /// \fn SetWestDestination(Destination d)
        /// 
        /// \brief Sets d to the WestDestination
        /// \details <b>Details</b>
        ///
        /// Sets Destination d to the West destination so that getting the West Destination does not
        /// allow for possible accidental overwritting of the WestDestination
        ///
        /// \param d <b>Destination</b> - The Destination that will be set
        ///
        /// \return Nothing is returned
        ///
        public void SetEastDestination(Destination d)
        {
            _EastDestination = d;
        }
    }
}
