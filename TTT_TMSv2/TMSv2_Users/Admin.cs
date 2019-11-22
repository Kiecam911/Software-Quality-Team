using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSv2_Users
{
    /// 
    /// \class Admin : User
    ///
    /// \brief The purpose of this class is to simulate the "Admin" actor
    /// \details <b>Details</b>
    ///
    /// This class represents the "Admin" actor and its capabilities. This class is able to access general configuration options for the 
    /// Transport Management System(TMS), such as selecting directories for log files, targeting IP address and ports for all Database 
    /// Management Systems communications, etc. It is also able to alter the Rate/fee tables, carrier Data, Route Table, review log files
    /// within the application, and can initiate a file backup specifying the directory for the backup files.
    ///
    /// \var data member orderId <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa User
    ///
    public class Admin : User
    {
        ///
        /// \fn Admin()
        /// 
        /// \brief To instantiate a new Admin
        /// \details <b>Details</b>
        ///
        /// Instantiates the Admin class by setting the permission level
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Admin class, nothing is returned
        ///
        public Admin()
        {
            PermissionLevel = PERMISSION_ADMIN;
        }

        ///
        /// \fn ChooseLogDirectory()
        /// 
        /// \brief Choose the directory to save the log files
        /// \details <b>Details</b>
        ///
        /// Chooses the directory in which to save the log files
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Nothing is returned
        ///
        public void ChooseLogDirectory()
        {
            throw new Exception("Invalid Directory");
        }

        ///
        /// \fn ViewLogFile()
        /// 
        /// \brief Views the Log files in-app
        /// \details <b>Details</b>
        ///
        /// Opens the Log files and displays them in the application for evaluation
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Nothing is returned
        ///
        public void ViewLogFile()
        {
            throw new Exception("Invalid Log Files");
        }

        ///
        /// \fn ModifyFeeTable()
        /// 
        /// \brief Modifies the fee table
        /// \details <b>Details</b>
        ///
        /// Allows for modification of the Rate/Fee table in the application
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Nothing is returned
        ///
        public void ModifyFeeTable()
        {
            throw new Exception("Cannot Modify Fee Table");
        }

        ///
        /// \fn ModifyCarrierData()
        /// 
        /// \brief Modifies the <b>Carrier's</b> data
        /// \details <b>Details</b>
        ///
        /// Allows for modification of the <b>Carrier's</b> table in the application
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Nothing is returned
        ///
        public void ModifyCarrierData()
        {
            throw new Exception("Cannot Modify Carrier Data");
        }

        ///
        /// \fn BackupData()
        /// 
        /// \brief Backup the database to a local directory
        /// \details <b>Details</b>
        ///
        /// Specify and initiate the backup of the database to a local directory
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this function
        ///
        /// \return Nothing is returned
        ///
        public void BackupData()
        {
            throw new Exception("Cannot Backup Data");
        }
    }
}
