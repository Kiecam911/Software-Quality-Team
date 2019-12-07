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
    /// \var data member _WestDestination <i>Destination</i> - <i>private<i> data member that contains a reference to the destination to the west
    /// \var data member _EastDestination <i>Destination</i> - <i>private<i> data member that contains a reference to the destination to the east
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \see Trip
    ///
    public class Destination
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
        public string WestDestination { get; set; }
        public string EastDestination { get; set; }


        public Destination()
        {
            _RouteID = 0;
            City = "";
            _DistanceHours = TimeSpan.FromHours(0.0);
            _DistanceKm = 0;
            WestDestination = "";
            EastDestination = "";
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
        public List<Destination> GetRoutes()
        {
            // create objects to hold information from DAL
            DataAccess tempDA = DataAccess.Instance();
            DataTable routesTable = tempDA.GetRouteTable().Tables[0];
            List<Destination> routesList = new List<Destination>();
            Destination tempDestination = null;

            DataRowCollection routeRows = routesTable.Rows;

            // loop through each row of data, creating contract and assigning values to it
            foreach (DataRow currentRow in routeRows)
            {
                //Instantiate a new Destination
                tempDestination = new Destination();

                //Fill destination
                tempDestination.RouteID = currentRow.Field<int>(0);
                tempDestination.City = currentRow.Field<string>(1);
                tempDestination.DistanceKm = currentRow.Field<int>(2);
                tempDestination.DistanceHours = currentRow.Field<TimeSpan>(3);
                tempDestination.WestDestination = currentRow.Field<string>(4);
                tempDestination.EastDestination = currentRow.Field<string>(5);

                //Add to list
                routesList.Add(tempDestination);
            }
            return routesList;
        }

        public bool UpdateRoutesTable(List<Destination> destList)
        {
            //Variables
            DataAccess da = DataAccess.Instance();

            //For each destination in the list update the database and check for failure/success
            foreach (Destination d in destList)
            {
                if(!da.UpdateRoutes(d.RouteID, d.City, d.DistanceKm, d.DistanceHours, d.WestDestination, d.EastDestination))
                {
                    return false;
                }
            }

            //Return success
            return true;
        }

    }
}
