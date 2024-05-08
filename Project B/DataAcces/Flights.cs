using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
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
            return $"Time: {DepartureTime:dd/MM/yyyy HH:mm}, Destination: {Destination}, Flight Number: {FlightNumber}, Gate: {Gate}, Terminal: {Terminal}";
        }
        public static void AddFlight(Flight flight)
        {
            using (SQLiteConnection sqlite_conn = CreateConnection())
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Flights (DepartureTime, Terminal, FlightNumber, AircraftType, Seats, AvailableSeats, Destination, Origin, Airline, Gate) VALUES (@DepartureTime, @Terminal, @FlightNumber, @AircraftType, @Seats, @AvailableSeats, @Destination, @Origin, @Airline, @Gate)";
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
                using (SQLiteCommand command = new SQLiteCommand("UPDATE Flights SET DepartureTime = @DepartureTime, Terminal = @Terminal, FlightNumber = @FlightNumber, AircraftType = @AircraftType, Seats = @Seats, AvailableSeats = @AvailableSeats, Destination = @Destination, Origin = @Origin, Airline = @Airline, Gate = @Gate WHERE FlightID = @FlightID", connection))
                {
                    // Add the parameters
                    command.Parameters.AddWithValue("@DepartureTime", flightToUpdate.DepartureTime);
                    command.Parameters.AddWithValue("@Terminal", flightToUpdate.Terminal);
                    command.Parameters.AddWithValue("@FlightNumber", flightToUpdate.FlightNumber);
                    command.Parameters.AddWithValue("@AircraftType", flightToUpdate.AircraftType);
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
                            // ... set the other properties ...

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
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete rows: {ex.Message}");
            }
        }
        public string ToAdminString()
        {
            return $"Flight Number: {FlightNumber}, Destination: {Destination}, Origin: {Origin}, Departure Time: {DepartureTime}, Terminal: {Terminal}, Gate: {Gate}, Aircraft Type: {AircraftType}, Airline: {Airline}";
        }
        public static void AdminUpdateFlight()
        {
            List<Flight> flights = GetFlights();
            int flightIndex = 0;

            // Display the title
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine("Update Flight".PadLeft(Console.WindowWidth / 2 + "Update Flight".Length / 2));
            Console.WriteLine(new string('-', Console.WindowWidth));

            Console.WriteLine("Select a flight to update (use arrow keys to navigate, press Enter to select):");
            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight.ToString());
            }

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    flightIndex = (flightIndex - 1 + flights.Count) % flights.Count;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    flightIndex = (flightIndex + 1) % flights.Count;
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }

                Console.CursorTop = Console.CursorTop - flights.Count;
                for (int i = 0; i < flights.Count; i++)
                {
                    if (i == flightIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(flights[i].ToString());

                    Console.ResetColor();
                }
            }
            Console.WriteLine("Selected flight with ID: " + flights[flightIndex].FlightId);

            Flight flightToUpdate = flights[flightIndex];
            flightToUpdate.FlightId = flights[flightIndex].FlightId;

            // Store the original flight information
            string originalFlightInfo = flightToUpdate.ToAdminString();

            // List of properties to update
            string[] properties = { "Flight Number", "Destination", "Origin", "Departure Time", "Terminal", "Gate", "Aircraft Type", "Airline" };
            foreach (string property in properties)
            {
                string[] updateOptions = { "Yes", "No" };
                    int updateIndex = 0;

                // Clear the console and display the flight information
                Console.Clear();
                Console.WriteLine(new string('-', Console.WindowWidth));
                Console.WriteLine($"Flight Selected: {flightToUpdate.FlightId})".PadLeft(Console.WindowWidth / 2 + $"Flight Selected (Flight ID: {flightToUpdate.FlightId})".Length / 2));
                Console.WriteLine(new string('-', Console.WindowWidth));
                Console.WriteLine(flightToUpdate.ToAdminString());

                Console.WriteLine($"Do you want to update the {property}? (use arrow keys to navigate, press Enter to select): ");

                foreach (var option in updateOptions)
                {
                    Console.WriteLine(option);
                }

                while (true)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow)
                    {
                        updateIndex = (updateIndex - 1 + updateOptions.Length) % updateOptions.Length;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        updateIndex = (updateIndex + 1) % updateOptions.Length;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        break;
                    }

                    Console.CursorTop = Console.CursorTop - updateOptions.Length;
                    for (int i = 0; i < updateOptions.Length; i++)
                    {
                        if (i == updateIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(updateOptions[i]);

                        Console.ResetColor();
                    }
                }

                if (updateOptions[updateIndex] == "Yes")
                {
                    // Update the property
                    switch (property)
                    {
                        case "Flight Number":
                            // Update flight number
                            Console.Write("Enter new flight number (1000-9999): ");
                            string flightNumberString = Console.ReadLine();
                            if (flightNumberString?.ToLower() == "exit") return;
                            int flightNumber;
                            while (!int.TryParse(flightNumberString, out flightNumber) || flightNumber < 1000 || flightNumber > 9999)
                            {
                                Console.Write("Invalid input. Please enter a number between 1000 and 9999: ");
                                flightNumberString = Console.ReadLine();
                                if (flightNumberString?.ToLower() == "exit") return;
                            }
                            flightToUpdate.FlightNumber = flightNumber.ToString();
                            break;

                        case "Destination":
                            // Update destination
                            Console.Write("Enter new destination: ");
                            string destination = Console.ReadLine();
                            if (destination?.ToLower() == "exit") return;
                            flightToUpdate.Destination = destination;
                            break;

                        case "Origin":
                            // Update origin
                            string[] originOptions = { "Amsterdam", "Exit" };
                            int originIndex = 0;
                            Console.WriteLine("Select new origin: ");
                            foreach (var option in originOptions)
                            {
                                Console.WriteLine(option);
                            }
                            while (true)
                            {
                                var key = Console.ReadKey(true).Key;
                                if (key == ConsoleKey.UpArrow)
                                {
                                    originIndex = (originIndex - 1 + originOptions.Length) % originOptions.Length;
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    originIndex = (originIndex + 1) % originOptions.Length;
                                }
                                else if (key == ConsoleKey.Enter)
                                {
                                    if (originOptions[originIndex] == "Exit")
                                    {
                                        return;
                                    }
                                    break;
                                }
                                Console.CursorTop = Console.CursorTop - originOptions.Length;
                                for (int i = 0; i < originOptions.Length; i++)
                                {
                                    if (i == originIndex)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    }
                                    Console.WriteLine(originOptions[i]);
                                    Console.ResetColor();
                                }
                            }
                            flightToUpdate.Origin = originOptions[originIndex];
                            break;

                        case "Departure Time":
                            // Update departure time
                            Console.Write("Enter new departure time (dd/MM/yyyy HH:mm): ");
                            DateTime departureTime;
                            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out departureTime))
                            {
                                Console.Write("Invalid input. Please enter a date and time in the format dd/MM/yyyy HH:mm: ");
                            }
                            flightToUpdate.DepartureTime = departureTime;
                            break;
                        case "Terminal":
                            // Update terminal
                            Console.Write("Enter new terminal (1-4): ");
                            string terminalString = Console.ReadLine();
                            if (terminalString?.ToLower() == "exit") return;
                            int terminal;
                            while (!int.TryParse(terminalString, out terminal) || terminal < 1 || terminal > 4)
                            {
                                Console.Write("Invalid input. Please enter a number between 1 and 4: ");
                                terminalString = Console.ReadLine();
                                if (terminalString?.ToLower() == "exit") return;
                            }
                            flightToUpdate.Terminal = terminal.ToString();
                            break;

                        case "Gate":
                            // Update gate
                            Console.Write("Enter new gate (1-24): ");
                            string gateString = Console.ReadLine();
                            if (gateString?.ToLower() == "exit") return;
                            int gate;
                            while (!int.TryParse(gateString, out gate) || gate < 1 || gate > 24)
                            {
                                Console.Write("Invalid input. Please enter a number between 1 and 24: ");
                                gateString = Console.ReadLine();
                                if (gateString?.ToLower() == "exit") return;
                            }
                            flightToUpdate.Gate = gate.ToString();
                            break;

                        case "Aircraft Type":
                            // Update aircraft type
                            string[] aircraftTypeOptions = { "Boeing 787", "Boeing 737", "Airbus 330", "Exit" };
                            int aircraftTypeIndex = 0;
                            Console.WriteLine("Enter new aircraft type (use arrow keys to navigate, press Enter to select): ");
                            foreach (var option in aircraftTypeOptions)
                            {
                                Console.WriteLine(option);
                            }
                            while (true)
                            {
                                var key = Console.ReadKey(true).Key;
                                if (key == ConsoleKey.UpArrow)
                                {
                                    aircraftTypeIndex = (aircraftTypeIndex - 1 + aircraftTypeOptions.Length) % aircraftTypeOptions.Length;
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    aircraftTypeIndex = (aircraftTypeIndex + 1) % aircraftTypeOptions.Length;
                                }
                                else if (key == ConsoleKey.Enter)
                                {
                                    if (aircraftTypeOptions[aircraftTypeIndex] == "Exit")
                                    {
                                        return;
                                    }
                                    break;
                                }
                                Console.CursorTop = Console.CursorTop - aircraftTypeOptions.Length;
                                for (int i = 0; i < aircraftTypeOptions.Length; i++)
                                {
                                    if (i == aircraftTypeIndex)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    }
                                    Console.WriteLine(aircraftTypeOptions[i]);
                                    Console.ResetColor();
                                }
                            }
                            flightToUpdate.AircraftType = aircraftTypeOptions[aircraftTypeIndex];
                            break;

                        case "Airline":
                            // Update airline
                            flightToUpdate.Airline = "New South";
                            break;
                    }
                }
            }
            string updatedFlightInfo = flightToUpdate.ToAdminString();

            // Ask the admin if they want to save the changes
            string[] confirmSaveOptions = { "Save changes", "Exit without saving" };
            int confirmSaveIndex = 0;

            Console.Write("Original flight information: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(originalFlightInfo);

            Console.ResetColor();
            Console.Write("Updated flight information: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(updatedFlightInfo);

            Console.ResetColor();
            Console.WriteLine("Do you want to save the changes?:");
            foreach (var option in confirmSaveOptions)
            {
                Console.WriteLine(option);
            }

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    confirmSaveIndex = (confirmSaveIndex - 1 + confirmSaveOptions.Length) % confirmSaveOptions.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    confirmSaveIndex = (confirmSaveIndex + 1) % confirmSaveOptions.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }

                Console.CursorTop = Console.CursorTop - confirmSaveOptions.Length;
                for (int i = 0; i < confirmSaveOptions.Length; i++)
                {
                    if (i == confirmSaveIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(confirmSaveOptions[i]);

                    Console.ResetColor();
                }
            }

            if (confirmSaveOptions[confirmSaveIndex] == "Save changes")
            {
                UpdateFlight(flightToUpdate);
                Console.WriteLine("Flight updated successfully!");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("Changes not saved.");
                System.Threading.Thread.Sleep(3000);
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
        private static List<string> cities = new List<string>()
        {
            "London", "Paris", "Berlin", "Rome", "Madrid", "Barcelona", "Amsterdam", "Vienna", "Prague", "Dublin",
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
                Gate = random.Next(1, 24).ToString()
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
                Gate = random.Next(1, 24).ToString()
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
                Gate = random.Next(1, 24).ToString()
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

        public static void AdminAddFlight()
        {
            Console.Clear();
            string title = "Add Flight";
            int windowWidth = Console.WindowWidth;
            int titlePadding = (windowWidth - title.Length) / 2;
            string titleLine = new string('-', windowWidth);

            Console.WriteLine(titleLine);
            Console.WriteLine($"{new string(' ', titlePadding)}{title}");
            Console.WriteLine(titleLine);
            Console.WriteLine("Enter flight details (or type 'exit' to cancel):");

            Console.Write("Enter flight number (1000-9999): ");
            string flightNumberString = Console.ReadLine();
            if (flightNumberString?.ToLower() == "exit") return;
            int flightNumber;
            while (!int.TryParse(flightNumberString, out flightNumber) || flightNumber < 1000 || flightNumber > 9999)
            {
                Console.Write("Invalid input. Please enter a number between 1000 and 9999: ");
                flightNumberString = Console.ReadLine();
                if (flightNumberString?.ToLower() == "exit") return;
            }

            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();
            if (destination?.ToLower() == "exit") return;

            string[] originOptions = { "Amsterdam", "Exit" };
            int originIndex = 0;

            Console.WriteLine("Select origin: ");

            foreach (var option in originOptions)
            {
                Console.WriteLine(option);
            }

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    originIndex = (originIndex - 1 + originOptions.Length) % originOptions.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    originIndex = (originIndex + 1) % originOptions.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (originOptions[originIndex] == "Exit")
                    {
                        return;
                    }
                    break;
                }

                Console.CursorTop = Console.CursorTop - originOptions.Length;
                for (int i = 0; i < originOptions.Length; i++)
                {
                    if (i == originIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(originOptions[i]);

                    Console.ResetColor();
                }
            }

            string origin = originOptions[originIndex];

            Console.Write("Enter departure time (dd/MM/yyyy HH:mm): ");
            DateTime departureTime;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out departureTime))
            {
                Console.Write("Invalid input. Please enter a date and time in the format dd/MM/yyyy HH:mm: ");
            }

            Console.Write("Enter terminal (1-4): ");
            string terminalString = Console.ReadLine();
            if (terminalString?.ToLower() == "exit") return;
            int terminal;
            while (!int.TryParse(terminalString, out terminal) || terminal < 1 || terminal > 4)
            {
                Console.Write("Invalid input. Please enter a number between 1 and 4: ");
                terminalString = Console.ReadLine();
                if (terminalString?.ToLower() == "exit") return;
            }

            Console.Write("Enter gate (1-24): ");
            string gateString = Console.ReadLine();
            if (gateString?.ToLower() == "exit") return;
            int gate;
            while (!int.TryParse(gateString, out gate) || gate < 1 || gate > 24)
            {
                Console.Write("Invalid input. Please enter a number between 1 and 24: ");
                gateString = Console.ReadLine();
                if (gateString?.ToLower() == "exit") return;
            }

            string[] aircraftTypeOptions = { "Boeing 787", "Boeing 737", "Airbus 330", "Exit" };
            int aircraftTypeIndex = 0;

            Console.WriteLine("Enter aircraft type (use arrow keys to navigate, press Enter to select): ");

            foreach (var option in aircraftTypeOptions)
            {
                Console.WriteLine(option);
            }

            int seats = 0, availableSeats = 0;
            string aircraftType = "";

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    aircraftTypeIndex = (aircraftTypeIndex - 1 + aircraftTypeOptions.Length) % aircraftTypeOptions.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    aircraftTypeIndex = (aircraftTypeIndex + 1) % aircraftTypeOptions.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    aircraftType = aircraftTypeOptions[aircraftTypeIndex];
                    if (aircraftType == "Exit")
                    {
                        return;
                    }
                    else if (aircraftType == "Boeing 787")
                    {
                        seats = availableSeats = 219;
                    }
                    else if (aircraftType == "Boeing 737")
                    {
                        seats = availableSeats = 186;
                    }
                    else if (aircraftType == "Airbus 330")
                    {
                        seats = availableSeats = 345;
                    }
                    break;
                }

                Console.CursorTop = Console.CursorTop - aircraftTypeOptions.Length;
                for (int i = 0; i < aircraftTypeOptions.Length; i++)
                {
                    if (i == aircraftTypeIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(aircraftTypeOptions[i]);

                    Console.ResetColor();
                }
            }

            string airline = "New South";

            Flight newFlight = new Flight
            {
                FlightNumber = flightNumber.ToString(),
                Destination = destination,
                Origin = origin,
                DepartureTime = departureTime,
                Terminal = terminal.ToString(),
                AircraftType = aircraftType,
                Gate = gate.ToString(),
                Seats = seats,
                AvailableSeats = availableSeats,
                Airline = airline
            };

            AddFlight(newFlight);

            Console.WriteLine("Flight added successfully!");
            System.Threading.Thread.Sleep(3000);
        }
    }
}
