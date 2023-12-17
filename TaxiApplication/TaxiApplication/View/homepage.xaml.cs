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
    /// Interaction logic for homepage.xaml
    /// </summary>
    public partial class homepage : Window
    {
        private string connectionString = "Data Source=DataFile.db";
        public homepage()
        {
            InitializeComponent();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                string deleteQuery = "DELETE FROM LoggedInUsers";
                dbConnection.Execute(deleteQuery);
            }
            Application.Current.Shutdown();
        }
        private void btnPlanTrip_Click(object sender, RoutedEventArgs e)
        {
            ChooseCar chooseCar = new ChooseCar();
            chooseCar.Show();
            Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();

                // Delete the logged-in user from LoggedInUsers table using Dapper
                string deleteQuery = "DELETE FROM LoggedInUsers";
                dbConnection.Execute(deleteQuery);
            }

            LoginUser logout = new LoginUser();
            logout.Show();
            Close();
        }
    }
}
