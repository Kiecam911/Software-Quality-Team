using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// \var data member _CarrierDepots <i>List Depot</i> - <i>private<i> The list of <b>Depots</b> associated with the carrier
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
        private List<Depot> _CarrierDepots;
        public List<Depot> CarrierDepots
        {
            get { return _CarrierDepots; }
            set { _CarrierDepots = value; }
        }
        

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
            DestinationCity = "";
            _FTLAvailability = 0;
            _LTLAvailability = 0;
            _FTLRate = 0.000;
            _LTLRate = 0.000;
            _ReefCharge = 0.000;
        }

    }
}
