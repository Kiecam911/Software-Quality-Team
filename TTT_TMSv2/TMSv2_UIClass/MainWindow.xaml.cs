using System.Windows;
using TMSv2_UIClass.Pages;

namespace TMSv2_UIClass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            TMSv2_Logging.Logger.LogToFile("Program loaded");
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var newPage = new MenuPage();
            _mainFrame.Navigate(newPage);
        }
    }
}
