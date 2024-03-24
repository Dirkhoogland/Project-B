using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Project_B.DataAcces
{
    public class Flight
    {
        public DateTime DepartureTime { get; set; }
        public string Terminal { get; set; }
        public string FlightNumber { get; set; }
        public string AircraftType { get; set; }
        public int Seats { get; set; }
        public int AvailableSeats { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public string Airline { get; set; }
        public string Status { get; set; }
        public string Gate { get; set; }

        private static string databasePath
        {
            get
            {
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try { sqlite_conn.Open(); }
            catch (Exception ex) { }
            return sqlite_conn;
        }
        public static List<Flight> GetFlights()
        {
            List<Flight> flights = new List<Flight>();
            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Flights";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                Flight flight = new Flight
                {
                    DepartureTime = DateTime.Parse(sqlite_datareader["DepartureTime"].ToString()),
                    Terminal = sqlite_datareader["Terminal"].ToString(),
                    FlightNumber = sqlite_datareader["FlightNumber"].ToString(),
                    AircraftType = sqlite_datareader["AircraftType"].ToString(),
                    Seats = int.Parse(sqlite_datareader["Seats"].ToString()),
                    AvailableSeats = int.Parse(sqlite_datareader["AvailableSeats"].ToString()),
                    Destination = sqlite_datareader["Destination"].ToString(),
                    Origin = sqlite_datareader["Origin"].ToString(),
                    Airline = sqlite_datareader["Airline"].ToString(),
                    Status = sqlite_datareader["Status"].ToString(),
                    Gate = sqlite_datareader["Gate"].ToString()
                };
                flights.Add(flight);
            }
            sqlite_conn.Close();
            return flights;
        }
        public static void AddFlight(Flight flight)
        {
            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Flights (DepartureTime, Terminal, FlightNumber, AircraftType, Seats, AvailableSeats, Destination, Origin, Airline, Status, Gate) VALUES (@DepartureTime, @Terminal, @FlightNumber, @AircraftType, @Seats, @AvailableSeats, @Destination, @Origin, @Airline, @Status, @Gate)";
            sqlite_cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
            sqlite_cmd.Parameters.AddWithValue("@Terminal", flight.Terminal);
            sqlite_cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
            sqlite_cmd.Parameters.AddWithValue("@AircraftType", flight.AircraftType);
            sqlite_cmd.Parameters.AddWithValue("@Seats", flight.Seats);
            sqlite_cmd.Parameters.AddWithValue("@AvailableSeats", flight.AvailableSeats);
            sqlite_cmd.Parameters.AddWithValue("@Destination", flight.Destination);
            sqlite_cmd.Parameters.AddWithValue("@Origin", flight.Origin);
            sqlite_cmd.Parameters.AddWithValue("@Airline", flight.Airline);
            sqlite_cmd.Parameters.AddWithValue("@Status", flight.Status);
            sqlite_cmd.Parameters.AddWithValue("@Gate", flight.Gate);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
        public static void CreateFlightBoeing737()
        {
            Random random = new Random();
            Flight flight = new Flight
            {
                DepartureTime = DateTime.Now.AddHours(random.Next(1, 24)),
                Terminal = random.Next(1, 4).ToString(),
                FlightNumber = random.Next(1000, 9999).ToString(),
                AircraftType = "Boeing 737",
                Seats = 186,
                AvailableSeats = 186,
                Destination = "Berlin",
                Origin = "Amsterdam",
                Airline = "New South",
                Status = "On time",
                Gate = random.Next(1, 20).ToString()
            };
            AddFlight(flight);
        }
        public static void CreateFlightBoeing787()
        {
            Random random = new Random();
            Flight flight = new Flight
            {
                DepartureTime = DateTime.Now.AddHours(random.Next(1, 24)),
                Terminal = random.Next(1, 4).ToString(),
                FlightNumber = random.Next(1000, 9999).ToString(),
                AircraftType = "Boeing 787",
                Seats = 219,
                AvailableSeats = 219,
                Destination = "Paris",
                Origin = "Amsterdam",
                Airline = "New South",
                Status = "On time",
                Gate = random.Next(1, 20).ToString()
            };
            AddFlight(flight);
        }
        public static void CreateFlightAirbus330()
        {
            Random random = new Random();
            Flight flight = new Flight
            {
                DepartureTime = DateTime.Now.AddHours(random.Next(1, 24)),
                Terminal = random.Next(1, 4).ToString(),
                FlightNumber = random.Next(1000, 9999).ToString(),
                AircraftType = "Airbus 330",
                Seats = 345,
                AvailableSeats = 345,
                Destination = "London",
                Origin = "Amsterdam",
                Airline = "New South",
                Status = "On time",
                Gate = random.Next(1, 20).ToString()
            };
            AddFlight(flight);
        }
    }
}
