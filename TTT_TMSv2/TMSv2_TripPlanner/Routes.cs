using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_DAL;

namespace TMSv2_TripPlanner
{
    /// 
    /// \class Destination
    ///
    /// \brief The purpose of this class is to 
    /// \details <b>Details</b>
    ///
    /// This class is meant to be the base for a doublely linked list taken from the Routes Table in the SQL Database
    /// 
    /// \var data member City <i>string</i> - <i>private<i> data member that holds the final city the <b>Order</b> will reach
    /// \var data member _DistanceKm <i>double</i> - <i>private<i> data member that holds the distance in KM
    /// \var data member _DistanceHours <i>double</i> - <i>private<i> data member that holds the distance in hours
    /// \var data member WestDestination <i>string</i> - <i>public<i> data member that contains the name of the city to the west
    /// \var data member EastDestination <i>string</i> - <i>public<i> data member that contains the name of the city to the east
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \see Trip
    ///
    public class Routes
    {
        // Data members
        private int _RouteID;
        public int RouteID
        {
            get { return _RouteID; }
            set
            {
                if (value >= 0)
                {
                    _RouteID = value;
                }
            }
        }
        public string City { get; set; }
        private int _DistanceKm;
        public int DistanceKm
        { 
            get { return _DistanceKm; }
            set
            {
                if(value >= 0.0)
                {
                    _DistanceKm = value;
                }
            }
        }
        private TimeSpan _DistanceHours;
        public TimeSpan DistanceHours
        {
            get { return _DistanceHours; }
            set
            {
                if (value >= TimeSpan.FromHours(0.0))
                {
                    _DistanceHours = value;
                }
            }
        }
        public string WestDestinationName { get; set; }
        public string EastDestinationName { get; set; }

        public Routes WestDestination;
        public Routes EastDestination;


        public Routes()
        {
            _RouteID = 0;
            City = "";
            _DistanceHours = TimeSpan.FromHours(0.0);
            _DistanceKm = 0;
            WestDestinationName = "";
            EastDestinationName = "";
        }

        public Routes(int newIndex, string newCity, int newDistanceKm, double newDistanceHours, string newWestDest, string newEastDest)
        {
            RouteID = newIndex;
            City = newCity;
            DistanceKm = newDistanceKm;
            DistanceHours = TimeSpan.FromHours(newDistanceHours);
            WestDestinationName = newWestDest;
            EastDestinationName = newEastDest;
        }



        public Routes(int newIndex, string newCityName, int newDistanceKm, double newDistanceHours)
        {
            RouteID = newIndex;
            City = newCityName;
            
            DistanceKm = newDistanceKm;
            DistanceHours = TimeSpan.FromHours(newDistanceHours);
        }



        ///
        /// \brief To retrieve Routes from Route table in the database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the database to retrieve the data from the routes table
        ///
        /// \param <b>void</b> - None
        ///
        /// \return List Destination Returns a list of the rows of routes in the database
        ///
        public static List<Routes> GetRoutes()
        {
            // create objects to hold information from DAL
            DataAccess tempDA = DataAccess.Instance();
            DataTable routesTable = tempDA.GetRouteTable().Tables[0];
            List<Routes> routesList = new List<Routes>();
            Routes tempDestination = null;

            DataRowCollection routeRows = routesTable.Rows;

            // loop through each row of data, creating contract and assigning values to it
            foreach (DataRow currentRow in routeRows)
            {
                //Instantiate a new Destination
                tempDestination = new Routes();

                //Fill destination
                tempDestination.RouteID = currentRow.Field<int>(0);
                tempDestination.City = currentRow.Field<string>(1);
                tempDestination.DistanceKm = currentRow.Field<int>(2);
                tempDestination.DistanceHours = currentRow.Field<TimeSpan>(3);
                tempDestination.WestDestinationName = currentRow.Field<string>(4);
                tempDestination.EastDestinationName = currentRow.Field<string>(5);

                //Add to list
                routesList.Add(tempDestination);
            }
            return routesList;
        }

        public static bool UpdateRoutesTable(List<Routes> destList)
        {
            //Variables
            DataAccess da = DataAccess.Instance();

            //For each destination in the list update the database and check for failure/success
            foreach (Routes d in destList)
            {
                if(!da.UpdateRoutes(d.RouteID, d.City, d.DistanceKm, d.DistanceHours, d.WestDestinationName, d.EastDestinationName))
                {
                    return false;
                }
            }

            //Return success
            return true;
        }

    }
}
