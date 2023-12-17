using TaxiApplication;
using TaxiApplication.View;
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
using System.Data.SQLite;
using Dapper;
using System.Data;

namespace TaxiApplication.View
{
    /// <summary>
    /// Interaction logic for loginUser.xaml
    /// </summary>
    public partial class LoginUser : Window
    {
        private string connectionString = "Data Source=DataFile.db";
        public LoginUser() { 
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Password;

            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();
                string query = "SELECT * FROM Users WHERE Name = @Name AND Password = @Password";
                User user = dbConnection.QueryFirstOrDefault<User>(query, new { Name = username, Password = password });

                if (user != null)
                {
                    AddLoggedInUser(username);
                    userAllowed();
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials");
                }
            }

        }

        public void userAllowed()
        {
            homepage home = new homepage();
            home.Show();

        }

        private void AddLoggedInUser(string username)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();
                string insertQuery = "INSERT INTO LoggedInUsers (Name) VALUES (@Name)";
                dbConnection.Execute(insertQuery, new { Name = username });
            }
        }
    }
}