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
    /// \var data member associatedContacts <i>List<int></i> - <i>private<i> list containing ContractID of all associated contracts
    /// \var data member CustomerID <i>int</i> - <i>private<i> int containing unique identifier for the customer
    /// \var data member CustomerName <i>string</i> - <i>private<i> strnig containing the name of the customer
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public class Carrier
    {
        private int carrierID;
        private string carrierName;
        private bool isFTL;
        private double perPalletLTLRate;
        private bool isReefer;
        private double carrierCapacity;

        public Carrier()
        {
            carrierID = 000;
            carrierName = "";
            isFTL = false;
            perPalletLTLRate = 0;
            isReefer = false;
            carrierCapacity = 0;
        }

        public void setCarrierID(int newID)
        {
            carrierID = newID;
        }

        public void setCarrierName(string newName)
        {
            if (newName != "")
            {
                carrierName = newName;
            }
        }

        public void setIsFTL(bool state)
        {
            isFTL = state;
        }

        public void setPerPalletLTLRate(double newRate)
        {
            if (newRate >= 0)
            {
                perPalletLTLRate = newRate;
            }
        }

        public void setIsReefer(bool state)
        {
            isReefer = state;
        }

        public void setCarrierCapacity(double newCap)
        {
            if (newCap >= 0)
            {
                carrierCapacity = newCap;
            }
        }

        public int getCarrierID()
        {
            return carrierID;
        }

        public string getCarrierName()
        {
            return carrierName; 
        }

        public bool getIsFTL()
        {
            return isFTL;
        }

        public double getPerPalletFTLRate()
        {
            return perPalletLTLRate;
        }

        public bool getIsReefer()
        {
            return isReefer;
        }

        public double getCarrierCapacity()
        {
            return carrierCapacity;
        }
    }
}
