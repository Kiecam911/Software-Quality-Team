using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// \var data member endDistance <i>string</i> - <i>private<i> data member that is the designates the end destination of an <b>Order</b>
    /// \var data member kmDistance <i>int</i> - <i>private<i> data member that holds the distance to the end destination
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \sa Destination
    ///
    public class Trip
    {
        // Private Data members
        private string endDestination;              /// The ending destination for the <b>Order</b>
        private int kmDistance;                     /// The kilometer distance from the end destination

    }
}
