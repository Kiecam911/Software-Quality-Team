using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Logging;

namespace TMSv2_TripPlanner
{
    public static class DestinationInfo
    {
        public static List<Routes> AllCities;

        public static void InitializeDestinations()
        {
            Routes dWindsor = new Routes(0, "Windsor", 191, 2.5, "END", "London");
            Routes dLondon = new Routes(1, "London", 128, 1.75, "Windsor", "Hamilton");
            Routes dHamilton = new Routes(2, "Hamilton", 68, 1.25, "London", "Toronto");
            Routes dToronto = new Routes(3, "Toronto", 60, 1.3, "Hamilton", "Oshawa");
            Routes dOshawa = new Routes(4, "Oshawa", 134, 1.65, "Toronto", "Belleville");
            Routes dBelleville = new Routes(5, "Belleville", 82, 1.2, "Oshawa", "Kingston");
            Routes dKingston = new Routes(6, "Kingston", 196, 2.5, "Ottawa", "Belleville");
            Routes dOttawa = new Routes(7, "Ottawa", 0, 0, "Kingston", "END");

            //dWindsor.WestDest = null;
            //dWindsor.EastDest = dLondon;
            //AllCities.Add(dWindsor);

            //dLondon.WestDest = dWindsor;
            //dLondon.EastDest = dHamilton;
            //AllCities.Add(dLondon);

            //dHamilton.WestDest = dLondon;
            //dHamilton.EastDest = dToronto;
            //AllCities.Add(dHamilton);

            //dToronto.WestDest = dHamilton;
            //dToronto.EastDest = dOshawa;
            //AllCities.Add(dToronto);

            //dOshawa.WestDest = dToronto;
            //dOshawa.EastDest = dBelleville;
            //AllCities.Add(dOshawa);

            //dBelleville.WestDest = dOshawa;
            //dBelleville.EastDest = dKingston;
            //AllCities.Add(dBelleville);

            //dKingston.WestDest = dBelleville;
            //dKingston.EastDest = dOttawa;
            //AllCities.Add(dKingston);

            //dOttawa.WestDest = dKingston;
            //dOttawa.EastDest = null;
            //AllCities.Add(dOttawa);
        }


        public static Routes GetDestinationByName(string name)
        {
            foreach(Routes d in AllCities)
            {
                if (name == d.City)
                {
                    return d;
                }
            }
            return null;
        }
    }
}
