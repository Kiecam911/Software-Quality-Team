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
                // Setting the cargoVolume to a 0.0001
                reqCargo.SetCargoVolume(0.0001);
                Assert.AreEqual(0.0001, reqCargo.GetCargoVolume());

                // Setting the cargoVolume to a 0.000001
                reqCargo.SetCargoVolume(0.00001);
                Assert.AreEqual(0.00001, reqCargo.GetCargoVolume());
            }
            catch (Exception e)
            {
                // Asserting that Test Failed
                Assert.AreNotEqual("Invalid Cargo Volume", e.Message);
            }

            try
            {
                //Testing Failed Boundary for -0.0001
                reqCargo.SetCargoVolume(-0.0001);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Invalid Cargo Volume", e.Message);
            }

            try
            {
                //Testing Failed Boundary for -0.00001
                reqCargo.SetCargoVolume(-0.00001);
            }
            catch (Exception e)
            {
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
        * Trip SetEndDestination Functional Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the functionality of the SetEndDestination method
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends a valid destination to method
        * 
        * \test <b>Type of Test:</b>
        * Functional
        * 
        * \test <b>Expected:</b>
        * No Exception Catch (Expected Exception catch For now)
        * 
        * \test <b>Actual:</b>
        * ****CANNOT TEST AS YET TO BE IMPLEMENTED****
        */
        [TestMethod]
        public void TripEndDestinationFunctionalTest()
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
                throw new AssertFailedException();
            }
        }

        /**
        * \test 
        * Trip Boundary Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the boundary and functionality for the SetKmDistance of the Trip class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends a values on the boundary of success. Tests: 0.0001, 0.00001, 0.000001, -0.0001, -0.00001, -0.000001
        * 
        * \test <b>Type of Test:</b>
        * Boundary and Functional
        * 
        * \test <b>Expected:</b>
        * Respectively 0.0001, 0.00001, 0.000001, Exception, Exception, Exception
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void TripBoundaryTest()
        {
            //Variables
            var trip = new Trip();

            // Try Catch block to test the fail condition
            try
            {
                // Test for 0.0001 boundary
                trip.SetKmDistance(0.0001f);
                Assert.AreEqual(0.0001f, trip.GetKmDistance());

                // Test for 0.00001 boundary
                trip.SetKmDistance(0.00001f);
                Assert.AreEqual(0.00001f, trip.GetKmDistance());

                // Test for 0.000001 boundary
                trip.SetKmDistance(0.000001f);
                Assert.AreEqual(0.000001f, trip.GetKmDistance());
            }
            catch (Exception e)
            {
                // Assert that test failed
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Test for -0.0001 boundary
                trip.SetKmDistance(-0.0001f);
            }
            catch (Exception e)
            {
                // Assert that test Succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Test for -0.00001 boundary
                trip.SetKmDistance(-0.00001f);
            }
            catch (Exception e)
            {
                // Assert that test Succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Test for -0.000001 boundary
                trip.SetKmDistance(-0.000001f);
            }
            catch (Exception e)
            {
                // Assert that test Succeeded
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
       * Admin Functional Test
       * 
       * \test <b>Purpose:</b>
       * This test is designed to test the functionality of the methods in the Admin class
       * 
       * \test <b>How Test Is Conducted:</b>
       * Calls the methods and runs them until there is an exception thrown or the method completes successfully (Cannot Test Now As Functionality 
       * has yet to be implemented for the tested methods)
       * 
       * \test <b>Type of Test:</b>
       * Functional
       * 
       * \test <b>Expected:</b>
       * No Exception Thrown (Exception expected for now)
       * 
       * \test <b>Actual:</b>
       * ****CANNOT TEST AS YET TO IMPLEMENT****
       */
        [TestMethod]
        public void AdminFunctionalTest()
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
                // Assert that method has failed test
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ChooseLogDirectory
                admin.ChooseLogDirectory();
            }
            catch (Exception e)
            {
                // Assert that method has failed test
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ModifyCarrierData
                admin.ModifyCarrierData();
            }
            catch (Exception e)
            {
                // Assert that method has failed test
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ModifyFeeTable
                admin.ModifyFeeTable();
            }
            catch (Exception e)
            {
                // Assert that method has failed test
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Call ViewLogFiles
                admin.ViewLogFile();
            }
            catch (Exception e)
            {
                // Assert that method has failed test
                throw new AssertFailedException();
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
                Assert.AreEqual("Invalid Distance", e.Message);
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
                Assert.AreEqual("Invalid City", e.Message);
            }
        }

        /**
        * \test 
        * Destination.SetCity Functional Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test functionality of the SetCity method in the Destination class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends a valid value to the SetCity method
        * 
        * \test <b>Type of Test:</b>
        * Functional
        * 
        * \test <b>Expected:</b>
        * No Exception catch (Cannot test as proper functionality has yet to be implemented)
        * 
        * \test <b>Actual:</b>
        * ****CANNOT TEST AS YET TO IMPLEMENT****
        */
        [TestMethod]
        public void DestinationCityFunctionalTest()
        {
            //Variables
            var destination = new Destination();

            // Try Catch block to test the fail condition
            try
            {
                // Setting the city to an invalid amount
                destination.SetCity("");
            }
            catch (Exception e)
            {
                // Assert that test failed
                throw new AssertFailedException();
            }
        }

        /**
        * \test 
        * Destination.SetDistanceHours Boundary and Functional Test
        * 
        * \test <b>Purpose:</b>
        * This test is designed to test the boundary and functionality of the SetDistanceHours Setter method in the Destination class
        * 
        * \test <b>How Test Is Conducted:</b>
        * Sends a value on the boundary of a successful input to the methods. Sending (in order): 0.0001f, 0.00001f, 0.000001f, -0.0001f, -0.00001f, -0.000001f 
        * 
        * \test <b>Type of Test:</b>
        * Boundary and Functional
        * 
        * \test <b>Expected:</b>
        * Respectively (0.0001f, 0.00001f, 0.000001f, Exception, Exception, Exception)
        * 
        * \test <b>Actual:</b>
        * 
        */
        [TestMethod]
        public void DestinationHoursBoundaryTest()
        {
            //Variables
            var destination = new Destination();

            // Try Catch block to test the fail condition
            try
            {
                // Testing for 0.0001f
                destination.SetDistanceHours(0.0001f);
                Assert.AreEqual(0.0001f, destination.GetDistanceHours());

                // Testing for 0.00001f
                destination.SetDistanceHours(0.00001f);
                Assert.AreEqual(0.00001f, destination.GetDistanceHours());

                // Testing for 0.000001f
                destination.SetDistanceHours(0.000001f);
                Assert.AreEqual(0.000001f, destination.GetDistanceHours());
            }
            catch (Exception e)
            {
                // Assert that test failed
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Testing for -0.0001f
                destination.SetDistanceHours(-0.0001f);
            }
            catch (Exception e)
            {
                // Assert that test succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Testing for -0.00001f
                destination.SetDistanceHours(-0.00001f);
            }
            catch (Exception e)
            {
                // Assert that test succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Testing for -0.000001f
                destination.SetDistanceHours(-0.000001f);
            }
            catch (Exception e)
            {
                // Assert that test succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }
        }

        /**
       * \test 
       * Destination.SetDistanceKm Boundary and Functional Test
       * 
       * \test <b>Purpose:</b>
       * This test is designed to test the boundary and functionality of the SetDistanceKm Setter method in the Destination class
       * 
       * \test <b>How Test Is Conducted:</b>
       * Sends a value on the boundary of a successful input to the methods. Sending (in order): 0.0001f, 0.00001f, 0.000001f, -0.0001f, -0.00001f, -0.000001f 
       * 
       * \test <b>Type of Test:</b>
       * Boundary and Functional
       * 
       * \test <b>Expected:</b>
       * Respectively (0.0001f, 0.00001f, 0.000001f, Exception, Exception, Exception)
       * 
       * \test <b>Actual:</b>
       * 
       */
        [TestMethod]
        public void DestinationKmBoundaryTest()
        {
            //Variables
            var destination = new Destination();

            // Try Catch block to test the fail condition
            try
            {
                // Testing for 0.0001f
                destination.SetDistanceKm(0.0001f);
                Assert.AreEqual(0.0001f, destination.GetDistanceHours());

                // Testing for 0.00001f
                destination.SetDistanceKm(0.00001f);
                Assert.AreEqual(0.00001f, destination.GetDistanceHours());

                // Testing for 0.000001f
                destination.SetDistanceKm(0.000001f);
                Assert.AreEqual(0.000001f, destination.GetDistanceHours());
            }
            catch (Exception e)
            {
                // Assert that test failed
                throw new AssertFailedException();
            }

            // Try Catch block to test the fail condition
            try
            {
                // Testing for -0.0001f
                destination.SetDistanceKm(-0.0001f);
            }
            catch (Exception e)
            {
                // Assert that test succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Testing for -0.00001f
                destination.SetDistanceKm(-0.00001f);
            }
            catch (Exception e)
            {
                // Assert that test succeeded
                Assert.AreEqual("Invalid Distance", e.Message);
            }

            // Try Catch block to test the fail condition
            try
            {
                // Testing for -0.000001f
                destination.SetDistanceKm(-0.000001f);
            }
            catch (Exception e)
            {
                // Assert that test succeeded
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
    }
}
