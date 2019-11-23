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
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public class Depot
    {
        private int depotID
        {
            get;
        }
        private List<int> associatedCarriers;               /// list of carrierIDs associated with this depot
        private string City                                 /// name of the city the depot is in
        {
            get; set;
        }



        ///
        /// \brief Returns all associated carriers
        /// \details <b>Details</b>
        ///
        /// 
        /// This method returns a list of integers corresponding to the IDs of 
        /// all carriers that are owned by the respective depot
        ///
        /// \param nothing <b>void</b> - None
        ///
        /// \return List<int> containing the carrierIDs
        ///
        public List<int> GetAssociatedCarriers()
        {
            throw new Exception("No carriers");
        }



        ///
        /// \brief Assigns a carrier to a depot
        /// \details <b>Details</b>
        ///
        /// 
        /// This method takes the carrierID parameter and assigns
        /// it to the depot denoted by the depotID parameter.
        ///
        /// \param nothing <b>void</b> - None
        ///
        /// \return List<int> containing the contract IDs
        ///
        public void AssignCarrier(int carrerID, int depotID)
        {
            throw new Exception("Invalid carrier");
        }
    }
}
