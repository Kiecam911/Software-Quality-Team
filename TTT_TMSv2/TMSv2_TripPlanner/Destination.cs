using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_TripPlanner
{
    /// 
    /// \class Destination
    ///
    /// \brief The purpose of this class is to 
    /// \details <b>Details</b>
    ///
    /// This class represents the 
    /// 
    /// \var data member city <i>string</i> - <i>private<i> data member that holds the final city the <b>Order</b> will reach
    /// \var data member distanceKm <i>float</i> - <i>private<i> data member that holds the distance in KM
    /// \var data member dsitanceHours <i>float</i> - <i>private<i> data member that holds the distance in hours
    /// \var data member westDest <i>Destination</i> - <i>private<i> data member that contains a reference to the destination to the west
    /// \var data member eastDest <i>Destination</i> - <i>private<i> data member that contains a reference to the destination to the east
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \see Trip
    ///
    public class Destination
    {
        // Private data members
        private string city;                    /// 
        private float distanceKm;                 /// 
        private float distanceHours;
        private Destination westDest { get; }
        private Destination eastDest { get; }

        public Destination()
        {
            city = "";
            distanceHours = -1.0f;
            distanceKm = -1.0f;
            westDest = null;
            eastDest = null;
        }

        ///
        /// \fn SetCity(string c)
        /// 
        /// \brief Sets c to the city
        /// \details <b>Details</b>
        ///
        /// If 
        ///
        /// \param c <b>string</b> - the city to set
        ///
        /// \return Nothing is returned
        ///
        public void SetCity(string c)
        {
            throw new Exception("Invalid City");
        }

        ///
        /// \fn SetDistanceKm(float km)
        /// 
        /// \brief Sets km to the kmDistance
        /// \details <b>Details</b>
        ///
        /// If 
        ///
        /// \param km <b>float</b> - the km to set
        ///
        /// \return Nothing is returned
        ///
        public void SetDistanceKm(float km)
        {
            if (km > 0.0f)
            {
                distanceKm = km;
            }
            else
            {
                throw new Exception("Invalid Distance");
            }
        }

        ///
        /// \fn SetDistanceHours(float hrs)
        /// 
        /// \brief Sets hrs to the distanceHours
        /// \details <b>Details</b>
        ///
        /// If 
        ///
        /// \param hrs <b>float</b> - the hours to set
        ///
        /// \return Nothing is returned
        ///
        public void SetDistanceHours(float hrs)
        {
            if (hrs > 0.0f)
            {
                distanceHours = hrs;
            }
            else
            {
                throw new Exception("Invalid Distance");
            }
        }

        ///
        /// \fn GetCity()
        /// 
        /// \brief Gets the city
        /// \details <b>Details</b>
        ///
        /// Returns the city
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Returns the city
        ///
        public string GetCity()
        {
            return city;
        }

        ///
        /// \fn GetDistanceKm()
        /// 
        /// \brief Gets the distanceKm
        /// \details <b>Details</b>
        ///
        /// Returns the distanceKm
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Returns the distanceKm
        ///
        public float GetDistanceKm()
        {
            return distanceKm;
        }

        ///
        /// \fn GetDistanceHours()
        /// 
        /// \brief Gets the distanceHours
        /// \details <b>Details</b>
        ///
        /// Returns the distanceHours
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Returns the distanceHours
        ///
        public float GetDistanceHours()
        {
            return distanceHours;
        }
    }
}
