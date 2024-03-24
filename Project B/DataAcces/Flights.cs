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
        public int FlightID { get; set; }

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
        
        public override string ToString()
        {
            return $"Time: {DepartureTime}, Destination: {Destination}, Flight Number: {FlightNumber}, Gate: {Gate}, Status: {Status}, Terminal: {Terminal}";
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
        public static void UpdateFlight(Flight updatedFlight)
        {
            using (SQLiteConnection conn = CreateConnection())
            {
                string sql = "UPDATE Flights SET DepartureTime = @DepartureTime, Terminal = @Terminal, FlightNumber = @FlightNumber, AircraftType = @AircraftType, Seats = @Seats, AvailableSeats = @AvailableSeats, Destination = @Destination, Origin = @Origin, Airline = @Airline, Status = @Status, Gate = @Gate WHERE FlightID = @FlightID";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartureTime", updatedFlight.DepartureTime);
                    cmd.Parameters.AddWithValue("@Terminal", updatedFlight.Terminal);
                    cmd.Parameters.AddWithValue("@FlightNumber", updatedFlight.FlightNumber);
                    cmd.Parameters.AddWithValue("@AircraftType", updatedFlight.AircraftType);
                    cmd.Parameters.AddWithValue("@Seats", updatedFlight.Seats);
                    cmd.Parameters.AddWithValue("@AvailableSeats", updatedFlight.AvailableSeats);
                    cmd.Parameters.AddWithValue("@Destination", updatedFlight.Destination);
                    cmd.Parameters.AddWithValue("@Origin", updatedFlight.Origin);
                    cmd.Parameters.AddWithValue("@Airline", updatedFlight.Airline);
                    cmd.Parameters.AddWithValue("@Status", updatedFlight.Status);
                    cmd.Parameters.AddWithValue("@Gate", updatedFlight.Gate);
                    cmd.Parameters.AddWithValue("@FlightID", updatedFlight.FlightID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("No flight found with the given FlightID.");
                    }
                }
            }
        }
        public static Flight GetFlightById(int flightId)
        {
            Flight flight = new Flight();
            using (SQLiteConnection conn = CreateConnection())
            {
                string sql = "SELECT * FROM Flights WHERE FlightID = @FlightID";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FlightID", flightId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            flight.FlightID = int.Parse(reader["FlightID"].ToString());
                            flight.DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString());
                            flight.Terminal = reader["Terminal"].ToString();
                            flight.FlightNumber = reader["FlightNumber"].ToString();
                            flight.AircraftType = reader["AircraftType"].ToString();
                            flight.Seats = int.Parse(reader["Seats"].ToString());
                            flight.AvailableSeats = int.Parse(reader["AvailableSeats"].ToString());
                            flight.Destination = reader["Destination"].ToString();
                            flight.Origin = reader["Origin"].ToString();
                            flight.Airline = reader["Airline"].ToString();
                            flight.Status = reader["Status"].ToString();
                            flight.Gate = reader["Gate"].ToString();
                        }
                    }
                }
            }
            return flight;
        }
        public static void AdminUpdateFlight()
        {
            Console.WriteLine("Enter the FlightID of the flight you want to update: ");
            Console.WriteLine("If you don't want to update anything, type '0'");
            int flightId = int.Parse(Console.ReadLine());
            if (flightId == 0) { return; }

            Flight flightToUpdate = GetFlightById(flightId);

            if (AskQuestion("Do you want to update the departure time? (yes/no): ") == "yes")
            {
                Console.Write("Enter the new departure time (yyyy-MM-dd HH:mm:ss): ");
                DateTime newDepartureTime;
                if (DateTime.TryParse(Console.ReadLine(), out newDepartureTime))
                {
                    flightToUpdate.DepartureTime = newDepartureTime;
                }
                else
                {
                    Console.WriteLine("Invalid date and time format.");
                }
            }

            if (AskQuestion("Do you want to update the destination? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new destination: ");
                flightToUpdate.Destination = Console.ReadLine();
            }

            if (AskQuestion("Do you want to update the flight number? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new flight number: ");
                flightToUpdate.FlightNumber = Console.ReadLine();
            }

            if (AskQuestion("Do you want to update the aircraft type? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new aircraft type: ");
                flightToUpdate.AircraftType = Console.ReadLine();
            }

            if (AskQuestion("Do you want to update the seats? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new amount of seats: ");
                int newSeats;
                if (int.TryParse(Console.ReadLine(), out newSeats))
                {
                    flightToUpdate.Seats = newSeats;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            if (AskQuestion("Do you want to update the available seats? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new amount of available seats: ");
                int newAvailableSeats;
                if (int.TryParse(Console.ReadLine(), out newAvailableSeats))
                {
                    flightToUpdate.AvailableSeats = newAvailableSeats;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            if (AskQuestion("Do you want to update the origin? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new origin: ");
                flightToUpdate.Origin = Console.ReadLine();
            }

            if (AskQuestion("Do you want to update the status? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new status: ");
                flightToUpdate.Status = Console.ReadLine();
            }

            if (AskQuestion("Do you want to update the gate? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new gate: ");
                flightToUpdate.Gate = Console.ReadLine();
            }

            if (AskQuestion("Do you want to update the terminal? (yes/no): ") == "yes")
            {
                Console.WriteLine("Enter the new terminal: ");
                flightToUpdate.Terminal = Console.ReadLine();
            }

            UpdateFlight(flightToUpdate);
        }
        public static string AskQuestion(string question)
        {
            string input;
            do
            {
                Console.WriteLine(question);
                input = Console.ReadLine().ToLower();
                if (input == "yes" || input == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            } while (true);

            return input;
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
