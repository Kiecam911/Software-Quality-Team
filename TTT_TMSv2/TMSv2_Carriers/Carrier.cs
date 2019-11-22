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
        private float perPalletLTLRate;
        private bool isReefer;
        private float carrierCapacity;

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

        public void setPerPalletLTLRate(float newRate)
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

        public void setCarrierCapacity(float newCap)
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

        public float getPerPalletFTLRate()
        {
            return perPalletLTLRate;
        }

        public bool getIsReefer()
        {
            return isReefer;
        }

        public float getCarrierCapacity()
        {
            return carrierCapacity;
        }
    }
}
