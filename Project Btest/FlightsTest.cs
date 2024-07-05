using Project_B.DataAcces;
using System.ComponentModel;
using System.Data.SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_Btest
{
    [TestClass]
    public class FlightsTest
    {
        public static string databasePath
        {
            get
            {   // gets the path to where ever its currently on your pc/laptop and then into a DataSource file, which if its correctly downloaded from github it should find.
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }
        [TestMethod]
        public void CheckIfTableExists()
        {   // assert
            DataAccess.Database();
            string tablename = "";
            bool check = false;
            try
            {   // act
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sqlCommands = "SELECT name FROM sqlite_master WHERE type='table' AND name='Flights'";;
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(sqlCommands, c))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                tablename = rdr.GetTableName(0);
                              int FlightID = rdr.GetInt32(0);  
                            }

                        }
                    }
                }
            }

            catch (Exception ex) { } 
            if (tablename != "")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            // assert 
            Assert.IsTrue(check);
        }


        [TestMethod]
        public void TestCreateFlights()
        {   // arrange
            DataAccess.Database();
            Flight flight = new Flight
            {
                DepartureTime = new DateTime(2024, 12, 31, 12, 0, 0),
                Terminal = "1",
                FlightNumber = "1234",
                AircraftType = "Airbus 330",
                Seats = 345,
                AvailableSeats = 345,
                Destination = "London",
                Origin = "Amsterdam",
                Airline = "New South",
                Gate = "1",
                Distance = 150,
                Revenue = 0
            };
            Flight.AddFlight(flight);
            Flight flight2 = new Flight
            {
                DepartureTime = new DateTime(2024, 12, 31, 12, 0, 0),
                Terminal = "2",
                FlightNumber = "12345",
                AircraftType = "Airbus 330",
                Seats = 345,
                AvailableSeats = 345,
                Destination = "London",
                Origin = "Amsterdam",
                Airline = "New South",
                Gate = "2",
                Distance = 150,
                Revenue = 0
            };
            Flight.AddFlight(flight2);
            // act
            var flights = Flight.GetFlights();
            int number = 1234;
            int number2 = 12345;
            Flight.Removeflight(number);
            Flight.Removeflight(number2);
            // assert
            Assert.AreEqual(2, flights.Count);

        }

        [TestMethod]
        public void TestDeleteFlight()
        {
            // Arrange
            DataAccess.Database();
            Flight flight2 = new Flight
            {
                DepartureTime = new DateTime(2024, 12, 31, 12, 0, 0),
                Terminal = "2",
                FlightNumber = "546",
                AircraftType = "Airbus 330",
                Seats = 345,
                AvailableSeats = 345,
                Destination = "London",
                Origin = "Amsterdam",
                Airline = "New South",
                Gate = "2",
                Distance = 150,
                Revenue = 0
            };
            Flight.AddFlight(flight2);
            int number = 546;
            // Act
            Flight.Removeflight(number);

            // Assert
            var flights = Flight.GetFlights();
            Assert.AreEqual(0, flights.Count);
        }

    }
}