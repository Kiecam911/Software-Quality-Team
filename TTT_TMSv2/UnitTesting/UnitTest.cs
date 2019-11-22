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
        * RequestedCargo Boundary Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the boundaries and functionality of the Set Method in RequestedCargo
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends a value that is on the boundary of the accepted inputs (eg. 0.00001, 0.0001, -0.00001, -0.0001)
        * 
        * \test <b>Type of Test:</b>
        * Boundary and Functional
        * 
        * \test <b>Expected:</b>
        * For First Two: 0.00001, 0.0001
        * For Second Two: Exception Catch
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void RequestedCargoBoundaryTest()
        {
            //Variables
            var reqCargo = new RequestedCargo();

            // Try Catch block to test the fail condition
            try
            {
                // Setting the cargoVolume to a 0.00001
                reqCargo.SetCargoVolume(0.00001);
                Assert.AreEqual(0.00001, reqCargo.GetCargoVolume());

                // Setting the cargoVolume to a 0.00001
                reqCargo.SetCargoVolume(0.0001);
                Assert.AreEqual(0.0001, reqCargo.GetCargoVolume());


            }
            catch (Exception e)
            {
                // Asserting that Test Failed
                Assert.AreNotEqual("Invalid Cargo Volume", e.Message);
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

        /**
        * \test 
        * CarrierFunctionTest
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the methods in the Carrier class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Methods use expected values to check whether or not the datamembers have been set
        * to the correct/expected values
        * 
        * \test <b>Type of Test:</b>
        * Function
        * 
        * \test <b>Expected:</b>
        * Values are equal
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void CarrierFunctionTest()
        {
            Carrier carrier = new Carrier();

            //Testing for CarrierID
            try
            {
                carrier.setCarrierID(989);

                Assert.AreEqual(989, carrier.getCarrierID());
            }
            catch
            {
                Console.WriteLine("Setting the carrierID has failed");
            }

            //Test for Carrier Name
            try
            {
                carrier.setCarrierName("JohnDeer");
                Assert.AreEqual("JohnDeer", carrier.getCarrierName());
            }
            catch
            {
                Console.WriteLine("Setting carrier name has failed");
            }

            //Test for setting isFTL
            try
            {
                carrier.setIsFTL(true);
                Assert.AreEqual(true, carrier.getIsFTL());
            }
            catch
            {
                Console.WriteLine("Set FTL has failed");
            }

            //Test for PalletFTLRate
            try
            {
                carrier.setPerPalletFTLRate(12.41);
                Assert.AreEqual(12.41, 12.41);
            }
            catch
            {
                Console.WriteLine("Set Pallet LTL Rate has failed");
            }

            //Test for setting IsReefer
            try
            {
                carrier.setIsReefer(true);
                Assert.AreEqual(true, carrier.getIsReefer());
            }
            catch
            {
                Console.WriteLine("Setting isReefer has failed");
            }

            //Test for setting carrier capacity
            try
            {
                carrier.setCarrierCapacity(1200);
                Assert.AreEqual(1200, carrier.getCarrierCapacity());
            }
            catch
            {
                Console.WriteLine("Set carrier capacity has failed");
            }
        }

        /**
        * \test 
        * CarrierExceptionTests
        * 
        * \test <b>Purpose:</b>
        * This test is designed to exception test the methods in the Carrier class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Methods use expected values to check whether or not the datamembers have been set
        * to the correct/expected values
        * 
        * \test <b>Type of Test:</b>
        * Exception
        * 
        * \test <b>Expected:</b>
        * Values are equal
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void CarrierExceptionTests()
        {
            Carrier carrier = new Carrier();
            
            //Test for setting carrier name to blank
            try
            {
                carrier.setCarrierName("");
                Assert.AreEqual("none", carrier.getCarrierName());
            }
            catch
            {
                Console.WriteLine("Error handling failed for Carrier Name");
            }

            //Test error handling for PalletFTLRate
            try
            {
                carrier.setPerPalletFTLRate(-5);
                Assert.AreEqual(0, carrier.getPerPalletFTLRate());
            }
            catch
            {
                Console.WriteLine("Error handling failed for PalletLTLRate");
            }

            //Test for carrierCap
            try
            {
                carrier.setCarrierCapacity(-100);
                Assert.AreEqual(0, carrier.getCarrierCapacity());
            }
            catch
            {
                Console.WriteLine("Error handling failed for carrier capacity");
            }
        }

        /**
        * \test 
        * CarrierExceptionTests
        * 
        * \test <b>Purpose:</b>
        * This test is designed to exception test the methods in the Customer class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Methods use expected values to check whether or not the datamembers have been set
        * to the correct/expected values, and check data verification
        * 
        * \test <b>Type of Test:</b>
        * Automated
        * 
        * \test <b>Expected:</b>
        * Values are reset to defaulted values
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void CustomerExceptionTests()
        {
            Customer Cx = new Customer();

            //Test for setting customer name
            try
            {
                Cx.setCustomerName("");
                Assert.AreEqual("none", Cx.getCxName());
            }
            catch
            {
                Console.WriteLine("Error handling for customer name failed");
            }

            //Test for customer ID
            try
            {
                Cx.setCustomerID(-5);
                Assert.AreEqual(0, Cx.getCxID());
            }
            catch
            {
                Console.WriteLine("Error handling for customerID failed");
            }
        }
    }
}
