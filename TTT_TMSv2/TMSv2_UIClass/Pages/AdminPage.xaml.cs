using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Configuration;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMSv2_Users;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_TripPlanner;
using TMSv2_Order;
using TMSv2_Logging;
using TMSv2_DAL;

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
            Carrier tempCarrier = new Carrier();
            List<Carrier> carriers = tempCarrier.GetCarriers();
            foreach (Carrier c in carriers)
            {
                tableSelect.Items.Add(c.CarrierName);
            }
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
            //Logger.ChangeLogDirectory();
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

            //Load the current database access information into the text boxes
            currIP.Text = ConfigurationManager.AppSettings["DatabaseIP"];
            currDBName.Text = ConfigurationManager.AppSettings["DatabaseName"];
            currUsername.Text = ConfigurationManager.AppSettings["DatabaseUsername"];
            currPassword.Text = ConfigurationManager.AppSettings["DatabasePassword"];

            //Clear previous data from the boxes
            newIP.Text = "";
            newDBName.Text = "";
            newUsername.Text = "";
            newPassword.Text = "";
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

            //Reset Alter Table Grids
            AlterTableGrid.Visibility = Visibility.Hidden;
            AlterRoutesGrid.Visibility = Visibility.Hidden;
            AlterRatesGrid.Visibility = Visibility.Hidden;
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

        ///
        /// \fn newDBSettings_Click(object sender, RoutedEventArgs e)
        /// 
        /// \brief The Button Event Handler for the Save Settings Button
        /// \details <b>Details</b>
        ///
        /// Clicking the Save Settings button will trigger this event handler and cause the text in the
        /// new database settings to be saved to the config file before checking and reporting a success
        /// or failure result (and reverting back to the old settings upon failure)
        ///
        /// \param sender <b>object</b> - The Object that is triggering the event
        /// \param e <b>RoutedEventArgs</b> - The Event that is being triggered
        ///
        /// \return Nothing is returned
        /// 
        /// \sa resetView()
        ///
        private void newDBSettings_Click(object sender, RoutedEventArgs e)
        {
            //Get the instance of the database access class
            DataAccess temp = DataAccess.Instance();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //Save the new Database settings to the config file
            config.AppSettings.Settings["DatabaseIP"].Value = newIP.Text;
            config.AppSettings.Settings["DatabaseName"].Value = newDBName.Text;
            config.AppSettings.Settings["DatabaseUsername"].Value = newUsername.Text;
            config.AppSettings.Settings["DatabasePassword"].Value = newPassword.Text;
            config.Save();

            if(temp.ConnectToDatabase() == false)
            {
                //Display failure result
                MessageBox.Show("New Settings are invalid and does not connect to the database. Changing back to previous settings", "Alert!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                //Save old settings back to config file
                config.AppSettings.Settings["DatabaseIP"].Value = currIP.Text;
                config.AppSettings.Settings["DatabaseName"].Value = currDBName.Text;
                config.AppSettings.Settings["DatabaseUsername"].Value = currUsername.Text;
                config.AppSettings.Settings["DatabasePassword"].Value = currPassword.Text;
                config.Save();
            }
            else
            {
                //Display success result
                MessageBox.Show("Database Connected!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

                //Close open Database connection
                temp.CloseConnection();

                //Reset the view
                resetView();
            }
        }

        private void AlterTable_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            AlterTableGrid.Visibility = Visibility.Visible;
        }

        private void copyPathButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(admin.LogFileDirectory);
        }

        private void TableSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Variables
            Carrier tempCarrier = new Carrier();
            List<Carrier> carriers = tempCarrier.GetCarriers();

            if (tableSelect.SelectedItem != null)
            {
                foreach (Carrier c in carriers)
                {
                    if((string)tableSelect.SelectedItem == c.CarrierName)
                    {
                        tableView.ItemsSource = c.CarrierDepots;
                    }
                }
            }

            tableView.HeadersVisibility = DataGridHeadersVisibility.All;

            tableView.Items.Refresh();
        }

        private void AlterRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            Routes destination = new Routes();
            List<Routes> routeTable = destination.GetRoutes();

            //Reset view
            resetView();

            //Set datagrid itemssource to routeTable
            tableRouteView.ItemsSource = routeTable;

            //Set Grid to visible
            AlterRoutesGrid.Visibility = Visibility.Visible;

            //Set headers to visible
            tableRouteView.HeadersVisibility = DataGridHeadersVisibility.All;

            //Refresh items
            tableRouteView.Items.Refresh();
        }

        private void SaveCarrierData_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            Carrier tempCarrier = new Carrier();
            List<Carrier> carriers = tempCarrier.GetCarriers();

            if (tableSelect.SelectedItem != null)
            {
                foreach (Carrier c in carriers)
                {
                    if ((string)tableSelect.SelectedItem == c.CarrierName)
                    {
                        c.CarrierDepots = tableView.ItemsSource as List<Depot>;
                    }
                }
            }

            if(!tempCarrier.UpdateCarriers(carriers))
            {
                MessageBox.Show("Could Not Update the Database.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Changes Saved!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //Refresh view of items in datagrid
            tableView.Items.Refresh();
        }

        private void RouteSave_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            Routes destination = new Routes();
            List<Routes> routeTable = tableRouteView.ItemsSource as List<Routes>;

            if (!destination.UpdateRoutesTable(routeTable))
            {
                MessageBox.Show("Could Not Update the Database.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Changes Saved!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //Refresh view of items in datagrid
            tableRouteView.Items.Refresh();

        }

        private void AlterRatesButton_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            OSHTRates tempRates = new OSHTRates();
            List<OSHTRates> rates = new List<OSHTRates>();

            //Add rates to list
            rates.Add(tempRates.GetRates());

            //Reset view
            resetView();

            //Set datagrid itemssource to rates
            tableRatesView.ItemsSource = rates;

            //Set Grid to visible
            AlterRatesGrid.Visibility = Visibility.Visible;

            //Set headers to visible
            tableRatesView.HeadersVisibility = DataGridHeadersVisibility.All;

            //Refresh items
            tableRatesView.Items.Refresh();
        }

        private void SaveRates_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            OSHTRates rates = new OSHTRates();
            List<OSHTRates> ratesTable = tableRatesView.ItemsSource as List<OSHTRates>;

            if (!rates.UpdateRates(ratesTable[0]))
            {
                MessageBox.Show("Could Not Update the Database.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Changes Saved!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //Refresh view of items in datagrid
            tableRatesView.Items.Refresh();
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess da = DataAccess.Instance();

            if(!da.FullDatabaseBackup())
            {
                MessageBox.Show("There was an error in Creating the backupfiles!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Database has successfully backed up!", "Alert!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
