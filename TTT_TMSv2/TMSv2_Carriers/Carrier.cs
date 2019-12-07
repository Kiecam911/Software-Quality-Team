using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_DAL;

namespace TMSv2_Carriers
{
    /// 
    /// \class Carrier
    ///
    /// \brief The purpose of this class is to track certain metrics about the carriers themselves <b>Details</b>
    ///
    /// This class contains the attributes and methods related to a carrier. Customers serve to be the owner of
    /// their respective contracts, and contain info related to each customer that has ever had orders taken from
    /// in the system. <b>Customer</b> contains methods to assign contracts as well as get all associated contracts.
    /// 
    /// \var data member _CarrierID <i>int</i> - <i>private<i> The identification number for the <b>Carrier</b>
    /// \var data member CarrierName <i>string</i> - <i>public<i> The <b>Carrier's</b> company/personal name (can be null if no name is provided)
    /// \var data member CarrierDepots <i>List Depot</i> - <i>public<i> The list of <b>Depots</b> associated with the carrier
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Depot
    ///
    public class Carrier
    {
        private int _CarrierID;                         /// The identification number for the carrier
        public int CarrierID                            /// The public accessor for the _CarrierID
        {
            get { return _CarrierID; }
            set
            {
                if(value >= 0)
                {
                    _CarrierID = value;
                }
            }
        }
        public string CarrierName { get; set; }         /// The carrier's company/personal name
        public List<Depot> CarrierDepots { get; set; } /// 
        

        ///
        /// \fn Carrier()
        /// 
        /// \brief To instantiate a new Carrier object
        /// \details <b>Details</b>
        ///
        /// Instantiates the Carrier's data members to 0, null, or false as default placeholders for data
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Carrier class, nothing is returned
        ///
        public Carrier()
        {
            _CarrierID = 0;
            CarrierName = "";
            CarrierDepots = new List<Depot>();
        }

        ///
        /// \brief To retrieve details for the new contract from the contract marketplace
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the contract marketplace database to populate the variables
        /// related to the contract marketplace. 
        ///
        /// \param <b>void</b> - None
        ///
        /// \return Nothing
        ///
        public List<Carrier> GetCarriers()
        {
            // create objects to hold information from DAL
            DataAccess tempDA = new DataAccess();
            DataTable carrierTable = tempDA.GetCarrierData().Tables[0];
            List<Carrier> carrierList = new List<Carrier>();
            int ID = 0;
            string name = "";
            Carrier tempCarrier = null;
            Depot depots = null;

            DataRowCollection contractRows = carrierTable.Rows;

            // loop through each row of data, creating contract and assigning values to it
            foreach (DataRow currentRow in contractRows)
            {

                //Get Carrier Data
                ID = currentRow.Field<int>(0);
                name = currentRow.Field<string>(1);

                //Check if carrier is null or it is a different carrier to instantiate a new Carrier otherwise add depot to current carrier
                if (tempCarrier != null)
                {
                    if (tempCarrier.CarrierID != ID)
                    {
                        tempCarrier = new Carrier();
                    }
                }
                else
                {
                    //Instantiate new objects
                    tempCarrier = new Carrier();
                }
                depots = new Depot();

                //Fill Carrier with carrier data (if it is the same carrier instance then the change won't make a difference)
                tempCarrier.CarrierID = ID;
                tempCarrier.CarrierName = name;

                //Get Depot data
                depots.DestinationCity = currentRow.Field<string>(2);
                depots.FTLAvailability = currentRow.Field<int>(3);
                depots.LTLAvailability = currentRow.Field<int>(4);
                depots.FTLRate = currentRow.Field<double>(5);
                depots.LTLRate = currentRow.Field<double>(6);
                depots.ReefCharge = currentRow.Field<double>(7);

                //Add Depots to the carrier
                tempCarrier.CarrierDepots.Add(depots);

                //Check if the carrier is already in the list; if so then do NOT add to list otherwise do
                if (carrierList.Count == 0)
                {
                    carrierList.Add(tempCarrier);
                }
                else
                {
                    if (carrierList[carrierList.Count - 1].CarrierID != tempCarrier.CarrierID)
                    {
                        carrierList.Add(tempCarrier);
                    }
                }
            }
            return carrierList;
        }

    }
}
