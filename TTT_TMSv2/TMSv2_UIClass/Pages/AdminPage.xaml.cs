using System;
using System.Collections.Generic;
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

namespace TMSv2_UIClass.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
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

        private void configButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            ConfigGrid.Visibility = Visibility.Visible;
        }

        private void setLogButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            LogFileGrid.Visibility = Visibility.Visible;
        }

        private void setDBMSInfoButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            DBMSInfoScreen.Visibility = Visibility.Visible;
        }

        private void resetView()
        {
            //Reset configGrid
            ConfigGrid.Visibility = Visibility.Hidden;
            DBMSInfoScreen.Visibility = Visibility.Hidden;
            LogFileGrid.Visibility = Visibility.Hidden;

            //Reset logGrid
            ViewLogGrid.Visibility = Visibility.Hidden;
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            resetView();
            ViewLogGrid.Visibility = Visibility.Visible;
        }
    }
}
