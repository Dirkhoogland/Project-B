using System;
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
                string[] sqlCommands = new string[]
                {
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
                    "Airline VARCHAR(255))",
                    "ALTER TABLE Flights ADD COLUMN Origin VARCHAR(255)",
                    "ALTER TABLE Flights ADD COLUMN Status VARCHAR(255)",
                    "ALTER TABLE Flights ADD COLUMN Gate VARCHAR(255)",
                    "ALTER TABLE Flights ADD COLUMN AvailableSeats INTEGER"
                };

                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    foreach (var sql in sqlCommands)
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

    }
}