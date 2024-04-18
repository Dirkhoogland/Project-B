using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Project_B.Presentation;
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
        public Seat? PlaneLayout { get; set; }
        public int FlightId { get; set; }

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
            catch (Exception ex) 
            { 
                Console.WriteLine($"Failed to open SQLite connection: {ex.Message}");
            }
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
                catch (FormatException ex)
                {
                    Console.WriteLine($"Failed to parse flight data: {ex.Message}");
                }
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
                        Status = sqlite_datareader["Status"].ToString(),
                        Gate = sqlite_datareader["Gate"].ToString()
                    };
                    flights.Add(flight);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to parse flight data: {ex.Message}");
                }
            }
            sqlite_conn.Close();
            return flights;
        }
        
        public override string ToString()
        {
            return $"Time: {DepartureTime:HH:mm}, Destination: {Destination}, Flight Number: {FlightNumber}, Gate: {Gate}, Status: {Status}, Terminal: {Terminal}";
        }
        public static void AddFlight(Flight flight)
        {
            using (SQLiteConnection sqlite_conn = CreateConnection())
            {
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
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete rows: {ex.Message}");
            }
        }
        public static void AdminUpdateFlight()
        {
            Console.WriteLine("Enter the FlightID of the flight you want to update: ");
            Console.WriteLine("If you don't want to update anything, type '0'");

            int flightId;
            while (!int.TryParse(Console.ReadLine(), out flightId))
            {
                Console.WriteLine("\nInvalid input. Please enter a number.");
                Console.WriteLine("Enter the FlightID of the flight you want to update: ");
                Console.WriteLine("If you don't want to update anything, type '0'");
            }

            if (flightId == 0) 
            {
                Console.Clear();
                return; 
            }

            Flight flightToUpdate = GetFlightById(flightId);

            UpdateFlightProperty<Flight, DateTime>(flightToUpdate, "\nDo you want to update the departure time? (yes/no): ", 
                "Enter the new departure time (yyyy-MM-dd HH:mm:ss): ", 
                DateTime.TryParse, 
                (flight, value) => flight.DepartureTime = value);

            UpdateFlightProperty<Flight, string>(flightToUpdate, "\nDo you want to update the destination? (yes/no): ", 
                "Enter the new destination: ", 
                (string input, out string result) => { result = input; return true; }, 
                (flight, value) => flight.Destination = value);

            UpdateFlightProperty<Flight, int>(flightToUpdate, "\nDo you want to update the number of seats? (yes/no): ", 
                "Enter the new number of seats: ", 
                int.TryParse, 
                (flight, value) => flight.Seats = value);

            UpdateFlightProperty<Flight, int>(flightToUpdate, "\nDo you want to update the number of available seats? (yes/no): ", 
                "Enter the new number of available seats: ", 
                int.TryParse, 
                (flight, value) => flight.AvailableSeats = value);

            UpdateFlightProperty<Flight, string>(flightToUpdate, "\nDo you want to update the gate? (yes/no): ", 
                "Enter the new gate: (1-20) ", 
                (string input, out string result) => { result = input; return true; }, 
                (flight, value) => flight.Gate = value);

            UpdateFlightProperty<Flight, string>(flightToUpdate, "\nDo you want to update the terminal? (yes/no): ", 
                "Enter the new terminal: (1-4) ", 
                (string input, out string result) => { result = input; return true; }, 
                (flight, value) => flight.Terminal = value);

            UpdateFlightProperty<Flight, string>(flightToUpdate, "\nDo you want to update the airline? (yes/no): ", 
                "Enter the new airline: ", 
                (string input, out string result) => { result = input; return true; }, 
                (flight, value) => flight.Airline = value);

            UpdateFlight(flightToUpdate);
            Console.WriteLine("\nFlight updated.");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
        }

        public delegate bool TryParseDelegate<T>(string input, out T result);

        public static void UpdateFlightProperty<TFlight, TProperty>(TFlight flightToUpdate, string question, string prompt, TryParseDelegate<TProperty> parse, Action<TFlight, TProperty> update)
            where TFlight : Flight
        {
            if (AskQuestion(question) == "yes")
            {
                Console.Write(prompt);
                TProperty newValue;
                if (parse(Console.ReadLine(), out newValue))
                {
                    update(flightToUpdate, newValue);
                    Console.WriteLine("Property updated.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
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
                Status = "On time",
                Gate = "1"
            };
            AddFlight(flight);
        }
        
        // functie om een paar vluchten aan te maken voor test
        public static void CreateTestFlights()
        {
            Flight.CreateFlightBoeing737();
            Flight.CreateFlightBoeing787();
            Flight.CreateFlightAirbus330();
        }
        // Filter-systeem:
        // Implementeer een zoekfilter waarmee gebruikers vluchten naar een specifieke locatie kunnen vinden.
        // Implementeer functionaliteit om zoekopdrachten te filteren op bestemming, waardoor gebruikers alle beschikbare vluchten naar die specifieke locatie kunnen bekijken.
        // Ontwikkel filters voor onder andere vertrekdatum en luchtvaartmaatschappij om de zoekresultaten te verbeteren.
        public static List<Flight> FilterFlights()
        {
            Console.Clear();
            List<Flight> flights = GetFlights();

            if (flights == null)
            {
                Console.WriteLine("Error: Failed to get flights.");
                return new List<Flight>(); // return an empty list if there's an error
            }

            int currentOption = 0;
            string[] yesNoOptions = new string[] { "yes", "no" };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Do you want to filter by destination?");

                for (int i = 0; i < yesNoOptions.Length; i++)
                {
                    if (i == currentOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(yesNoOptions[i]);

                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentOption > 0) currentOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentOption < yesNoOptions.Length - 1) currentOption++;
                        break;
                    case ConsoleKey.Enter:
                        if (yesNoOptions[currentOption] == "yes")
                        {
                            flights = FilterByDestination(flights);
                        }
                        // Continue with the next filter
                        currentOption = 0; // reset the current option for the next question
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Do you want to filter by departure time?");

                            for (int i = 0; i < yesNoOptions.Length; i++)
                            {
                                if (i == currentOption)
                                {
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }

                                Console.WriteLine(yesNoOptions[i]);

                                Console.ResetColor();
                            }

                            keyInfo = Console.ReadKey(true);

                            switch (keyInfo.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    if (currentOption > 0) currentOption--;
                                    break;
                                case ConsoleKey.DownArrow:
                                    if (currentOption < yesNoOptions.Length - 1) currentOption++;
                                    break;
                                case ConsoleKey.Enter:
                                    if (yesNoOptions[currentOption] == "yes")
                                    {
                                        flights = FilterByDepartureTime(flights);
                                    }
                                    return flights; // return the filtered flights
                            }
                        }
                }
            }
        }
        public static List<Flight> FilterByDestination(List<Flight> flights)
        {
            List<string> destinations = flights.Select(f => f.Destination).Distinct().ToList();
            int currentOption = 0;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < destinations.Count; i++)
                {
                    if (i == currentOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(destinations[i]);

                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentOption > 0) currentOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentOption < destinations.Count - 1) currentOption++;
                        break;
                    case ConsoleKey.Enter:
                        return flights.Where(f => f.Destination == destinations[currentOption]).ToList();
                }
            }
        }
        public static List<Flight> FilterByDepartureTime(List<Flight> flights)
        {
            List<DateTime> departureDates = flights.Select(f => f.DepartureTime.Date).Distinct().ToList();
            int currentOption = 0;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < departureDates.Count; i++)
                {
                    if (i == currentOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(departureDates[i].ToString("dd/MM/yyyy"));

                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentOption > 0) currentOption--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentOption < departureDates.Count - 1) currentOption++;
                        break;
                    case ConsoleKey.Enter:
                        return flights.Where(f => f.DepartureTime.Date == departureDates[currentOption]).ToList();
                }
            }
        }
        public static List<Flight> FlightInformation()
        {
            var flights = Flight.GetFlights();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
            return flights;
        }
    }
}
