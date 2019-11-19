using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    /// 
    /// \class Buyer : User
    ///
    /// \brief The purpose of this class is to contain the data and methods associated with the "Buyer" actor
    /// \details <b>Details</b>
    ///
    /// This class represents the "Buyer" actor and is capable of 
    ///
    /// \data member orderId <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    ///
    /// \author <i>TeamTeamTeam</i>
    ///
    public class Buyer : User
    {
        ///
        /// \brief To instantiate a new Buyer
        /// \details <b>Details</b>
        ///
        /// Instantiates the Buyer class by setting the permission level
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Buyer class, nothing is returned
        ///
        public Buyer()
        {
            PermissionLevel = PERMISSION_BUYER;
        }

        ///
        /// \brief Creates a new <b>Order</b>
        /// \details <b>Details</b>
        ///
        /// Creates a new <b>Order</b>
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this method
        ///
        /// \return Returns an integer value indicating success or fail (along with what type of failure)
        /// 
        /// \see Order
        ///
        public int CreateOrder()
        {

            return 1;
        }
    }
}
