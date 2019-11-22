using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            //Variables
            var reqCargo = new RequestedCargo();
            double result = 0.0;

            /// Try Catch block to test the fail condition
            try
            {
                /// Setting the cargoVolume to an invalid amount
                reqCargo.SetCargoVolume(-1.0);
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Cargo Volume", e.Message);
            }

            /// Get the cargoVolume after a failed set call
            result = reqCargo.GetCargoVolume();

            /// Determining if the Get works
            Assert.AreEqual(-1.0, result);
        }

        ///
        /// \test TripUnitTest
        /// 
        /// \brief This test is designed to test the methods in the Trip class
        /// \details <b>Details</b>
        /// 
        /// This test checks that the Trip class has proper input checking for the private data members. For example,
        /// <b>kmDistance</b> should be no less than or equal to 0.
        ///
        [TestMethod]
        public void TripUnitTest()
        {
            //Variables
            var trip = new Trip();

            /// Try Catch block to test the fail condition
            try
            {
                /// Setting the endDestination to an invalid amount
                trip.SetEndDestination("");
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid City", e.Message);
            }

            /// Get the endDestination after a failed set call
            var result1 = trip.GetEndDestination();

            /// Determining if the Get works
            Assert.AreEqual("", result1);

            /// Try Catch block to test the fail condition
            try
            {
                /// Setting the kmDistance to an invalid amount
                trip.SetKmDistance(-1.0f);
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            /// Get the kmDistance after a failed set call
            var result2 = trip.GetKmDistance();

            /// Determining if the Get works
            Assert.AreEqual(-1.0f, result2);
        }

        ///
        /// \test AdminUnitTest
        /// 
        /// \brief This test is designed to test the methods in the Admin class
        /// \details <b>Details</b>
        /// 
        /// This test checks that the Admin class can access general configuration options, review log files in-app, alter database data, 
        /// and initiate local TMS database while specifying the directory for the backup files.
        ///
        [TestMethod]
        public void AdminUnitTest()
        {
            //Variables
            var admin = new Admin();

            /// Try Catch block to test the fail condition
            try
            {
                /// Calling the BackupData
                admin.BackupData();
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Cannot Backup Data", e.Message);
            }

            /// Try Catch block to test the fail condition
            try
            {
                /// Call ChooseLogDirectory
                admin.ChooseLogDirectory();
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Directory", e.Message);
            }

            /// Try Catch block to test the fail condition
            try
            {
                /// Call ModifyCarrierData
                admin.ModifyCarrierData();
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Cannot Modify Carrier Data", e.Message);
            }

            /// Try Catch block to test the fail condition
            try
            {
                /// Call ModifyFeeTable
                admin.ModifyFeeTable();
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Cannot Modify Fee Table", e.Message);
            }

            /// Try Catch block to test the fail condition
            try
            {
                /// Call ViewLogFiles
                admin.ViewLogFile();
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Log Files", e.Message);
            }

        }

        ///
        /// \test DistanceUnitTest
        /// 
        /// \brief This test is designed to test the methods in the Distance class
        /// \details <b>Details</b>
        /// 
        /// This test checks that the Trip class has proper input checking for the private data members. For example,
        /// <b>distanceKm</b> should be no less than or equal to 0.
        ///
        [TestMethod]
        public void DistanceUnitTest()
        {
            //Variables
            var destination = new Destination();

            /// Try Catch block to test the fail condition
            try
            {
                /// Setting the endDestination to an invalid amount
                destination.SetDistanceHours(-1.0f);
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Time", e.Message);
            }

            /// Get the endDestination after a failed set call
            var result1 = destination.GetDistanceHours();

            /// Determining if the Get works
            Assert.AreEqual("", result1);

            /// Try Catch block to test the fail condition
            try
            {
                /// Setting the kmDistance to an invalid amount
                destination.SetDistanceKm(-1.0f);
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            /// Get the kmDistance after a failed set call
            var result2 = destination.GetDistanceKm();

            /// Determining if the Get works
            Assert.AreEqual(-1.0f, result2);

            /// Try Catch block to test the fail condition
            try
            {
                /// Setting the city to an invalid amount
                destination.SetCity("");
            }
            catch (Exception e)
            {
                /// Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            /// Get the city after a failed set call
            var result3 = destination.GetCity();

            /// Determining if the Get works
            Assert.AreEqual("", result3);
        }
    }
}
