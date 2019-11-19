using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    /// 
    /// \class Planner : User
    ///
    /// \brief The purpose of this class is to simulate the "Planner" actor
    /// \details <b>Details</b>
    ///
    /// This class represents the <b>Planner</b> actor and its capabilities. The <b>Planner</b> recieves <b>Orders</b> from <b>Buyers</b>. From there the
    /// <b>Planner</b> selects <b>Carriers</b> from targeted cities to the completed <b>Order</b>. The <b>Planner</b> can also simulate the passage of time
    /// in 1-day increments to move <b>Orders</b> and their trips to completed states. Completed <b>Orders</b> are marked for follow up from the <b>Buyer</b>.
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \see Buyer
    /// \see Order
    /// \see Carrier
    /// \see User
    ///
    public class Planner : User
    {
        ///
        /// \brief To instantiate a new Planner
        /// \details <b>Details</b>
        ///
        /// Instantiates the Planner class by setting the permission level
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Planner class, nothing is returned
        ///
        public Planner()
        {
            PermissionLevel = PERMISSION_PLANNER;
        }
    }
}
