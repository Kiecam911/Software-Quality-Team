using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMSv2_Logging
{
    /// 
    /// \class Logger
    ///
    /// \brief The purpose of this class is to provide methods for all purposes relating to system logging.
    /// 
    /// \details <b>Details</b>
    ///
    /// This class includes wmethods for riting a message to a file with a timestamp, and changing the directory where
    /// log files are written to.
    /// 
    /// \var data member filePath <i>string</i> - <i>private<i> the path where the logger currently writes to
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    ///
    public static class Logger
    {
        private static string filePath = "";



        ///
        /// \fn ChangeLogDirectory()
        /// 
        /// \brief To change the directory that log files are written to
        /// \details <b>Details</b>
        ///
        /// Opens a folder selector dialog box and saves the selected folder path to filePath
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this method
        ///
        /// \return <b>bool</b> - bool representing whether filePath was successfully changed or not
        ///
        public static bool ChangeLogDirectory()
        {
            string tmpPath = filePath;
            try
            {
                // open folder selector and set path to filePath
                FolderBrowserDialog directorySelector = new FolderBrowserDialog();
                directorySelector.ShowDialog();
                filePath = directorySelector.SelectedPath + "\\";
                return true;
            }
            catch (Exception e)
            {
                // reset file path to default and log if error occurs
                filePath = tmpPath;
                LogToFile(e.Message);
                return false;
            }

        }



        ///
        /// \fn LogToFile()
        /// 
        /// \brief Writes text to the log file
        /// \details <b>Details</b>
        ///
        /// Takes the text parameter and appends it to the file log.txt in the current filePath directory
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this method
        ///
        /// \return nothing <b>void</b> - Nothing is returned from this method
        ///
        public static void LogToFile(string text)
        {
            StreamWriter fileWriter = File.AppendText(filePath + "log.txt");
            fileWriter.WriteLine(DateTime.Now.ToString() + " - " + text);
            fileWriter.Close();
        }
    }
}
