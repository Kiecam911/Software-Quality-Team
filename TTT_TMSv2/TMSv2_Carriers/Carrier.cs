using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Carriers
{
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
