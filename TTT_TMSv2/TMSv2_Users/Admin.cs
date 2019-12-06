using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Text;
using System.Configuration;
using System.IO;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_Order;
using TMSv2_TripPlanner;

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
        // Data members
        private static string _LogFileDirectory { get; set; }
        public string LogFileDirectory
        {
            get { return _LogFileDirectory; }
            set { _LogFileDirectory = value; }
        }
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
            string value = ConfigurationManager.AppSettings["LogFileDirectory"];
            PermissionLevel = PERMISSION_ADMIN;

            //Sets the LogFileDirectory path to a relative path based on the current directory
            if (_LogFileDirectory == null)
            {
                try
                {
                    if (false == Path.IsPathRooted(value))
                    {
                        LogFileDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), value));
                    }
                    else
                    {
                        Path.GetDirectoryName(value);
                        LogFileDirectory = value;
                    }
                }
                catch (ArgumentException e)
                {
                    LogFileDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\"));
                }
            }
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
        public string ChooseLogDirectory()
        {
            //Variables
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string path = null;

            //Sets the starting directory to the directory of the log files
            dialog.SelectedPath = _LogFileDirectory;
            dialog.Description = "Select the Directory for the Log Files";

            //Checks if the dialogResult returned OK; if so then set logDirectory path to the new selected path
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }

            return path;
        }

        public bool ChooseLogDirectory(string path)
        {
            //Open config file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            try
            {
                //If Path is relative get full path and set LogFileDirectory to said path else set it to path
                if (false == Path.IsPathRooted(path))
                {
                    LogFileDirectory = Path.GetFullPath(Path.Combine(_LogFileDirectory, path));
                }
                else
                {
                    Path.GetDirectoryName(path);
                    LogFileDirectory = path;
                }

            }
            catch(ArgumentException e)
            {
                return false;
            }

            //Save the new path to the config file
            config.AppSettings.Settings["LogFileDirectory"].Value = LogFileDirectory;
            config.Save();

            return true;
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
        public string ViewLogFile()
        {
            //Variables
            OpenFileDialog openDialog = new OpenFileDialog();
            string retString = null;

            //Setsup the Dialog box
            openDialog.InitialDirectory = _LogFileDirectory;
            openDialog.Title = "Select Log File";
            openDialog.AddExtension = true;
            openDialog.Filter = "Text Document (*.txt)|*.txt";
            openDialog.DefaultExt = ".txt";
            openDialog.RestoreDirectory = false;

            //If the dialog boxes is open then
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                //Set the text filename to the return string
                retString = openDialog.FileName;
            }

            return retString;
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
