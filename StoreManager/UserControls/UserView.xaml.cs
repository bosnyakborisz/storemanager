using StoreManager.Models;
using StoreManager.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreManager.UserControls
{
    public partial class UserView : UserControl
    {
        User selectedUser;

        public UserView()
        {
            InitializeComponent();

            cbRole.ItemsSource = Enum.GetNames(typeof(Roles));

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            tbUsername.Text = "";
            tbEmail.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            pbPassword.Password = "";
            selectedUser = null;

            cbRole.SelectedItem = Enum.GetName(typeof(Roles), Roles.Admin);

            var userRepository = new GenericRepository<User>(App.databasePath);
            dgUsers.ItemsSource = userRepository.GetAll();


            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgUsers.SelectedItem != null)
            {
                selectedUser = (User)dgUsers.SelectedItem;

                tbUsername.Text = selectedUser.Username;
                tbEmail.Text = selectedUser.Email;
                tbFirstName.Text = selectedUser.FirstName;
                tbLastName.Text = selectedUser.LastName;
                cbRole.Text = selectedUser.RoleName;
                pbPassword.Password = "";

                btnAdd.Visibility = Visibility.Hidden;
                btnUpdate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
            else
            {

                btnAdd.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show("Username is required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string roleName = (string)cbRole.SelectedItem;
            Roles role = (Roles)Enum.Parse(typeof(Roles), roleName);

            User newUser = new User
            {
                Username = tbUsername.Text,
                Email = tbEmail.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                PasswordHash = PasswordHelper.HashPassword(pbPassword.Password),
                Role = (int)role,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            var userRepository = new GenericRepository<User>(App.databasePath);
            userRepository.insert(newUser);

            ReadDatabase();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null) return;

            var userRepository = new GenericRepository<User>(App.databasePath);
            userRepository.delete(selectedUser);

            ReadDatabase();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null) return;

            selectedUser.Username = tbUsername.Text;
            selectedUser.Email = tbEmail.Text;
            selectedUser.FirstName = tbFirstName.Text;
            selectedUser.LastName = tbLastName.Text;

            string roleName = (string)cbRole.SelectedItem;
            Roles role = (Roles)Enum.Parse(typeof(Roles), roleName);
            selectedUser.Role = (int)role;


            if (!string.IsNullOrEmpty(pbPassword.Password))
            {
                selectedUser.PasswordHash = PasswordHelper.HashPassword(pbPassword.Password);
            }

            var userRepository = new GenericRepository<User>(App.databasePath);
            userRepository.update(selectedUser);

            ReadDatabase();
        }
    }
}