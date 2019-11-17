using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    public class Admin : User
    {
        public Admin()
        {
            PermissionLevel = PERMISSION_ADMIN;
        }
    }
}
