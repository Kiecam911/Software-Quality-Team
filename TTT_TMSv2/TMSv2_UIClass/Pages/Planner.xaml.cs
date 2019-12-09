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
        public Planner()
        {
            InitializeComponent();
        }

        private void activeOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            ActiveOrders.Visibility = Visibility.Visible;

            Order order = new Order();
            ActiveOrderDataGrid.ItemsSource = order.GetActiveOrders();

        }

        private void assignCarrierButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            AssignCarrierScreen.Visibility = Visibility.Visible;

            Order order = new Order();
            AssignCarrierDatagrid.ItemsSource = order.GetActiveOrders();

            MySqlConnection connection = new MySqlConnection(("Server=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; database=" + ConfigurationManager.AppSettings["DatabaseName"] + "; UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + "; password=" + ConfigurationManager.AppSettings["DatabasePassword"]));
            string selectQuery = "SELECT CarrierID FROM Carriers";
            connection.Open();
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) //Add BranchID to combo box
            {
                CarrierComboBox.Items.Add(reader.GetString("CarrierID"));
            }
            connection.Close();
        }

        private void completeOrderButton_Click(object sender, RoutedEventArgs e)
        {

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
        }
    }
}
