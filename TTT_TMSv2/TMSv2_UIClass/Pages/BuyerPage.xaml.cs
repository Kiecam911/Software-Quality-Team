using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

using TMSv2_Users;
using TMSv2_Order;
using TMSv2_DAL;
using TMSv2_Logging;

namespace TMSv2_UIClass.Pages
{
    
    /// <summary>
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Page
    {
        private Buyer CurrentBuyer;
        private Timer timer;
        string connectionString = "SERVER=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; PORT = 3306 ;" + "DATABASE=" + ConfigurationManager.AppSettings["DatabaseName"] + ";" + "UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + ";" + "PASSWORD=" + ConfigurationManager.AppSettings["DatabasePassword"] + ";";

        public BuyerPage()
        {
            InitializeComponent();
            CurrentBuyer = new Buyer();
        }

        private void newContract_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            loadNewContracts();
            newContractGrid.Visibility = Visibility.Visible;
            Timer timer = new Timer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = 10000;
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            loadNewContracts();
        }

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

        private void CurrentContractsButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            CurrentContractGrid.Visibility = Visibility.Visible;
            LoadCurrentContracts();
        }

        private void CompletedContractsButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            CompletedContractGrid.Visibility = Visibility.Visible;

            LoadCompleteContracts();
        }

        private void loadNewContracts()
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["contractMarketplace"].ConnectionString;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT * FROM Contract";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "*");
                NewContractsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = ds.Tables["*"] });
                connection.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("Database failed to load, please check your connection");
            }
        }

        private void LoadCurrentContracts()
        {
            try
            {
                
                MySqlConnection connection = new MySqlConnection(connectionString);
                string sqlCommand = "SELECT Contracts.ContractID, OrderID, Client_Name, Quantity, Origin, Destination FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.OrderID WHERE IsActive = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "*");
                currentContractsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = ds.Tables["*"] });
                connection.Close();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

        private void LoadCompleteContracts()
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(connectionString);
                string sqlCommand = "SELECT Contracts.ContractID, OrderID, Client_Name, Quantity, Origin, Destination FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.OrderID WHERE Completed = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Orders");
                completedContractsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = ds.Tables["Orders"] });
                connection.Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("Database failed to load, please check your connection");
            }
        }

        private void resetView()
        {
            //Reset New Contracts
            newContractGrid.Visibility = Visibility.Hidden;
            CurrentContractGrid.Visibility = Visibility.Hidden;
            CompletedContractGrid.Visibility = Visibility.Hidden;
            try
            {
                timer.Stop();
            }
            catch { }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewContractsDataGrid.SelectedItem != null)
                {
                    DataRowView rows = (DataRowView)NewContractsDataGrid.SelectedItems[0];

                    // get values from selected row
                    string clientName = (string)rows.Row.ItemArray[0];
                    int jobType = (int)rows.Row.ItemArray[1];
                    int quantity = (int)rows.Row.ItemArray[2];
                    string origin = (string)rows.Row.ItemArray[3];
                    string destination = (string)rows.Row.ItemArray[4];
                    int vanType = (int)rows.Row.ItemArray[5];

                    // create order from it
                    Order newOrder = CurrentBuyer.CreateOrder(clientName, jobType, quantity, origin, destination, vanType);

                    // insert the new contract into the database for record, getting contract ID
                    DataAccess dal = new DataAccess();
                    int contractID = dal.InsertNewContract(clientName, jobType, quantity, origin, destination, vanType);

                    dal.InsertNewOrder(contractID);
                }
            }
            catch (Exception ex)
            {
                Logger.LogToFile("Error accepting contract - " + ex.Message);
            }
        }

        private void invoiceButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess dal = new DataAccess();

            DataRowView rows = (DataRowView)currentContractsDataGrid.SelectedItems[0];
            int contractID = (int)rows.Row.ItemArray[0];
            int orderID = (int)rows.Row.ItemArray[1];

            DataRow contractInfo = dal.GetContractByID(contractID);
            DataRow orderInfo = dal.GetOrderByID(orderID);
        }
    }
}
