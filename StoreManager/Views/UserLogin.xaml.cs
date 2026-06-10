using StoreManager.Models;
using StoreManager.Services;
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
using System.Windows.Shapes;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = loginUserText.Text;
            string passwordHash = PasswordHelper.HashPassword(loginPasswordText.Password);
            if (!string.IsNullOrEmpty(loginUserText.Text) || !string.IsNullOrEmpty(loginPasswordText.Password))
            {
                using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.databasePath))
                {
                    var user = connection.Table<User>().FirstOrDefault(u => u.Username == userName);
                    if (user != null)
                    {
                        if (user.PasswordHash == passwordHash)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Entry denied!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Entry denied!");
                    }
                }
            }
        }
    }
}
