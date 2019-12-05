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
    /// \var data member DestinationCity <i>string</i> - <i>public<i> The destination city of the <b>Carrier</b>
    /// \var data member _FTLAvailability <i>int</i> - <i>private<i> The availability for Full Truck Load transportation provided by the <b>Carrier</b>
    /// \var data member _LTLAvailability <i>int</i> - <i>private<i> The availability for Less Than Load transportation provided by the <b>Carrier</b>
    /// \var data member _FTLRate <i>double</i> - <i>private<i> The rate for Full Truck Load transportation provided by the <b>Carrier</b>
    /// \var data member _LTLRate <i>double</i> - <i>private<i> The rate for Less Than Load transportation provided by the <b>Carrier</b>
    /// \var data member _ReefCharge <i>double</i> - <i>private<i> The charge rate for using a refrigerated Contained provided by the <b>Carrier</b>
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
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
        public string DestinationCity { get; set; }     /// The destination city of the <b>Carrier</b>
        private int _FTLAvailability;                   /// The availability of Full Truck Load for the <b>Carrier</b>
        public int FTLAvailability                      /// The public accessor for the _FTLAvailability
        {
            get { return _FTLAvailability; }
            set
            {
                if(value >= 0)
                {
                    _FTLAvailability = value;
                }
            }
        }
        private int _LTLAvailability;                   /// The availability of the Less Than Load for the <b>Carrier</b>
        public int LTLAvailability                      /// The public accessor for the _LTLAvailability
        {
            get { return _LTLAvailability; }
            set
            {
                if (value >= 0)
                {
                    _LTLAvailability = value;
                }
            }
        }
        private double _FTLRate;                        /// The <b>Carrier's</b> rates for Full Truck Load transportation
        public double FTLRate                           /// The public accessor for the _FTLRate
        {
            get { return _FTLRate; }
            set
            {
                if(value >= 0.0000)
                {
                    _FTLRate = value;
                }
            }
        }
        private double _LTLRate;                        /// The <b>Carrier's</b> rates for Less Than Load transportation
        public double LTLRate                           /// The public accessor for the _LTLRate
        {
            get { return _LTLRate; }
            set
            {
                if (value >= 0.0000)
                {
                    _LTLRate = value;
                }
            }
        }
        private double _ReefCharge;                     /// The rate to use a refrigerated Container from the <b>Carrier</b>
        public double ReefCharge                        /// The public accessor for the _ReefCharge
        {
            get { return _ReefCharge; }
            set
            {
                if (value >= 0.0000)
                {
                    _ReefCharge = value;
                }
            }
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
