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
            AllCities = new List<Routes>();

            Routes rWindsor = new Routes(0, "Windsor", 191, 2.5);
            Routes rLondon = new Routes(1, "London", 128, 1.75);
            Routes rHamilton = new Routes(2, "Hamilton", 68, 1.25);
            Routes rToronto = new Routes(3, "Toronto", 60, 1.3);
            Routes rOshawa = new Routes(4, "Oshawa", 134, 1.65);
            Routes rBelleville = new Routes(5, "Belleville", 82, 1.2);
            Routes rKingston = new Routes(6, "Kingston", 196, 2.5);
            Routes rOttawa = new Routes(7, "Ottawa", 0, 0);

            rWindsor.WestDestination = null;
            rWindsor.EastDestination = rLondon;
            AllCities.Add(rWindsor);

            rLondon.WestDestination = rWindsor;
            rLondon.EastDestination = rHamilton;
            AllCities.Add(rLondon);

            rHamilton.WestDestination = rLondon;
            rHamilton.EastDestination = rToronto;
            AllCities.Add(rHamilton);

            rToronto.WestDestination = rHamilton;
            rToronto.EastDestination = rOshawa;
            AllCities.Add(rToronto);

            rOshawa.WestDestination = rToronto;
            rOshawa.EastDestination = rBelleville;
            AllCities.Add(rOshawa);

            rBelleville.WestDestination = rOshawa;
            rBelleville.EastDestination = rKingston;
            AllCities.Add(rBelleville);

            rKingston.WestDestination = rBelleville;
            rKingston.EastDestination = rOttawa;
            AllCities.Add(rKingston);

            rOttawa.WestDestination = rKingston;
            rOttawa.EastDestination = null;
            AllCities.Add(rOttawa);
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
