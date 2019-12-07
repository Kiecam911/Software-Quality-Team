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

namespace TMSv2_UIClass.Pages
{
    
    /// <summary>
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Page
    {

        
        string connectionString = "SERVER=" + ConfigurationManager.AppSettings["DatabaseIP"] + "; PORT = 3306 ;" + "DATABASE=" + ConfigurationManager.AppSettings["DatabaseName"] + ";" + "UID=" + ConfigurationManager.AppSettings["DatabaseUsername"] + ";" + "PASSWORD=" + ConfigurationManager.AppSettings["DatabasePassword"] + ";";

        public BuyerPage()
        {
            InitializeComponent();  
        }

        private void newContract_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            newContractGrid.Visibility = Visibility.Visible;
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
                NewContractsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["*"] });
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Database failed to load, please check your connection");
            }
        }

        private void LoadCurrentContracts()
        {
            try
            {
                
                MySqlConnection connection = new MySqlConnection(connectionString);
                string sqlCommand = "SELECT * FROM Orders";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "*");
                currentContractsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["*"] });
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Database failed to load, please check your connection");
            }
        }

        private void LoadCompleteContracts()
        {
            try
            {

                MySqlConnection connection = new MySqlConnection(connectionString);
                string sqlCommand = "SELECT * FROM Orders WHERE Completed=1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand, connection);


                connection.Open();

                DataSet ds = new DataSet();
                adapter.Fill(ds, "*");
                currentContractsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = ds.Tables["*"] });
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Database failed to load, please check your connection");
            }
        }

        private void resetView()
        {
            //Reset New Contracts
            newContractGrid.Visibility = Visibility.Hidden;
            CurrentContractGrid.Visibility = Visibility.Hidden;
            CompletedContractGrid.Visibility = Visibility.Hidden;
        }

    }
}
