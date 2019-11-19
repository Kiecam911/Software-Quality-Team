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
        public Buyer()
        {
            PermissionLevel = PERMISSION_BUYER;
        }

        public int CreateOrder()
        {

            return 1;
        }
    }
}
