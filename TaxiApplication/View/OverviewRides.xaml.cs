using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
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

namespace TaxiApplication.View
{
    /// <summary>
    /// Interaction logic for OverviewRides.xaml
    /// </summary>
    public partial class OverviewRides : Window
    {
        private string connectionString = "Data Source=DataFile.db";
        public OverviewRides()
        {
            InitializeComponent();
            LoadTripDetails();
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            homepage homepage = new homepage();
            homepage.Show();
            Close();
        }

        private void LoadTripDetails()
        {
            using(IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();
                string userNameQuery = "SELECT Name FROM LoggedInUsers";
                string? userName = dbConnection.QueryFirstOrDefault<string>(userNameQuery);

                string getHighestTripIdQuery = "SELECT MAX(TripId) FROM Trips WHERE User = @UserName";
                int? latestTripByUser = dbConnection.QueryFirstOrDefault<int>(getHighestTripIdQuery, new {UserName = userName});

                string getDetailsQuery = "SELECT StartLocation, Destination, PickupTime, CarName FROM Trips WHERE TripId = @TripId";
                var detailsTrip = dbConnection.QueryFirstOrDefault(getDetailsQuery, new {TripId = latestTripByUser});

                StartAddress.Text = detailsTrip.StartLocation;
                DestinationAddress.Text = detailsTrip.Destination;
                TimeOfPickup.Text = detailsTrip.PickupTime;
                NameOfCar.Text = detailsTrip.CarName;
            }
        }
    }
}
