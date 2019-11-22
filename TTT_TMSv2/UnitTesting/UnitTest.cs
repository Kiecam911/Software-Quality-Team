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
        /**
        * \test 
        * RequestedCargo Exception Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the methods in the RequestedCargo class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends an invalid value to the function and tests for exception
        * 
        * \test <b>Type of Test:</b>
        * Exception
        * 
        * \test <b>Expected:</b>
        * Exception Catch
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void RequestedCargoExceptionTest()
        {
            //Variables
            var reqCargo = new RequestedCargo();

            // Try Catch block to test the fail condition
            try
            {
                // Setting the cargoVolume to an invalid amount
                reqCargo.SetCargoVolume(-1.0);
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Cargo Volume", e.Message);
            }
        }

        /**
        * \test 
        * Trip Exception Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the methods in the Trip class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends an invalid value to the function and tests for exception
        * 
        * \test <b>Type of Test:</b>
        * Exception
        * 
        * \test <b>Expected:</b>
        * Exception Catch
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void TripExceptionTest()
        {
            //Variables
            var trip = new Trip();

            // Try Catch block to test the fail condition
            try
            {
                // Setting the endDestination to an invalid amount
                trip.SetEndDestination("");
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid City", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Setting the kmDistance to an invalid amount
                trip.SetKmDistance(-1.0f);
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }
        }

        /**
        * \test 
        * Admin Exception Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the methods in the Admin class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends an invalid value to the function and tests for exception
        * 
        * \test <b>Type of Test:</b>
        * Exception
        * 
        * \test <b>Expected:</b>
        * Exception Catch
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void AdminExceptionTest()
        {
            //Variables
            var admin = new Admin();

            // Try Catch block to test the fail condition
            try
            {
                // Calling the BackupData
                admin.BackupData();
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Cannot Backup Data", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ChooseLogDirectory
                admin.ChooseLogDirectory();
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Directory", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ModifyCarrierData
                admin.ModifyCarrierData();
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Cannot Modify Carrier Data", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ModifyFeeTable
                admin.ModifyFeeTable();
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Cannot Modify Fee Table", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ViewLogFiles
                admin.ViewLogFile();
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Log Files", e.Message);
            }

        }

        /**
        * \test 
        * Destination Exception Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the methods in the Destination class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends an invalid value to the function and tests for exception
        * 
        * \test <b>Type of Test:</b>
        * Exception
        * 
        * \test <b>Expected:</b>
        * Exception Catch
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void DestinationExceptionTest()
        {
            //Variables
            var destination = new Destination();

            // Try Catch block to test the fail condition
            try
            {
                // Setting the endDestination to an invalid amount
                destination.SetDistanceHours(-1.0f);
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Time", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Setting the kmDistance to an invalid amount
                destination.SetDistanceKm(-1.0f);
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Setting the city to an invalid amount
                destination.SetCity("");
            }
            catch (Exception e)
            {
                // Determining if the fail condition succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }
        }
    }
}
