using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    /// 
    /// \class Admin : User
    ///
    /// \brief The purpose of this class is to simulate the "Admin" actor
    /// \details <b>Details</b>
    ///
    /// This class represents the "Admin" actor and its capabilities. This class is able to access general configuration options for the 
    /// Transport Management System(TMS), such as selecting directories for log files, targeting IP address and ports for all Database 
    /// Management Systems communications, etc. It is also able to alter the Rate/fee tables, carrier Data, Route Table, review log files
    /// within the application, and can initiate a file backup specifying the directory for the backup files.
    ///
    /// \data member orderId <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \see User
    ///
    public class Admin : User
    {
        ///
        /// \brief To instantiate a new Admin
        /// \details <b>Details</b>
        ///
        /// Instantiates the Admin class by setting the permission level
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Admin class, nothing is returned
        ///
        public Admin()
        {
            PermissionLevel = PERMISSION_ADMIN;
        }
    }
}
