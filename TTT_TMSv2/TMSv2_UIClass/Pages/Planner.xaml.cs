﻿using System;
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
using TMSv2_Users;
using TMSv2_TripPlanner;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_Logging;

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
        public int jobType;

        private TMSv2_Users.Planner currentPlanner;

        private List<int> DaysRequiredList;
        

        public Planner()
        {
            InitializeComponent();
            currentPlanner = new TMSv2_Users.Planner();
            DaysRequiredList = new List<int>();
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

            LoadOrdersWithTrip(CompleteOrdersDatagrid);
        }

        private void generateReportButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            GenerateReportsScreen.Visibility = Visibility.Visible;
        }

        private void increaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess dal = new DataAccess();
            dal.DayPassed();
            LoadOrdersWithTrip(CompleteOrdersDatagrid);
            TMSv2_Logging.Logger.LogToFile("Day increased by 1");
        }

        private void resetView()
        {
            ActiveOrders.Visibility = Visibility.Hidden;
            AssignCarrierScreen.Visibility = Visibility.Hidden;
            CompleteOrderScreen.Visibility = Visibility.Hidden;
            GenerateReportsScreen.Visibility = Visibility.Hidden;
        }

        private void AssignCarrierButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (carrierID == 0 || orderIDAssignCarrier == 0)
            {
                return;
            }
            int newTripID = 0;

            string sqlCommand = String.Format(@"INSERT INTO Trips (CarrierID, OrderID, Origin, Destination) VALUES ({0}, {1}, ""{2}"", ""{3}"");", carrierID, orderIDAssignCarrier, origin, destination);
            string sqlCommand2 = String.Format(@"SELECT LAST_INSERT_ID();");
            MySqlConnection connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));
            MySqlCommand cmd = new MySqlCommand(sqlCommand, connection);
            connection.Open();

            cmd.ExecuteNonQuery();

            cmd.CommandText = sqlCommand2;

            newTripID = (int)(ulong)cmd.ExecuteScalar();

            connection.Close();

            // get inserted trip into object
            // get associated order into object
            // get associataed carrier into object
            // link the 3 together
            // carry out calculations
            // store results in order

            // get rows from dal
            DataAccess dal = new DataAccess();

            // get data from db
            DataRow orderRow = dal.GetOrderByID(orderIDAssignCarrier);
            DataRow tripRow = dal.GetTripByID(newTripID);
            DataRow carrierRow = dal.GetCarrierInfoByID(carrierID);

            // create objects from rows
            Order currentOrder = currentPlanner.LoadOrderRow(orderRow);
            Trip currentTrip = currentPlanner.LoadTripRow(tripRow);
            Depot currentDepotRates = currentPlanner.LoadDepotRatesRow(carrierRow);

            // get more data from db as parameters
            DataRow contractRow = dal.GetContractByID(orderRow.Field<int>("OrderID"));
            int jobType = contractRow.Field<int>("Job_Type");
            int vanType = contractRow.Field<int>("Van_Type");
            int quantity = contractRow.Field<int>("Quantity");

            string cOrigin = contractRow.Field<string>("Origin");
            string cDestination = contractRow.Field<string>("Destination");



            currentOrder.Trips.Add(currentTrip);
            currentOrder.Trips[0].DepotRates = currentDepotRates;

            currentOrder.Trips[0].SetOriginDestination(cOrigin, cDestination);
            currentOrder.Trips[0].CalculateTotals(jobType);

            currentOrder.CalculateTotalCost(jobType, vanType, quantity);

            loadCarrierAssignmentTable(AssignCarrierDatagrid);

            // DataRow carrier = dal.GetCarrierByID(carrierID);

            TMSv2_Logging.Logger.LogToFile("Carrier assigned to order");
        }

        private void AssignCarrierDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null) //Gets contents of row and inserts it into variables
            {
                orderIDAssignCarrier = Convert.ToInt32(row_selected["OrderID"]);
                origin = row_selected["Origin"].ToString();
                destination = row_selected["Destination"].ToString();
                jobType = Convert.ToInt32(row_selected["Job_Type"]);
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

            // update grid
            LoadOrdersWithTrip(CompleteOrdersDatagrid);
        }

        private void loadActiveContracts(DataGrid grid)
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT DaysRequired as 'Days Required', OrderID, Contracts.ContractID, Contracts.Client_Name, Contracts.Origin, Contracts.Destination FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.OrderID WHERE Orders.IsActive = 1";
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

        private void LoadOrdersWithTrip(DataGrid grid)
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = @"SELECT DaysRequired AS 'Days Left', OrderID, Contracts.ContractID, Contracts.Client_Name, Contracts.Origin, Contracts.Destination FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.OrderID WHERE Orders.hasTrip = 1 AND Orders.Completed = 0;";
                
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                DaysRequiredList.Clear();

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "items");

                connection.Close();

                grid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["items"] });
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
                string sqlCommand = "SELECT Orders.OrderID, Contracts.ContractID, Contracts.Client_Name, Contracts.Origin, Contracts.Destination, Job_Type FROM Orders INNER JOIN Contracts ON Contracts.ContractID = Orders.ContractID WHERE hasTrip = 0";
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
                string sqlCommand = "SELECT *, CarrierName FROM CarrierInfo INNER JOIN Carriers ON Carriers.CarrierID = CarrierInfo.CarrierID INNER JOIN Contracts ON Contracts.ContractID = CarrierInfo.CarrierInfoID WHERE CarrierInfo.DestinationCity = '" + origin + "' AND job_type = "+ jobType + ";";
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
                
                // destination = row_selected["DestinationCity"].ToString();
            }
        }

        private void ViewAllButton_Click(object sender, RoutedEventArgs e)
        {          
            loadAll();
            TMSv2_Logging.Logger.LogToFile("Loaded all reports");
        }

        private void ViewTwoButton_Click(object sender, RoutedEventArgs e)
        {
            loadTwoWeeks();
            TMSv2_Logging.Logger.LogToFile("Loaded last two weeks of reports");
        }

        private void loadAll()
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT DaysRequired, Contracts.ContractID, Orders.OrderID, client_name, Quantity, Trips.Origin, Trips.Destination, Carriers.CarrierName, TotalExpense, TotalIncome FROM Contracts INNER JOIN Orders ON OrderID = Contracts.ContractID INNER JOIN Trips ON TripID = Orders.OrderID INNER JOIN Carriers ON Carriers.CarrierID = Trips.CarrierID WHERE Orders.Completed = 1;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Report");
                reportOut.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["Report"] });
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void loadTwoWeeks()
        {
            try
            {
                string ConnectionString = ("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]);
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                string sqlCommand = "SELECT DaysRequired, Contracts.ContractID, Orders.OrderID, client_name, Quantity, Trips.Origin, Trips.Destination, Carriers.CarrierName, TotalExpense, TotalIncome FROM Contracts INNER JOIN Orders ON OrderID = Contracts.ContractID INNER JOIN Trips ON TripID = Orders.OrderID INNER JOIN Carriers ON Carriers.CarrierID = Trips.CarrierID WHERE Orders.Completed = 1 AND DaysRequired > -14;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);

                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Report");
                reportOut.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["Report"] });
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        
    }
}
