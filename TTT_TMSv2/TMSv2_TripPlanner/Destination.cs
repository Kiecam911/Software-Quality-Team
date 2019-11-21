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
    /// \data member city <i>string</i> - <i>private<i> data member that holds the final city the <b>Order</b> will reach
    /// \data member distanceKm <i>int</i> - <i>private<i> data member that holds the distance in KM
    /// \data member dsitanceHours <i>float</i> - <i>private<i> data member that holds the distance in hours
    /// \data member westDest <i>Destination</i> - <i>private<i> data member that holds the distance to the end destination
    /// \data member eastDest <i>Destination</i> - <i>private<i> data member that holds the distance to the end destination
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \see Order
    /// \see Carrier
    /// \see Trip
    ///
    public class Destination
    {
        private string city;
        private int distanceKm;
        private float distanceHours;
        private Destination westDest;
        private Destination eastDest;

    }
}
