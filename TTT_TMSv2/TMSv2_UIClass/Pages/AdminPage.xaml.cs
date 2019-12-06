using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMSv2_Users;
using TMSv2_TripPlanner;
using TMSv2_Order;
using TMSv2_Logging;

namespace TMSv2_UIClass.Pages
{
    /// 
    /// \class AdminPage : Page
    ///
    /// \brief The purpose of this class is to maintain and interface between the navigations and section
    /// visibilities for the Admin Page. As well as facilitate the Admin Class's Methods.
    /// \details <b>Details</b>
    ///
    /// This class contains the navigational/visiblity button events for the AdminPage.xaml document and facilitates the use of the methods
    /// within the <b>Admin</b> class. Such methods like ChooseLogDirectory, ViewLogFiles, ModifyCarrierData, and BackupData are to be used
    /// in this class.
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa Order
    /// \sa Admin
    /// \sa ChooseLogDirectory()
    /// \sa ViewLogFiles()
    /// \sa ModifyCarrierData()
    /// \sa BackupData()
    ///
    public partial class AdminPage : Page
    {
        private Admin admin;
        public AdminPage()
        {
            InitializeComponent();
            admin = new Admin();
        }

        ///
        /// \fn HomeButton_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the HomeButton
        /// \details <b>Details</b>
        ///
        /// Clicking the HomeButton will trigger this event handler and cause the user to return back to the home screen
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        ///
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Find the frame.
            Frame frame = null;
            DependencyObject parent = VisualTreeHelper.GetParent(this);

            // Cycles through to MainWindow frame
            while (parent != null && frame == null)
            {
                frame = parent as Frame;
                parent = VisualTreeHelper.GetParent(parent);
            }

            // Change the page of the frame.
            if (frame != null)
            {
                frame.Navigate(new MenuPage());
            }
        }

        ///
        /// \fn configButton_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the configButton
        /// \details <b>Details</b>
        ///
        /// Clicking the configButton will trigger this event handler and cause the screen to display
        /// the Configuration Settings view while hiding all other unnecessary grids and views. This
        /// view allows the user to edit the application's configuration settings.
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        /// 
        /// \sa resetView()
        ///
        private void configButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            ConfigGrid.Visibility = Visibility.Visible;
        }

        ///
        /// \fn setLogButton_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the LogButton
        /// \details <b>Details</b>
        ///
        /// Clicking the LogButton will trigger this event handler and cause the screen to display
        /// the View Log Files view while hiding all other unnecessary grids and views. This view
        /// allows the user to edit log files within the application.
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        /// 
        /// \sa resetView()
        ///
        private void setLogButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();

            ConfigGrid.Visibility = Visibility.Visible;
            LogFileGrid.Visibility = Visibility.Visible;

            currPathBox.Text = admin.LogFileDirectory;
            newPathBox.Text = "";
            Logger.ChangeLogDirectory();
        }

        ///
        /// \fn setDBMSInfoButton_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the SetDatabaseInfo Button found in the <b>configButton</b> view
        /// \details <b>Details</b>
        ///
        /// Clicking the "Set Database Info" button will trigger this event handler and cause the display
        /// the SetDatabaseInfo view while hiding all other unnecessary grids and views. This view allows
        /// the user to edit the databases' information such as the connection text string.
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        /// 
        /// \sa configButton_Click(object sender, RoutedEventArgs e)
        /// \sa resetView()
        ///
        private void setDBMSInfoButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();

            ConfigGrid.Visibility = Visibility.Visible;
            DBMSInfoScreen.Visibility = Visibility.Visible;
        }

        ///
        /// \fn resetView()
        /// 
        /// \brief The General ResetView method to avoid rewriting code needlessly
        /// \details <b>Details</b>
        ///
        /// This method is a private method that is meant to hide the displaying of the views and grid
        /// without having to needlessly rewrite the code
        ///
        /// \param nothing <b>none</b> - There is no parameter for this method
        ///
        /// \return Nothing is returned
        ///
        private void resetView()
        {
            //Reset configGrid
            ConfigGrid.Visibility = Visibility.Hidden;
            DBMSInfoScreen.Visibility = Visibility.Hidden;
            LogFileGrid.Visibility = Visibility.Hidden;

            //Reset logGrid
            ViewLogGrid.Visibility = Visibility.Hidden;
        }

        ///
        /// \fn LogButton_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the SetLogFileDirectory Button
        /// \details <b>Details</b>
        ///
        /// Clicking the SetLogFileDirectory button will trigger this event handler and cause the 
        /// screen to display the SetLogFileDirectory view while hiding all other unnecessary grids
        /// and views. This view allows the user to edit, within the application, the directory in 
        /// which the log files are saved.
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        /// 
        /// \sa resetView()
        ///
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            ViewLogGrid.Visibility = Visibility.Visible;
        }

        private void viewLogsButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName;

            if((fileName = admin.ViewLogFile()) != null )
            {
                LogDisplay.Text = File.ReadAllText(fileName);
            }
        }

        private void browseNewPath_Click(object sender, RoutedEventArgs e)
        {
            newPathBox.Text = admin.ChooseLogDirectory();
        }

        ///
        /// \fn newPathButton_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the SetNewPath Button
        /// \details <b>Details</b>
        ///
        /// Clicking the SetNewPath button will trigger this event handler and cause the text path
        /// in the newPathBox to update the LogFileDirectory string.
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        /// 
        /// \sa resetView()
        ///
        private void newPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (!admin.ChooseLogDirectory(newPathBox.Text))
            {
                MessageBox.Show("The Selected Path is Invalid.", "Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                resetView();
            }
        }
    }
}
