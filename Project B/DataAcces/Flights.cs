using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Project_B.Presentation;
using Spectre.Console;
using Spectre.Console.Cli;
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
        public string Gate { get; set; }
        public int FlightID { get; set; }
        public Seat? PlaneLayout { get; set; }
        public int FlightId { get; set; }

        public int Distance { get; set; }

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
            sqlite_conn = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
            try 
            { 
                sqlite_conn.Open(); 
            }
            catch (Exception ex){   }
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
                try
                {
                    Flight flight = new Flight
                    {
                        FlightId = int.Parse(sqlite_datareader["FlightId"].ToString()),
                        DepartureTime = DateTime.Parse(sqlite_datareader["DepartureTime"].ToString()),
                        Terminal = sqlite_datareader["Terminal"].ToString(),
                        FlightNumber = sqlite_datareader["FlightNumber"].ToString(),
                        AircraftType = sqlite_datareader["AircraftType"].ToString(),
                        Seats = int.Parse(sqlite_datareader["Seats"].ToString()),
                        AvailableSeats = int.Parse(sqlite_datareader["AvailableSeats"].ToString()),
                        Destination = sqlite_datareader["Destination"].ToString(),
                        Origin = sqlite_datareader["Origin"].ToString(),
                        Airline = sqlite_datareader["Airline"].ToString(),
                        Gate = sqlite_datareader["Gate"].ToString(),
                        Distance = int.Parse(sqlite_datareader["Distance"].ToString())
                    };
                    flights.Add(flight);
                }
                catch (FormatException ex) { }
            }
            sqlite_conn.Close();
            return flights;
        }
        public static List<Flight> GetFlightsAdmin()
        {
            List<Flight> flights = new List<Flight>();
            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Flights";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                try
                {
                    Flight flight = new Flight
                    {
                        FlightId = int.Parse(sqlite_datareader["FlightId"].ToString()),
                        DepartureTime = DateTime.Parse(sqlite_datareader["DepartureTime"].ToString()),
                        Terminal = sqlite_datareader["Terminal"].ToString(),
                        FlightNumber = sqlite_datareader["FlightNumber"].ToString(),
                        AircraftType = sqlite_datareader["AircraftType"].ToString(),
                        Seats = int.Parse(sqlite_datareader["Seats"].ToString()),
                        AvailableSeats = int.Parse(sqlite_datareader["AvailableSeats"].ToString()),
                        Destination = sqlite_datareader["Destination"].ToString(),
                        Origin = sqlite_datareader["Origin"].ToString(),
                        Airline = sqlite_datareader["Airline"].ToString(),
                        Gate = sqlite_datareader["Gate"].ToString()
                    };
                    flights.Add(flight);
                }
                catch (Exception ex) { }
            }
            sqlite_conn.Close();
            return flights;
        }
        
        public override string ToString()
        {
            return $"Time: {DepartureTime:dd/MM/yyyy HH:mm}, Origin: {Origin}, Destination: {Destination}, Flight Number: {FlightNumber}, Gate: {Gate}, Terminal: {Terminal}";
        }
        public static void AddFlight(Flight flight)
        {
            using (SQLiteConnection sqlite_conn = CreateConnection())
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Flights (DepartureTime, Terminal, FlightNumber, AircraftType, Seats, AvailableSeats, Destination, Origin, Airline, Gate, Distance) VALUES (@DepartureTime, @Terminal, @FlightNumber, @AircraftType, @Seats, @AvailableSeats, @Destination, @Origin, @Airline, @Gate, @Distance)";
                sqlite_cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
                sqlite_cmd.Parameters.AddWithValue("@Terminal", flight.Terminal);
                sqlite_cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
                sqlite_cmd.Parameters.AddWithValue("@AircraftType", flight.AircraftType);
                sqlite_cmd.Parameters.AddWithValue("@Seats", flight.Seats);
                sqlite_cmd.Parameters.AddWithValue("@AvailableSeats", flight.AvailableSeats);
                sqlite_cmd.Parameters.AddWithValue("@Destination", flight.Destination);
                sqlite_cmd.Parameters.AddWithValue("@Origin", flight.Origin);
                sqlite_cmd.Parameters.AddWithValue("@Airline", flight.Airline);
                sqlite_cmd.Parameters.AddWithValue("@Gate", flight.Gate);
                sqlite_cmd.Parameters.AddWithValue("@Distance", flight.Distance);
                sqlite_cmd.ExecuteNonQuery();
                sqlite_conn.Close();
            }
        }
        public static void UpdateFlight(Flight flightToUpdate)
        {
            // Get the connection string
            string connectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            // Create a new connection
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a new command
                using (SQLiteCommand command = new SQLiteCommand("UPDATE Flights SET DepartureTime = @DepartureTime, Terminal = @Terminal, FlightNumber = @FlightNumber, Seats = @Seats, AvailableSeats = @AvailableSeats, Destination = @Destination, Origin = @Origin, Airline = @Airline, Gate = @Gate WHERE FlightID = @FlightID", connection))
                {
                    // Add the parameters
                    command.Parameters.AddWithValue("@DepartureTime", flightToUpdate.DepartureTime);
                    command.Parameters.AddWithValue("@Terminal", flightToUpdate.Terminal);
                    command.Parameters.AddWithValue("@FlightNumber", flightToUpdate.FlightNumber);
                    command.Parameters.AddWithValue("@Seats", flightToUpdate.Seats);
                    command.Parameters.AddWithValue("@AvailableSeats", flightToUpdate.AvailableSeats);
                    command.Parameters.AddWithValue("@Destination", flightToUpdate.Destination);
                    command.Parameters.AddWithValue("@Origin", flightToUpdate.Origin);
                    command.Parameters.AddWithValue("@Airline", flightToUpdate.Airline);
                    command.Parameters.AddWithValue("@Gate", flightToUpdate.Gate);
                    command.Parameters.AddWithValue("@FlightID", flightToUpdate.FlightId);

                    // Execute the command
                    command.ExecuteNonQuery();
                }
            }
        }
        public static Flight GetFlightById(int flightId)
        {
            // Get the connection string
            string connectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            // Create a new connection
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a new command
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Flights WHERE FlightId = @FlightId", connection))
                {
                    // Add the parameter
                    command.Parameters.AddWithValue("@FlightId", flightId);

                    // Execute the command and get the result
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Create a new flight and set its properties
                            Flight flight = new Flight();
                            flight.FlightId = reader.GetInt32(reader.GetOrdinal("FlightId"));
                            flight.Gate = reader.GetString(reader.GetOrdinal("Gate"));
                            flight.AircraftType = reader.GetString(reader.GetOrdinal("AircraftType"));
                            flight.Destination = reader.GetString(reader.GetOrdinal("Destination"));
                            flight.Origin = reader.GetString(reader.GetOrdinal("Origin"));
                            flight.DepartureTime = reader.GetDateTime(reader.GetOrdinal("DepartureTime"));
                            flight.Distance = reader.GetInt32(reader.GetOrdinal("Distance"));
                            return flight;
                        }
                        else
                        {
                            // No flight found with the given ID
                            return null;
                        }
                    }
                }
            }
        }
        public static void DeleteRow()
        {
            try
            {
                using (SQLiteConnection c = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; "))
                {
                    c.Open();
                    string sqlCommands = "DELETE FROM Flights";
                    using (SQLiteCommand cmd = new SQLiteCommand(sqlCommands, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)   {   }
        }
        public static List<Flight> Adminflightlist()
        {
            List<Flight> flights = GetFlights();

            // Sort flights by time
            flights = flights.OrderBy(flight => flight.DepartureTime).ToList();
            return flights;
        }

        private static List<string> cities = new List<string>()
        {
            "London", "Paris", "Berlin", "Rome", "Madrid", "Barcelona", "Vienna", "Prague", "Dublin",
            "Munich", "Venice", "Brussels", "Budapest", "Zurich", "Warsaw", "Copenhagen", "Athens", "Lisbon", "Istanbul"
        };
        private static int cityIndex = 0;
        public static void CreateFlightBoeing737()
        {
            Random random = new Random();
            DateTime now = DateTime.Now.AddHours(random.Next(1, 24));
            DateTime departureTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            Flight flight = new Flight
            {
                DepartureTime = departureTime,
                Terminal = random.Next(1, 4).ToString(),
                FlightNumber = random.Next(1000, 9999).ToString(),
                AircraftType = "Boeing 737",
                Seats = 186,
                AvailableSeats = 186,
                Destination = cities[cityIndex],
                Origin = "Amsterdam",
                Airline = "New South",
                Gate = random.Next(1, 24).ToString(),
                Distance = random.Next(1000, 9999)
            };
            AddFlight(flight);
            cityIndex = (cityIndex + 1) % cities.Count;
        }
        public static void CreateFlightBoeing787()
        {
            Random random = new Random();
            DateTime now = DateTime.Now.AddHours(random.Next(1, 24));
            DateTime departureTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            Flight flight = new Flight
            {
                DepartureTime = departureTime,
                Terminal = random.Next(1, 4).ToString(),
                FlightNumber = random.Next(1000, 9999).ToString(),
                AircraftType = "Boeing 787",
                Seats = 219,
                AvailableSeats = 219,
                Destination = cities[cityIndex],
                Origin = "Amsterdam",
                Airline = "New South",
                Gate = random.Next(1, 24).ToString(),
                Distance = random.Next(1000, 9999)
            };
            AddFlight(flight);
            cityIndex = (cityIndex + 1) % cities.Count;
        }
        public static void CreateFlightAirbus330()
        {
            Random random = new Random();
            DateTime now = DateTime.Now.AddHours(random.Next(1, 24));
            DateTime departureTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            Flight flight = new Flight
            {
                DepartureTime = departureTime,
                Terminal = random.Next(1, 4).ToString(),
                FlightNumber = random.Next(1000, 9999).ToString(),
                AircraftType = "Airbus 330",
                Seats = 345,
                AvailableSeats = 345,
                Destination = cities[cityIndex],
                Origin = "Amsterdam",
                Airline = "New South",
                Gate = random.Next(1, 24).ToString(),
                Distance = random.Next(1000, 9999)
            };
            AddFlight(flight);
            cityIndex = (cityIndex + 1) % cities.Count;
        }
        public static void CreateSetFlight()
        {
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
                Distance = 150
            };
            AddFlight(flight);
        }

        // function for adding tickets to a plane
        public static void reserveseat(int flightid, int userid, string seat, string seatclass, string extranotes)
        {
            Users user = Users.GetuserbyId(userid);
            Flight flight = GetFlightById(flightid);
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            DateTime time = DateTime.Now;

            string sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination,Retour, Origin, Distance, Extranotes ) VALUES('{user.Email}','{time}', '{user.Name}','{seat}', '{seatclass}', {flightid}, {user.Id}, '{flight.Gate}', '{flight.DepartureTime}', '{flight.Destination}','0', '{flight.Origin}','{flight.Distance}', '{extranotes}');";

            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                {
                    cmd1.ExecuteNonQuery();
                }
            }
        }
        public static bool Removeflight(int FlightNumber)
        {
            try
            {
                string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = $"DELETE FROM Flights WHERE FlightNumber = {FlightNumber} ";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
        public static bool Removeticket(int FlightNumber)
        {
            try
            {
                string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = $"DELETE FROM Tickets WHERE FlightID = {FlightNumber} ";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
    }
}
