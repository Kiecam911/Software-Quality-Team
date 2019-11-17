using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    public class Planner : User
    {
        public Planner()
        {
            PermissionLevel = PERMISSION_PLANNER;
        }
    }
}
