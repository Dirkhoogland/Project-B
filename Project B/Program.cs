using Project_B.DataAcces;
using Project_B.Presentation;
using System.Xml.Linq;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Project_B
{
    namespace Project_B
    {
        public class Program
        {
              
            public static Flight SelectFlight(List<Flight> flights)
            {
                var flightPrompt = new SelectionPrompt<Flight>()
                    .Title("Please select a flight:")
                    .PageSize(10)
                    .AddChoices(flights);

                return AnsiConsole.Prompt(flightPrompt);
            }

            public static void PrintImages()
            {
                string[] image1 = new string[]
                {
                    "                      ___",
                    "                      \\\\ \\",
                    "                       \\\\ `\\",
                    "    ___                 \\\\  \\",
                    "   |    \\                \\\\  `\\",
                    "   |____\\                \\    \\",
                    "   |______\\                \\    `\\",
                    "   |       \\                \\     \\",
                    "   |      __\\__---------------------------------._.",
                    " __|---~~~__o_o_o_o_o_o_o_o_o_o_o_o_o_o_o_o_o_o_o_[]\\",
                    "|___                         /~      )  New South     \\__",
                    "    ~~~---..._______________/      ,/_________________/",
                    "                           /      /",
                    "                          /     ,/",
                    "                         /     /",
                    "                        /    ,/",
                    "                       /    /",
                    "                      //  ,/",
                    "                     //  /",
                    "                    // ,/",
                    "                   //_/"
                };

                string[] image2 = new string[]
                {
                    "",
                    "",
                    "",
                    "",
                    "__|__",
                    "\\___/",
                    " | |",
                    " | |",
                    "_|_|______________",
                    "        /|\\",
                    "      */ | \\*",
                    "      / -+- \\",
                    "---o--(_)--o---",
                    "    /  0 \" 0  \\",
                    "  */     |     \\*",
                    "  /      |      \\",
                    "*/       |       \\*"
                };

                for (int i = 0; i < Math.Max(image1.Length, image2.Length); i++)
                {
                    string line1 = i < image1.Length ? image1[i] : new string(' ', image1.Max(s => s.Length));
                    string line2 = i < image2.Length ? image2[i] : "";

                    AnsiConsole.WriteLine(line1.PadRight(image1.Max(s => s.Length)) + "    " + line2);
                }
            }
            private static CurrentUser Login()
            {
                CurrentUser currentuser = null;
                return currentuser = LoginRegistrations.LoginScreen();
            }
            public static void Main(string[] args)
            {
<<<<<<< Updated upstream
=======
                Console.OutputEncoding = System.Text.Encoding.Unicode;
>>>>>>> Stashed changes
                DataAccess.Database();

                var menuItemsGuest = new[] { "Login/Register", "Exit" };
                var menuItemsUser = new[] { "View Flights", "Flight History", "Logout", "Exit" };
                var menuItemsAdmin = new[] { "View Flights", "Flight History", "Manage Flights", "Manage Users", "Add Fly Points to user", "Logout", "Exit" };
                var manageFlightsMenu = new[] { "Add Flight", "Update Flight", "Back" };

                string[] menuItems = menuItemsGuest; // Default to guest menu
                int currentIndex = 0;
                CurrentUser currentuser = null;
                bool isFilterActive = false;
                int currentOption = 0;
                string filterFlightsText = " Filter Flights ";
                string removeFiltersText = " Remove Filters ";
                string backText = " Back ";
                int currentPage = 0;
                int itemsPerPage = 20;
                List<string> options = new List<string> { filterFlightsText };

                while (true)
                {
                    AnsiConsole.Clear();

                    if (currentuser == null)
                    {
                        PrintImages();
                    }
                    if (currentuser != null)
                    {
                        string loginText = $"Logged in as {currentuser.Name}";
                        int padding = (Console.WindowWidth - loginText.Length) / 2;
                        AnsiConsole.WriteLine(new string('-', padding) + loginText + new string('-', padding));
                    }

                    var selectedOption = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(menuItems));

                    switch (selectedOption)
                    {
                        case "Login/Register":
                            AnsiConsole.Clear();
                            currentuser = Login();
                            if (currentuser != null)
                            {
                                if (currentuser.rank == 1)
                                {
                                    menuItems = menuItemsAdmin;
                                }
                                else
                                {
                                    menuItems = menuItemsUser;
                                }
                            }
                            AnsiConsole.Clear();
                            break;
                        case "Logout":
                            currentuser = null;
                            menuItems = menuItemsGuest;
                            break;
                        case "Exit":
                            return;
                        case "View Flights":
                            bool continueViewFlights = true;
                            AnsiConsole.Clear();
                            List<Flight> flights = Flight.GetFlights(); // get the list of flights without any filters
                            int selectedFlightIndex = 0;
                            isFilterActive = false;

                            while (continueViewFlights)
                            {
                                AnsiConsole.Clear();

                                // Add the flights for the current page to the options
                                var pagedFlights = flights.Skip(currentPage * itemsPerPage).Take(itemsPerPage).ToList();

                                // Create a table and add columns
                                var table = new Table()
                                    .Border(TableBorder.Rounded)
                                    .AddColumn(new TableColumn("[blue]Time[/]"))
                                    .AddColumn(new TableColumn("[blue]Origin[/]"))
                                    .AddColumn(new TableColumn("[blue]Destination[/]"))
                                    .AddColumn(new TableColumn("[blue]Flight Number[/]"))
                                    .AddColumn(new TableColumn("[blue]Gate[/]"))
                                    .AddColumn(new TableColumn("[blue]Terminal[/]"));

                                // Add the flights for the current page to the table
                                for (int i = 0; i < pagedFlights.Count; i++)
                                {
                                    var flight = pagedFlights[i];
                                    if (i == selectedFlightIndex)
                                    {
                                        table.AddRow($"[green]{flight.DepartureTime}[/]", $"[green]{flight.Origin}[/]", $"[green]{flight.Destination}[/]", $"[green]{flight.FlightNumber}[/]", $"[green]{flight.Gate}[/]", $"[green]{flight.Terminal}[/]");
                                    }
                                    else
                                    {
                                        table.AddRow(flight.DepartureTime.ToString(), flight.Origin, flight.Destination, flight.FlightNumber, flight.Gate, flight.Terminal);
                                    }
                                }

                                // Render the table
                                AnsiConsole.Render(table);

                                // Display the options
                                if (isFilterActive)
                                {
                                    AnsiConsole.MarkupLine($"[red]FILTER IS ACTIVE [/]");
                                }
                                AnsiConsole.MarkupLine("[blue]Options:[/]");
                                AnsiConsole.MarkupLine("[green]F[/]: Filter flights");
                                if (isFilterActive)
                                {
                                    AnsiConsole.MarkupLine("[green]R[/]: Remove filters");
                                }
                                AnsiConsole.MarkupLine("[green]N[/]: Next page");
                                AnsiConsole.MarkupLine("[green]P[/]: Previous page");
                                AnsiConsole.MarkupLine("[green]B[/]: Back to previous menu");

                                // Display navigation instructions
                                AnsiConsole.MarkupLine("[blue]Navigation:[/]");
                                AnsiConsole.MarkupLine("[green]Up Arrow[/]: Move selection up");
                                AnsiConsole.MarkupLine("[green]Down Arrow[/]: Move selection down");
                                AnsiConsole.MarkupLine("[green]Enter[/]: Select option");

                                var key = Console.ReadKey(true).Key;

                                switch (key)
                                {
                                    case ConsoleKey.UpArrow:
                                        if (selectedFlightIndex > 0) selectedFlightIndex--;
                                        break;
                                    case ConsoleKey.DownArrow:
                                        if (selectedFlightIndex < pagedFlights.Count - 1) selectedFlightIndex++;
                                        break;
                                    case ConsoleKey.Enter:
                                        if (selectedFlightIndex >= 0 && selectedFlightIndex < pagedFlights.Count)
                                        {
                                            Flight selectedFlight = pagedFlights[selectedFlightIndex];
                                            AnsiConsole.Clear();
                                            // Fetch the aircraft type from the selected flight
                                            string aircraftType = selectedFlight.AircraftType;

                                            // Show the layout according to the aircraft type
                                            switch (aircraftType)
                                            {
                                                case "Boeing 737":
                                                    Seat seats = new Seat();
                                                    seats.lay_out();
                                                    seats.ToonMenu(currentuser, selectedFlight.FlightId);
                                                    break;
                                                case "Boeing 787":
                                                    BoeingSeat boeing787Seats = new BoeingSeat();
                                                    boeing787Seats.lay_out();
                                                    boeing787Seats.ToonMenu(currentuser, selectedFlight.FlightId);
                                                    break;
                                                case "Airbus 330":
                                                    AirbusSeat airbus330Seats = new AirbusSeat();
                                                    airbus330Seats.lay_out();
                                                    airbus330Seats.ToonMenu(currentuser, selectedFlight.FlightId);
                                                    break;
                                                default:
                                                    AnsiConsole.WriteLine("Unknown aircraft type.");
                                                    break;
                                            }
                                        }
                                        break;
                                    case ConsoleKey.F:
                                        // Handle filter flights
                                        flights = Flight.FilterFlights();
                                        isFilterActive = true;
                                        break;
                                    case ConsoleKey.R:
                                        // Handle remove filters
                                        if (isFilterActive)
                                        {
                                            flights = Flight.GetFlights(); // get the list of flights without any filters
                                            isFilterActive = false; // reset the filter flag
                                        }
                                        break;
                                    case ConsoleKey.N:
                                        // Handle next page
                                        if (currentPage < (flights.Count / itemsPerPage)) currentPage++;
                                        break;
                                    case ConsoleKey.P:
                                        // Handle previous page
                                        if (currentPage > 0) currentPage--;
                                        break;
                                    case ConsoleKey.B:
                                        // Back to previous menu
                                        continueViewFlights = false;
                                        break;
                                }
                            }
                            break;
                        case "Manage Flights":
                            var flightMenuItems = new[] { "Add Flight", "Update Flight", "Back to previous menu" };
                            bool continueManageFlightsLoop = true;
                            while (continueManageFlightsLoop)
                            {
                                AnsiConsole.Clear();
                                var flightMenuIndex = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Manage Flights:")
                                        .PageSize(10)
                                        .AddChoices(flightMenuItems));

                                switch (flightMenuIndex)
                                {
                                    case "Add Flight":
                                        AnsiConsole.Clear();
                                        Flight.AdminAddFlight();
                                        flights = Flight.GetFlights(); // Refresh the 'flights' list
                                        options = new List<string> { filterFlightsText, backText }; // reset the options
                                        options.AddRange(flights.Select(f => f.ToString())); // add the flights to the options
                                        Console.ReadKey();
                                        break;
                                    case "Update Flight":
                                        AnsiConsole.Clear();
                                        Flight.AdminUpdateFlight();
                                        Console.ReadKey();
                                        break;
                                    case "Back to previous menu":
                                        continueManageFlightsLoop = false;
                                        break;
                                }
                            }
                            break;

                        // Download ticket data in JSON format
                        case "Download Ticket Data":
                            Console.Clear();
                            JSONconversion.Create_Json();
                            Console.WriteLine("Data has been downloaded. Check your Documents folder on your personal computer.");
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
                            break;

                        case "Flight History":
                            // Clear the console
                            AnsiConsole.Clear();

                            // Check if a user is logged in
                            if (currentuser != null)
                            {
                                // Call the method with the current user
                                Userhistory.presentuserhistory(currentuser);
                                Console.ReadKey();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]No user is currently logged in.[/]");
                                Console.ReadKey();
                            }
                            break;

                        case "Add Fly Points to user":
                            AnsiConsole.Clear();
                            Administration.AddFlyPointsToUser();
                            Console.ReadKey();
                            break;


                        case "Manage Users":
                            var userMenuItems = new[] { "Present all users", "Present all tickets from a user", "Present all tickets", "Present all users from flight", "Present user with ID", "Back to previous menu" };
                            bool continueUsersLoop = true;

                            while (continueUsersLoop)
                            {
                                AnsiConsole.Clear();
                                var userMenuIndex = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Manage Users:")
                                        .PageSize(10)
                                        .AddChoices(userMenuItems));

                                switch (userMenuIndex)
                                {
                                    case "Present all users":
                                        AnsiConsole.Clear();
                                        Administration.presentallusers();
                                        Console.ReadKey();
                                        break;
                                    case "Present all tickets from a user":
                                        AnsiConsole.Clear();
                                        Administration.presentallticketsfromuser();
                                        Console.ReadKey();
                                        break;
                                    case "Present all tickets":
                                        AnsiConsole.Clear();
                                        Administration.presentalltickets();
                                        Console.ReadKey();
                                        break;
                                    case "Present all users from flight":
                                        AnsiConsole.Clear();
                                        Administration.presentallticketsfromflight();
                                        Console.ReadKey();
                                        break;
                                    case "Present user with ID":
                                        AnsiConsole.Clear();
                                        Administration.presentuserwithID();
                                        Console.ReadKey();
                                        break;
                                    case "Back to previous menu":
                                        continueUsersLoop = false;
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
