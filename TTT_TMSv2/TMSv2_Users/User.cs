using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    public class User
    {
        protected const char PERMISSION_NONE = 'n';
        protected const char PERMISSION_PLANNER = 'p';
        protected const char PERMISSION_BUYER = 'b';
        protected const char PERMISSION_ADMIN = 'a';

        public char PermissionLevel { get; set; }
        protected string userID;
        protected string password;

        public User()
        {
            PermissionLevel = PERMISSION_NONE;
        }
    }
}
