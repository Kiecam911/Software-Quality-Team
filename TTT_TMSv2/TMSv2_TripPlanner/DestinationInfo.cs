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
            Destination dWindsor = new Destination(0, "Windsor", 191, 2.5);
            Destination dLondon = new Destination(1, "London", 128, 1.75);
            Destination dHamilton = new Destination(2, "Hamilton", 68, 1.25);
            Destination dToronto = new Destination(3, "Toronto", 60, 1.3);
            Destination dOshawa = new Destination(4, "Oshawa", 134, 1.65);
            Destination dBelleville = new Destination(5, "Belleville", 82, 1.2);
            Destination dKingston = new Destination(6, "Kingston", 196, 2.5);
            Destination dOttawa = new Destination(7, "Ottawa", 0, 0);

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
