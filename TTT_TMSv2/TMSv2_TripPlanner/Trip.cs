using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_TripPlanner
{
    /// 
    /// \class Trip
    ///
    /// \brief The purpose of this class is to 
    /// 
    /// \details <b>Details</b>
    ///
    /// This class represents the Trips that are assigned to the <b>Carrier</b> for each <b>Order</b>
    /// 
    /// \var data member endDistance <i>string</i> - <i>private<i> data member that is the designates the end destination of an <b>Order</b>
    /// \var data member kmDistance <i>float</i> - <i>private<i> data member that holds the distance to the end destination
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Carrier
    /// \sa Destination
    ///
    public class Trip
    {
        // Private Data members
        private string endDestination;              /// The ending destination for the <b>Order</b>
        private float kmDistance;                     /// The kilometer distance from the end destination

        ///
        /// \fn Trip()
        /// 
        /// \brief To instantiate a new Trip
        /// \details <b>Details</b>
        ///
        /// Instantiates the Trip class by setting the endDestination and kmDistance
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Trip class, nothing is returned
        ///
        public Trip()
        {
            endDestination = "";
            kmDistance = -1.0f;
        }

        ///
        /// \fn SetEndDestination(string city)
        /// 
        /// \brief Sets the city to the endDestination
        /// \details <b>Details</b>
        ///
        /// If city is not one of the cities in the <b>Order</b> then it does not set city to endDestination
        ///
        /// \param city <b>string</b> - the final destination to set
        ///
        /// \return Nothing is returned
        ///
        public void SetEndDestination(string city)
        {
            throw new Exception("Invalid City");
        }

        ///
        /// \fn SetKmDistance(float km)
        /// 
        /// \brief Sets the km to the kmDistance
        /// \details <b>Details</b>
        ///
        /// If km is not greater than 0 then it does not set km to kmDistance
        ///
        /// \param km <b>float</b> - the km distance to the end destination
        ///
        /// \return Nothing is returned
        ///
        public void SetKmDistance(float km)
        {
            if(km > 0)
            {
                kmDistance = km;
            }
            else
            {
                throw new Exception("Invalid Distance");
            }
        }

        ///
        /// \fn GetEndDestination()
        /// 
        /// \brief Gets the endDestination
        /// \details <b>Details</b>
        ///
        /// Returns the endDestination
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Returns the endDestination
        ///
        public string GetEndDestination()
        {
            return endDestination;
        }

        ///
        /// \fn GetKmDistance()
        /// 
        /// \brief Gets the kmDistance
        /// \details <b>Details</b>
        ///
        /// Returns the kmDistance
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Returns the kmDistance
        ///
        public float GetKmDistance()
        {
            return kmDistance;
        }

    }
}
