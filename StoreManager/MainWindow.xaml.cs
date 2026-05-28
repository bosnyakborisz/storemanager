using StoreManager.UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreManager
{
    public partial class MainWindow : Window
    {
        private readonly ProductView _productView;
        private readonly CustomerView _customerView;
        private readonly UserView _userView;

        public MainWindow()
        {
            InitializeComponent();

            _productView = new ProductView();
            _customerView = new CustomerView();
            _userView = new UserView();

           // MainContent.Content = _productView;
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _productView;
            MainContent.Children.Clear();
            MainContent.Children.Add(_productView);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _customerView;
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = _userView;
        }
    }
}