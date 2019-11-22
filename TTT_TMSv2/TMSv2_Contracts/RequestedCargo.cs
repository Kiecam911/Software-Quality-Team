using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Contracts
{
    /// 
    /// \class RequestedCargo
    ///
    /// \brief The purpose of this class is to 
    /// \details <b>Details</b>
    ///
    /// This class represents the 
    /// 
    /// \var data member cargoVolume <i>double</i> - <i>private<i> data member that holds the volume of the cargo that is to be carried by the <b>Carrier</b>
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    ///
    public class RequestedCargo
    {
        // Private Data members
        private double cargoVolume;                 /// The volume of the cargo to be transported

        ///
        /// \fn RequestedCargo()
        /// 
        /// \brief To instantiate a new Cargo Class
        /// \details <b>Details</b>
        ///
        /// Instantiates the RequestedCargo class by setting the cargoVolume to -1 as default
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the REquestedCargo class, nothing is returned
        ///
        public RequestedCargo()
        {
            cargoVolume = -1.0;
        }

        ///
        /// \fn SetCargoVolume(double newVol)
        /// 
        /// \brief Sets the newVol to the cargoVolume
        /// \details <b>Details</b>
        ///
        /// If newVol is greater than 0 then set the newVol to cargoVolume
        ///
        /// \param newvol <b>double</b> - the new cargo volume to set
        ///
        /// \return Nothing is returned
        ///
        public void SetCargoVolume(double newVol)
        {
            if(newVol > 0)
            {
                cargoVolume = newVol;
            }
            else
            {
                throw new Exception("Invalid Cargo Volume");
            }
        }

        ///
        /// \fn GetCargoVolume()
        /// 
        /// \brief Gets the cargoVolume
        /// \details <b>Details</b>
        ///
        /// Returns the cargoVolume
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Returns the cargoVolume
        ///
        public double GetCargoVolume()
        {
            return cargoVolume;
        }
    }
}
