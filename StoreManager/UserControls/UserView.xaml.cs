using StoreManager.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StoreManager.UserControls
{
    public partial class UserView : UserControl
    {
        private GenericRepository<User> repository;
        private User selectedUser;

        public UserView()
        {
            InitializeComponent();

            repository = new GenericRepository<User>(App.databasePath);
            ReadDatabase();


        }

        private void ReadDatabase()
        {
            tbUsername.Text = "";
            tbEmail.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbRole.Text = "";

            var userRepository = new GenericRepository<User>(App.databasePath);

            dgUsers.ItemsSource = userRepository.GetAll();

            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;

            selectedUser = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User
                {
                    Username = tbUsername.Text,
                    Email = tbEmail.Text,
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    Role = tbRole.Text,

                    PasswordHash = "123456",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                repository.insert(user);

                ClearFields();
                LoadUsers();

                MessageBox.Show("User added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Select a user first!");
                return;
            }

            try
            {
                selectedUser.Username = tbUsername.Text;
                selectedUser.Email = tbEmail.Text;
                selectedUser.FirstName = tbFirstName.Text;
                selectedUser.LastName = tbLastName.Text;
                selectedUser.Role = tbRole.Text;

                repository.update(selectedUser);

                LoadUsers();

                MessageBox.Show("User updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Select a user first!");
                return;
            }

            try
            {
                repository.delete(selectedUser);

                ClearFields();
                LoadUsers();

                MessageBox.Show("User deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
                return;

            selectedUser = (User)dgUsers.SelectedItem;

            tbUsername.Text = selectedUser.Username;
            tbEmail.Text = selectedUser.Email;
            tbFirstName.Text = selectedUser.FirstName;
            tbLastName.Text = selectedUser.LastName;
            tbRole.Text = selectedUser.Role;
        }

        private void ClearFields()
        {
            tbUsername.Clear();
            tbEmail.Clear();
            tbFirstName.Clear();
            tbLastName.Clear();
            tbRole.Clear();

            selectedUser = null;
        }
    }
}