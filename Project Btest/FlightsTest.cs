using Project_B.DataAcces;
using System.Data.SQLite;

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
        public void CreateFlightsTable()
        {
            try
            {
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sqlCommands =
                
                    "CREATE TABLE IF NOT EXISTS Flights(" +
                    "FlightID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "FlightNumber VARCHAR(255)," +
                    "Destination VARCHAR(255)," +
                    "Origin VARCHAR(255)," +
                    "DepartureTime DATETIME," +
                    "Status VARCHAR(255)," +
                    "Terminal VARCHAR(255)," +
                    "AircraftType VARCHAR(255)," +
                    "Gate VARCHAR(255)," +
                    "Seats INTEGER," +
                    "AvailableSeats INTEGER," +
                    "Airline VARCHAR(255))";
                

                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();

                        using (SQLiteCommand cmd = new SQLiteCommand(sqlCommands, c))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    
                }
            }
            catch (Exception ex) { }
        }
        public void DeleteRow()
        {
            try
            {
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sqlCommands = "DELETE FROM Flights";

                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(sqlCommands, c))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex) { }
        }

        public static bool CheckIfTableExists()
        {
            string tablename = "";
            try
            {
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
                    return true;
                }
                else
                {
                    return false;
                }
        }

        [TestMethod]
        public void TestCreateFlightsTable()
        {
            // Act
            DeleteRow();
            DataAccess.CreateFlightsTable();

            // Assert
            var tableExists = CheckIfTableExists();
            Assert.IsTrue(tableExists);
            DeleteRow();
        }

        [TestMethod]
        public void TestCreateFlights()
        {
            // Act
            DeleteRow();
            Flight.CreateFlights();

            // Assert
            var flights = Flight.GetFlights();
            Assert.AreEqual(3, flights.Count);
            DeleteRow();
        }

        [TestMethod]
        public void TestGetFlights()
        {
            // Arrange
            DeleteRow();
            DataAccess.CreateFlightsTable();
            Flight.CreateFlights();

            // Act
            var flights = Flight.GetFlights();

            // Assert
            Assert.AreEqual(3, flights.Count);
            DeleteRow();
        }

        [TestMethod]
        public void TestDeleteRow()
        {
            // Arrange
            DeleteRow();
            DataAccess.CreateFlightsTable();
            Flight.CreateFlights();

            // Act
            DeleteRow();

            // Assert
            var flights = Flight.GetFlights();
            Assert.AreEqual(0, flights.Count);
            DeleteRow();
        }
    }
}