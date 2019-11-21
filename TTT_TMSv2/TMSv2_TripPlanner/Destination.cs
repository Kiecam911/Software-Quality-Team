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
    /// This class represents the 
    /// 
    /// \var data member city <i>string</i> - <i>private<i> data member that holds the final city the <b>Order</b> will reach
    /// \var data member distanceKm <i>int</i> - <i>private<i> data member that holds the distance in KM
    /// \var data member dsitanceHours <i>float</i> - <i>private<i> data member that holds the distance in hours
    /// \var data member westDest <i>Destination</i> - <i>private<i> data member that holds the distance to the end destination
    /// \var data member eastDest <i>Destination</i> - <i>private<i> data member that holds the distance to the end destination
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \see Trip
    ///
    public class Destination
    {
        // Private data members
        private string city;                    /// 
        private int distanceKm;                 /// 
        private float distanceHours;
        private Destination westDest;
        private Destination eastDest;

    }
}
