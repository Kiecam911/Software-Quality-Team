using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_Order;
using TMSv2_TripPlanner;

namespace TMSv2_Users
{
    /// 
    /// \class User
    ///
    /// \brief The purpose of this class is to contain the shared data between the 3 user subclasses.
    /// \details <b>Details</b>
    ///
    /// This class models the base user and some basic info including permission level and login credentials.
    ///
    /// \var data member PERMISSION_NONE <i>const char</i> - <i>protected<i> data member that holds the char representing the permission level
    /// \var data member PERMISSION_PLANNER <i>const char</i> - <i>protected<i> data member that holds the char representing the permission level
    /// \var data member PERMISSION_BUYER <i>const char</i> - <i>protected<i> data member that holds the char representing the permission level
    /// \var data member PERMISSION_ADMIN <i>const char</i> - <i>protected<i> data member that holds the char representing the permission level
    /// \var data member PermissionLevel <i>char</i> - <i>public<i> data member that holds which permission level the user possesses
    /// \var data member UserID <i>string</i> - <i>protected<i> data member that holds the userID login credential
    /// \var data member Password <i>string</i> - <i>protected<i> data member that holds the password login credential
    ///
    /// \author <i>TeamTeamTeam</i>
    ///
    public class User
    {
        protected const char PERMISSION_NONE = 'n';                    
        protected const char PERMISSION_PLANNER = 'p';
        protected const char PERMISSION_BUYER = 'b';
        protected const char PERMISSION_ADMIN = 'a';

        public char PermissionLevel { get; set; }
        protected string UserID;
        protected string Password;



        ///
        /// \brief To instantiate a new User
        /// \details <b>Details</b>
        ///
        /// Instantiates the User class by setting the permission level
        ///
        /// \param <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the User class, nothing is returned
        ///
        public User()
        {
            PermissionLevel = PERMISSION_NONE;
        }



        ///
        /// \brief To set a users permission level
        /// \details <b>Details</b>
        ///
        /// Called my a child class upon instantiation to set the permission level. Must be valid,
        /// or false is returned and the permission does not change.
        ///
        /// \param char level - the permission level 
        ///
        /// \return bool - true or false depending if the permission level changed or not.
        ///
        public bool SetPermissionLevel(char level)
        {
            if (level == PERMISSION_NONE
                || level == PERMISSION_PLANNER
                || level == PERMISSION_BUYER
                || level == PERMISSION_ADMIN)
            {
                PermissionLevel = level;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
