using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ChooseCar.xaml
    /// </summary>
    public partial class ChooseCar : Window
    {
        private string connectionString = "Data Source=DataFile.db";
        private User loggedInUser;
        private int CarId = -1;

        public ChooseCar()
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            homepage homepage = new homepage();
            homepage.Show();
            Close();
        }

        private void SaveCarToTrip(int CarId)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                dbConnection.Open();
                string getUserQuery = "SELECT Name FROM LoggedInUsers";
                string userName = dbConnection.QueryFirst<string>(getUserQuery);

                string getCarName = "SELECT CarName FROM Cars WHERE CarId = @CarId";
                string? carName = dbConnection.QueryFirstOrDefault<string>(getCarName, new { CarId = CarId });

                string insertInTripsQuery = "INSERT INTO Trips (CarName, User)" + "VALUES (@CarName, @UserName)";

                var CarAndName = new
                {
                    CarName = carName,
                    UserName = userName
                };

                dbConnection.Execute(insertInTripsQuery, CarAndName);
            }
        }

        private void CarButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;

            if (selectedButton != null)
            {
                if (selectedButton.Name == "RenaultClio")
                {
                    CarId = 1;
                }
                else if (selectedButton.Name == "VolkswagenGolf")
                {
                    CarId = 2;
                }
                else if (selectedButton.Name == "FordFiesta")
                {
                    CarId = 3;
                }
                else if (selectedButton.Name == "BMW5")
                {
                    CarId = 4;
                }
                else if (selectedButton.Name == "MercedesC")
                {
                    CarId = 5;
                }
                else if (selectedButton.Name == "TeslaModelS")
                {
                    CarId = 6;
                }
                else if (selectedButton.Name == "BentleyMulsanne")
                {
                    CarId = 7;
                }
                else if (selectedButton.Name == "FerrariPurosangue")
                {
                    CarId = 8;
                }
                else if (selectedButton.Name == "RolceRoycePhantom")
                {
                    CarId = 9;
                }

                SaveCarToTrip(CarId);

                TripDetailsEnter entertripdetails = new TripDetailsEnter();
                entertripdetails.Show();
                Close();
            }


        }
    }
}
