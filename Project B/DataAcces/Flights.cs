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
            return $"Time: {DepartureTime:dd/MM/yyyy HH:mm}, Origin: {Origin}, Destination: {Destination}, Flight Number: {FlightNumber}, Gate: {Gate}, Terminal: {Terminal}";
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
                            flight.Gate = reader.GetString(reader.GetOrdinal("Gate"));
                            flight.Destination = reader.GetString(reader.GetOrdinal("Destination"));
                            flight.Origin = reader.GetString(reader.GetOrdinal("Origin"));
                            flight.DepartureTime = reader.GetDateTime(reader.GetOrdinal("DepartureTime"));
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

            // Sort flights by time
            flights = flights.OrderBy(flight => flight.DepartureTime).ToList();

            int selectedRow = 0;
            int currentPage = 0;
            int rowsPerPage = 20;

            while (true)
            {
                var flightTable = new Table().Border(TableBorder.Rounded);
                flightTable.AddColumn("Time");
                flightTable.AddColumn("Origin");
                flightTable.AddColumn("Destination");
                flightTable.AddColumn("Flight Number");
                flightTable.AddColumn("Gate");
                flightTable.AddColumn("Terminal");

                int startRow = currentPage * rowsPerPage;
                int endRow = Math.Min(startRow + rowsPerPage, flights.Count);

                for (int i = startRow; i < endRow; i++)
                {
                    var flight = flights[i];
                    if (i == selectedRow)
                    {
                        flightTable.AddRow($"[green]{flight.DepartureTime}[/]", $"[green]{flight.Origin}[/]", $"[green]{flight.Destination}[/]", $"[green]{flight.FlightNumber}[/]", $"[green]{flight.Gate}[/]", $"[green]{flight.Terminal}[/]");
                    }
                    else
                    {
                        flightTable.AddRow(flight.DepartureTime.ToString(), flight.Origin, flight.Destination, flight.FlightNumber, flight.Gate, flight.Terminal);
                    }
                }

                AnsiConsole.Render(flightTable);

                // Display navigation instructions and current page number
                AnsiConsole.MarkupLine($"Page [green]{currentPage + 1}[/] of [green]{(flights.Count - 1) / rowsPerPage + 1}[/]");
                AnsiConsole.MarkupLine("[blue]Options:[/]");
                AnsiConsole.MarkupLine("[green]Up Arrow[/]: Move selection up");
                AnsiConsole.MarkupLine("[green]Down Arrow[/]: Move selection down");
                AnsiConsole.MarkupLine("[green]Enter[/]: Select option");
                AnsiConsole.MarkupLine("[blue]Navigation:[/]");
                AnsiConsole.MarkupLine("[green]N[/]: Next page");
                AnsiConsole.MarkupLine("[green]P[/]: Previous page");
                AnsiConsole.MarkupLine("[green]B[/]: Back to previous menu");

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedRow = Math.Max(0, selectedRow - 1);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedRow = Math.Min(flights.Count - 1, selectedRow + 1);
                }
                else if (key.Key == ConsoleKey.P)
                {
                    currentPage = Math.Max(0, currentPage - 1);
                    selectedRow = currentPage * rowsPerPage;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    currentPage = Math.Min((flights.Count - 1) / rowsPerPage, currentPage + 1);
                    selectedRow = currentPage * rowsPerPage;
                }
                else if (key.Key == ConsoleKey.B)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Handle selection
                    Flight selectedFlight = flights[selectedRow];
                    // Continue with your update logic here...
                    Flight flightToUpdate = new Flight
                    {
                        FlightId = selectedFlight.FlightId,
                        FlightNumber = selectedFlight.FlightNumber,
                        Destination = selectedFlight.Destination,
                        Origin = selectedFlight.Origin,
                        DepartureTime = selectedFlight.DepartureTime,
                        Terminal = selectedFlight.Terminal,
                        Gate = selectedFlight.Gate,
                        AircraftType = selectedFlight.AircraftType
                    };

                    AnsiConsole.Clear();

                    string[] properties = { "Flight Number", "Destination", "Origin", "Departure Time", "Terminal", "Gate", "Aircraft Type"};

                    foreach (string property in properties)
                    {
                        var updatePropertyPrompt = new SelectionPrompt<string>()
                            .Title($"Do you want to update the {property}?")
                            .AddChoices(new List<string> { "Yes", "No" });

                        var updateProperty = AnsiConsole.Prompt(updatePropertyPrompt) == "Yes";

                        if (updateProperty)
                        {
                            // Update the property
                            switch (property)
                            {
                                case "Flight Number":
                                    // Update flight number
                                    flightToUpdate.FlightNumber = AnsiConsole.Ask<string>("Enter new flight number (1000-9999): ");
                                    break;

                                case "Destination":
                                    // Update destination
                                    flightToUpdate.Destination = AnsiConsole.Ask<string>("Enter new destination: ");
                                    break;

                                case "Origin":
                                    // Update origin
                                    flightToUpdate.Origin = AnsiConsole.Ask<string>("Enter new origin: ");
                                    break;

                                case "Departure Time":
                                    // Update departure time
                                    string departureTimeString = AnsiConsole.Ask<string>("Enter new departure time (dd/MM/yyyy HH:mm): ");
                                    flightToUpdate.DepartureTime = DateTime.ParseExact(departureTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                                    break;

                                case "Terminal":
                                    // Update terminal
                                    flightToUpdate.Terminal = AnsiConsole.Ask<string>("Enter new terminal: ");
                                    break;

                                case "Gate":
                                    // Update gate
                                    flightToUpdate.Gate = AnsiConsole.Ask<string>("Enter new gate: ");
                                    break;

                                case "Aircraft Type":
                                    // Update aircraft type
                                    flightToUpdate.AircraftType = AnsiConsole.Ask<string>("Enter new aircraft type: ");
                                    break;
                            }
                        }
                    }

                    // Display the original and updated flight information
                    AnsiConsole.MarkupLine($"[red]Original flight information:[/]\n{selectedFlight}");
                    AnsiConsole.MarkupLine($"[green]Updated flight information:[/]\n{flightToUpdate}");

                    // Ask the user if they want to save the changes
                    var saveChangesPrompt = new SelectionPrompt<string>()
                        .Title("Do you want to save the changes?")
                        .AddChoices(new List<string> { "Yes", "No" });

                    var saveChanges = AnsiConsole.Prompt(saveChangesPrompt) == "Yes";

                    if (saveChanges)
                    {
                        // Update the original flight with the new information
                        selectedFlight.FlightNumber = flightToUpdate.FlightNumber;
                        selectedFlight.Destination = flightToUpdate.Destination;
                        selectedFlight.Origin = flightToUpdate.Origin;
                        selectedFlight.DepartureTime = flightToUpdate.DepartureTime;
                        selectedFlight.Terminal = flightToUpdate.Terminal;
                        selectedFlight.Gate = flightToUpdate.Gate;
                        selectedFlight.AircraftType = flightToUpdate.AircraftType;

                        // Save the updated flight information
                        UpdateFlight(selectedFlight);

                        AnsiConsole.MarkupLine("[green]Flight updated successfully![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Changes not saved.[/]");
                    }

                    break;
                }

                AnsiConsole.Clear();
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
    
        // Filter-systeem:
        // Implementeer een zoekfilter waarmee gebruikers vluchten naar een specifieke locatie kunnen vinden.
        // Implementeer functionaliteit om zoekopdrachten te filteren op bestemming, waardoor gebruikers alle beschikbare vluchten naar die specifieke locatie kunnen bekijken.
        // Ontwikkel filters voor onder andere vertrekdatum en luchtvaartmaatschappij om de zoekresultaten te verbeteren.
        public static List<Flight> FilterFlights()
        {
            AnsiConsole.Clear();
            List<Flight> flights = GetFlights();

            if (flights == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Failed to get flights.[/]");
                return new List<Flight>(); // return an empty list if there's an error
            }

            var yesNoOptions = new[] { "Yes", "No" };

            var filterByDestination = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to filter by destination?")
                    .AddChoices(yesNoOptions));

            if (filterByDestination == "Yes")
            {
                var destinations = flights.Select(f => f.Destination).Distinct().OrderBy(d => d).ToList();
                var selectedDestination = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select a destination:")
                        .PageSize(10)
                        .AddChoices(destinations));

                flights = flights.Where(f => f.Destination == selectedDestination).ToList();
            }

            var filterByDepartureTime = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to filter by departure time?")
                    .AddChoices(yesNoOptions));

            if (filterByDepartureTime == "Yes")
            {
                var departureTimes = flights.Select(f => f.DepartureTime.ToString()).Distinct().OrderBy(d => d).ToList();
                var selectedDepartureTime = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select a departure time:")
                        .PageSize(10)
                        .AddChoices(departureTimes));

                flights = flights.Where(f => f.DepartureTime.ToString() == selectedDepartureTime).ToList();
            }

            return flights; // return the filtered flights
        }

        public static void AdminAddFlight()
        {
            AnsiConsole.Clear();

            int consoleWidth = Console.WindowWidth;
            string title = "Add Flight";
            int padding = (consoleWidth - title.Length) / 2;

            AnsiConsole.WriteLine(new string('-', consoleWidth));
            AnsiConsole.WriteLine($"{new string(' ', padding)}{title}{new string(' ', padding)}");
            AnsiConsole.WriteLine(new string('-', consoleWidth));

            var flightNumber = AnsiConsole.Prompt(new TextPrompt<int>("Enter flight number: (1000-9999)")
                .Validate(value => value >= 1000 && value <= 9999, "Please enter a number between 1000 and 9999"));

            var destination = AnsiConsole.Ask<string>("Enter destination: ");

            var origin = AnsiConsole.Ask<string>("Enter origin: ");

            var departureTimeString = AnsiConsole.Prompt(new TextPrompt<string>("Enter departure time: (dd/MM/yyyy HH:mm)")
                .Validate(value => DateTime.TryParseExact(value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) && date > DateTime.Now, "Please enter a future date and time in the format dd/MM/yyyy HH:mm"));

            DateTime departureTime = DateTime.ParseExact(departureTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            var terminal = AnsiConsole.Prompt(new TextPrompt<int>("Enter terminal: (1-4)")
                .Validate(value => value >= 1 && value <= 4, "Please enter a number between 1 and 4"));

            var gate = AnsiConsole.Prompt(new TextPrompt<int>("Enter gate: (1-24)")
                .Validate(value => value >= 1 && value <= 24, "Please enter a number between 1 and 24"));

            var aircraftTypeOptions = new[] { "Boeing 787", "Boeing 737", "Airbus 330", "Exit" };
            var aircraftType = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Enter aircraft type: ")
                .AddChoices(aircraftTypeOptions));
            if (aircraftType == "Exit") return;

            int seats = 0, availableSeats = 0;
            if (aircraftType == "Boeing 787")
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
            
            AnsiConsole.Clear();
            // Show the flight information
            AnsiConsole.MarkupLine($"[blue]Flight Number: [/][green]{newFlight.FlightNumber}[/]");
            AnsiConsole.MarkupLine($"[blue]Destination: [/][green]{newFlight.Destination}[/]");
            AnsiConsole.MarkupLine($"[blue]Origin: [/][green]{newFlight.Origin}[/]");
            AnsiConsole.MarkupLine($"[blue]Departure Time: [/][green]{newFlight.DepartureTime}[/]");
            AnsiConsole.MarkupLine($"[blue]Terminal: [/][green]{newFlight.Terminal}[/]");
            AnsiConsole.MarkupLine($"[blue]Gate: [/][green]{newFlight.Gate}[/]");
            AnsiConsole.MarkupLine($"[blue]Aircraft Type: [/][green]{newFlight.AircraftType}[/]");
            AnsiConsole.MarkupLine($"[blue]Seats: [/][green]{newFlight.Seats}[/]");
            AnsiConsole.MarkupLine($"[blue]Available Seats: [/][green]{newFlight.AvailableSeats}[/]");

            // Ask the user if they really want to add the flight
            var confirmationOptions = new[] { "Yes", "No" };
            var confirmation = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Do you really want to add this flight?")
                .AddChoices(confirmationOptions));

            if (confirmation == "Yes")
            {
                AddFlight(newFlight);
                AnsiConsole.MarkupLine("[green]Flight added successfully![/]");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Flight not added.[/]");
                System.Threading.Thread.Sleep(3000);
            }
        }
        // function for adding tickets to a plane
        public static void reserveseat(int flightid, int userid, string seat, string seatclass, string extranotes)
        {
            Users user = Users.GetuserbyId(userid);
            Flight flight = GetFlightById(flightid);
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            DateTime time = DateTime.Now;
            string sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Origin, Extranotes ) VALUES('{user.Email}','{time}', '{user.Name}','{seat}', '{seatclass}', {flightid}, {user.Id}, '{flight.Gate}', '{flight.DepartureTime}', '{flight.Destination}', '{flight.Origin}', '{extranotes}');";
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                {
                    cmd1.ExecuteNonQuery();
                }
            }
        }
    }
}
