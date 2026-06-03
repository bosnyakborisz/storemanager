using StoreManager.UserControls;
using System.Windows;

namespace StoreManager
{
    public partial class MainWindow : Window
    {
        private ProductView _productView;
        private CustomerView _customerView;
        private UserView _userView;

        public MainWindow()
        {
            InitializeComponent();

            _productView = new ProductView();
            _customerView = new CustomerView();
            _userView = new UserView();

        }


        private void Products_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _productView;
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _customerView;
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _userView;
        }
    }
}