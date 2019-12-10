using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_Order;
using TMSv2_TripPlanner;
using System.Data;

namespace TMSv2_Users
{
    /// 
    /// \class Planner : User
    ///
    /// \brief The purpose of this class is to simulate the "Planner" actor
    /// \details <b>Details</b>
    ///
    /// This class represents the <b>Planner</b> actor and its capabilities. The <b>Planner</b> recieves <b>Orders</b> from <b>Buyers</b>. From there the
    /// <b>Planner</b> selects <b>Carriers</b> from targeted cities to the completed <b>Order</b>. The <b>Planner</b> can also simulate the passage of time
    /// in 1-day increments to move <b>Orders</b> and their trips to completed states. Completed <b>Orders</b> are marked for follow up from the <b>Buyer</b>.
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \see Buyer
    /// \see Order
    /// \see Carrier
    /// \see User
    ///
    public class Planner : User
    {
        ///
        /// \brief To instantiate a new Planner
        /// \details <b>Details</b>
        ///
        /// Instantiates the Planner class by setting the permission level
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Planner class, nothing is returned
        ///
        public Planner()
        {
            PermissionLevel = PERMISSION_PLANNER;
        }




        ///
        /// \brief Plans a trip for an order 
        /// \details <b>Details</b>
        ///
        /// This method plans a trip route based on the order and carrier provided as parameters 
        ///
        /// \param Order order - The order to base the trip on
        /// \param Carrier carrier - The carrier that is carrying out the trip
        ///
        /// \return Trip - the final calculated trip
        ///
        public Trip PlanTrip(Order order, Carrier carrier)
        {
            throw new Exception("Invalid order");
        }



        ///
        /// \brief Marks an order as completed
        /// \details <b>Details</b>C:\Users\joelm\source\repos\Kiecam911\Software-Quality-Team\TTT_TMSv2\TMSv2_Users\Planner.cs
        ///
        /// This method evaluates an order and marks it as completed within the system
        ///
        /// \param Order order - The order to complete
        ///
        /// \return Trip - the final calculated trip
        ///
        public void CompleteOrder(Order order)
        {
            throw new Exception("Invalid order");
        }



        ///
        /// \brief Generates a summary report of invoices
        /// \details <b>Details</b>C:\Users\joelm\source\repos\Kiecam911\Software-Quality-Team\TTT_TMSv2\TMSv2_Users\Planner.cs
        ///
        /// This method generates a summary report of all invoice data of all time or the past 2 weeks.
        ///
        /// \param bool isAllTime - bool indicating whether to generate the report accounting for all time or not
        ///
        /// \return none
        ///
        public void GenerateSummaryReport(bool isAllTime)
        {
            throw new Exception("No data");
        }



        ///
        /// \brief Loads data from a DataRow into an Order object
        /// \details <b>Details</b>
        ///
        /// This method creates an Order object, populates it with data from the given row,
        /// and returns it.
        ///
        /// \param DataRow orderInfo - row containing the data relevant for the Order object
        ///
        /// \return Order - the created Order
        ///
        public Order LoadOrderRow(DataRow orderInfo)
        {
            Order order = new Order();
            order.OrderID = orderInfo.Field<int>("OrderID");
            return order;
        }



        ///
        /// \brief Loads data from a DataRow into a Trip object
        /// \details <b>Details</b>
        ///
        /// This method creates an Trip object, populates it with data from the given row,
        /// and returns it.
        ///
        /// \param DataRow tripInfo - row containing the data relevant for the Trip object
        ///
        /// \return Trip - the created Trip
        ///
        public Trip LoadTripRow(DataRow tripInfo)
        {
            Trip trip = new Trip();
            trip.TripID = tripInfo.Field<int>("TripID");
            trip.Origin = tripInfo.Field<string>("Origin");
            trip.Destination = tripInfo.Field<string>("Destination");
            return trip;
        }



        ///
        /// \brief Loads data from a DataRow into a Depot object
        /// \details <b>Details</b>
        ///
        /// This method creates a Depot object, populates it with data from the given row,
        /// and returns it.
        ///
        /// \param DataRow depotInfo - row containing the data relevant for the Depot object
        ///
        /// \return Depot - the created Order
        ///
        public Depot LoadDepotRatesRow(DataRow depotInfo)
        {
            Depot depot = new Depot();
            depot.FTLRate = depotInfo.Field<double>("FTLRate");
            depot.LTLRate = depotInfo.Field<double>("LTLRate");
            depot.ReefCharge = depotInfo.Field<double>("ReefCharge");
            return depot;
        }
    }
}
