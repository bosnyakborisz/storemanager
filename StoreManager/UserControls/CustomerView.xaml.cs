using System;
using System.Windows;
using System.Windows.Controls;
using StoreManager.Models;

namespace StoreManager.UserControls
{
    public partial class CustomerView : UserControl
    {
        Customer selectedCustomer;

        public CustomerView()
        {
            InitializeComponent();
            ReadDatabase();
        }

        private void ReadDatabase()
        {
            tbGamerTag.Text = "";
            tbFullName.Text = "";
            tbEmail.Text = "";
            tbPhone.Text = "";
            tbPlatform.Text = "";
            tbLoyaltyPoints.Text = "0";

            var repo = new GenericRepository<Customer>(App.databasePath);
            dgCustomers.ItemsSource = repo.GetAll();

            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers.SelectedItem != null)
            {
                selectedCustomer = (Customer)dgCustomers.SelectedItem;
                tbGamerTag.Text = selectedCustomer.GamerTag;
                tbFullName.Text = selectedCustomer.FullName;
                tbEmail.Text = selectedCustomer.Email;
                tbPhone.Text = selectedCustomer.Phone;
                tbPlatform.Text = selectedCustomer.PreferredPlatform;
                tbLoyaltyPoints.Text = selectedCustomer.LoyaltyPoints.ToString();

                btnAdd.Visibility = Visibility.Hidden;
                btnUpdate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer
            {
                GamerTag = tbGamerTag.Text,
                FullName = tbFullName.Text,
                Email = tbEmail.Text,
                Phone = tbPhone.Text,
                PreferredPlatform = tbPlatform.Text,
                LoyaltyPoints = int.TryParse(tbLoyaltyPoints.Text, out int lp) ? lp : 0,
                RegisteredAt = DateTime.Now
            };

            var repo = new GenericRepository<Customer>(App.databasePath);
            repo.insert(newCustomer);
            ReadDatabase();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer == null) return;

            selectedCustomer.GamerTag = tbGamerTag.Text;
            selectedCustomer.FullName = tbFullName.Text;
            selectedCustomer.Email = tbEmail.Text;
            selectedCustomer.Phone = tbPhone.Text;
            selectedCustomer.PreferredPlatform = tbPlatform.Text;
            selectedCustomer.LoyaltyPoints = int.TryParse(tbLoyaltyPoints.Text, out int lp) ? lp : selectedCustomer.LoyaltyPoints;

            var repo = new GenericRepository<Customer>(App.databasePath);
            repo.update(selectedCustomer);
            ReadDatabase();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer == null) return;

            var repo = new GenericRepository<Customer>(App.databasePath);
            repo.delete(selectedCustomer);
            ReadDatabase();
        }
    }
}