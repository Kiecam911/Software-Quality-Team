using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMSv2_TripPlanner;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_Order;
using TMSv2_UI;
using TMSv2_Users;

namespace UnitTesting
{
    /// 
    /// \class UnitTest
    ///
    /// \brief The purpose of this class is to test the other classes and their methods
    /// \details <b>Details</b>
    ///
    /// This class is meant to hold the tests for the other classes in the application in order to ensure that they are working as intended
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Depot
    /// \sa Carrier
    /// \sa Contract
    /// \sa Customer
    /// \sa RequestedCargo
    /// \sa Order
    /// \sa Destination
    /// \sa Trip
    /// \sa Admin
    /// \sa Buyer
    /// \sa Planner
    /// \sa User
    ///
    [TestClass]
    public class UnitTest
    {
        ///
        /// \test RequestedCargoUnitTest
        /// 
        /// \brief This test is designed to test the methods in the RequestedCargo class
        /// \details <b>Details</b>
        /// 
        /// This test checks that the RequestedCargo class has proper input checking for the private data members. For example,
        /// <b>cargoVolume</b> should be no less than or equal to 0.
        ///
        [TestMethod]
        public void RequestedCargoUnitTest()
        {
        }
    }
}
