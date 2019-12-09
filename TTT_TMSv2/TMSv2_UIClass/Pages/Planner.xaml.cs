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
using TMSv2_DAL;
using TMSv2_Order;

namespace TMSv2_UIClass.Pages
{
    /// <summary>
    /// Interaction logic for Planner.xaml
    /// </summary>
    public partial class Planner : Page
    {
        public int orderIDAssignCarrier;
        public int orderIDCompleteOrder;
        public int carrierID;
        public string origin;
        public string destination;

        public Planner()
        {
            InitializeComponent();
        }

        private void activeOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            ActiveOrders.Visibility = Visibility.Visible;

            loadActiveContracts(ActiveOrderDataGrid);
        }

        private void assignCarrierButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            AssignCarrierScreen.Visibility = Visibility.Visible;

            loadCarrierAssignmentTable(AssignCarrierDatagrid);
        }

        private void completeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            CompleteOrderScreen.Visibility = Visibility.Visible;

            loadActiveContracts(CompleteOrdersDatagrid);
        }

        private void generateReportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void increaseTimeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resetView()
        {
            ActiveOrders.Visibility = Visibility.Hidden;
            AssignCarrierScreen.Visibility = Visibility.Hidden;
            CompleteOrderScreen.Visibility = Visibility.Hidden;
        }

        private void AssignCarrierButton_Click_1(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));
            connection.Open();
            string sqlCommand = "INSERT Trips SET TripID = 56, CarrierID = "+ carrierID+ ", OrderID = "+ orderIDAssignCarrier+", Origin = '"+origin+ "', Destination = '"+destination+"';";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


            DataSet sd = new DataSet();
            adapter.Fill(sd, "Orders");
            connection.Close();
        }

        private void AssignCarrierDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null) //Gets contents of row and inserts it into variables
            {
                orderIDAssignCarrier = Convert.ToInt32(row_selected["OrderID"]);
                origin = row_selected["Origin"].ToString();
            }
            loadPotentialCarriers(PricesForCarriers);
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

        private void CompleteOrdersDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null) //Gets contents of row and inserts it into variables
            {
                orderIDCompleteOrder = Convert.ToInt32(row_selected["OrderID"]);
            }
        }

        private void CompleteOrderButton_Click_1(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));
            connection.Open();
            string sqlCommand = "UPDATE Orders SET Completed = 1, IsActive = 0 WHERE OrderID = " + orderIDCompleteOrder;
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


            DataSet sd = new DataSet();
            adapter.Fill(sd, "Orders");
            connection.Close();
        }

        private void loadActiveContracts(DataGrid grid)
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT OrderID, Contracts.ContractID, Contracts.Client_Name, Contracts.Origin, Contracts.Destination FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.OrderID WHERE Orders.IsActive = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");
                grid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["items"] });
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Database failed to load, please check your connection");
            }
        }

        private void loadCarrierAssignmentTable(DataGrid grid)
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT Orders.OrderID, Contracts.ContractID, Contracts.Client_Name, Contracts.Origin, Contracts.Destination FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.ContractID WHERE hasTrip = 0";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");
                grid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["items"] });
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void loadPotentialCarriers(DataGrid grid)
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT *, CarrierName FROM CarrierInfo INNER JOIN Carriers ON Carriers.CarrierID = CarrierInfo.CarrierID WHERE CarrierInfo.DestinationCity = '" + origin + "';";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");
                grid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["items"] });
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void PricesForCarriers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null) //Gets contents of row and inserts it into variables
            {
                carrierID = Convert.ToInt32(row_selected["CarrierID"]);
                
                destination = row_selected["DestinationCity"].ToString();
            }
        }
    }
}
