using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_DAL;
using TMSv2_Logging;
using TMSv2_Order;
using TMSv2_TripPlanner;
using TMSv2_Users;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize data
            DestinationInfo.InitializeDestinations();

            Contract c = new Contract();
            c.JobType = 0;
            c.Origin = "Ottawa";
            c.Destination = "Windsor";
            c.Quantity = 10;

            Order o = new Order(c);
            Trip t = new Trip(c.Origin, c.Destination);

            List<Trip> lt = new List<Trip>();
            lt.Add(t);

            o.AddTripsToOrder(lt);

            bool isFTL = false;
            t.CalculateTotals(isFTL);
            o.CalculateTotalCost(isFTL);

            Console.WriteLine("FinalCarrierPrice: {0}\nFinalCustomerPrice: {1}\nTotalTripHours: {2}\nTotalDrivingHours: {3}\nTotalTripKM: {4}", o.FinalCarrierPrice, o.FinalCustomerPrice, t.TotalDistanceHours, t.TotalDrivingHours, t.TotalDistanceKm);

            Console.ReadKey();
        }
    }
}
