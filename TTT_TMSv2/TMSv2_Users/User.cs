using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// \var data member PermissionLevel <i>char</i> - <i>protected<i> data member that holds which permission level the user possesses
    /// \var data member userID <i>string</i> - <i>protected<i> data member that holds the userID login credential
    /// \var data member password <i>string</i> - <i>protected<i> data member that holds the password login credential
    ///
    /// \author <i>TeamTeamTeam</i>
    ///
    public class User
    {
        protected const char PERMISSION_NONE = 'n';                    
        protected const char PERMISSION_PLANNER = 'p';
        protected const char PERMISSION_BUYER = 'b';
        protected const char PERMISSION_ADMIN = 'a';

        protected char PermissionLevel { get; set; }
        protected string userID;
        protected string password;



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
    }
}
