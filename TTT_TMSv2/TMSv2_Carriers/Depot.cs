using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Carriers
{
    /// 
    /// \class Depot
    ///
    /// \brief The purpose of this class is to obtain depot info from a file and model each depot accordingly
    /// \details <b>Details</b>
    ///
    /// This class contains the attributes and methods necessary to perform transactions and utilize
    /// contracts within the system. A <b>Contract</b> is instantiated upon retrieving contract data from the
    /// contract marketplace, and the data members of the contract are filled in according to the data received.
    /// 
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
    public class Depot
    {
        private int _CarrierInfoID;                   /// The identification number of the carrierInfo table
        public int CarrierInfoID                      /// The public accessor for the _CarrierInfoID
        {
            get { return _CarrierInfoID; }
            set
            {
                if (value >= 0)
                {
                    _CarrierInfoID = value;
                }
            }
        }
        public string DestinationCity { get; set; }     /// The destination city of the <b>Carrier</b>
        private int _FTLAvailability;                   /// The availability of Full Truck Load for the <b>Carrier</b>
        public int FTLAvailability                      /// The public accessor for the _FTLAvailability
        {
            get { return _FTLAvailability; }
            set
            {
                if (value >= 0)
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
                if (value >= 0.0000)
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

        public Depot()
        {
            DestinationCity = "";
            _FTLAvailability = 0;
            _LTLAvailability = 0;
            _FTLRate = 0.0;
            _LTLRate = 0.0;
            _ReefCharge = 0.0;
        }

    }
}
