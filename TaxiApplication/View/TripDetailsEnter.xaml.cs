using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace TaxiApplication.View
{
    /// <summary>
    /// Interaction logic for TripDetailsEnter.xaml
    /// </summary>
    public partial class TripDetailsEnter : Window
    {
        private string connectionString = "Data Source=DataFile.db";
        public TripDetailsEnter()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            homepage homepage = new homepage();
            homepage.Show();
            Close();
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

        private void btnSubmitDetails_Click(object sender, RoutedEventArgs e)
        {
            string startingAddress = StartingAddress.Text;
            string destinationAddress= DestinationAddress.Text;
            string pickupTime = PickupTime.Text;

            if (string.IsNullOrEmpty(startingAddress) || string.IsNullOrEmpty(destinationAddress) || string.IsNullOrEmpty(pickupTime))
            {
                MessageBox.Show("All fields must be filled in");
                return;
            }

            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();
                string highestTripIdQuery = "SELECT MAX(TripId) FROM Trips;";
                int tripId = dbConnection.QueryFirstOrDefault<int>(highestTripIdQuery);

                string updateTripQuery = "UPDATE Trips SET PickupTime = @PickupTime, StartLocation = @StartLocation, Destination = @Destination WHERE TripId = @TripId";

                var tripDetails = new
                {
                    TripId = tripId,
                    StartLocation = startingAddress,
                    Destination = destinationAddress,
                    PickupTime = pickupTime,
                };

                dbConnection.Execute(updateTripQuery, tripDetails);
            }

            OverviewRides overViewRides = new OverviewRides();
            overViewRides.Show();
            Close();

        }
    }
}
