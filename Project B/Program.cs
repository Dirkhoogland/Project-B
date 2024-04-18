using Project_B.DataAcces;
using Project_B.Presentation;
using System.Drawing.Printing;
using System.Xml.Linq;
namespace Project_B

{
    namespace Project_B
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                DataAccess.Database();
                string[] menuItemsGuest = { "Login/Register", "Exit" };
                string[] menuItemsUser = { "View Flights", "Flight History", "Logout", "Exit" };
                string[] menuItemsAdmin = { "View Flights", "Flight History", "Update Flight", "Add Flight", "Logout", "Exit" };

                string[] menuItems = menuItemsGuest; // Default to guest menu
                int currentIndex = 0;
                CurrentUser currentuser = null;

                while (true)
                {
                    Console.Clear();

                    if (currentuser != null)
                    {
                        string loginText = $"Logged in as {currentuser.Name}";
                        int padding = (Console.WindowWidth - loginText.Length) / 2;
                        Console.WriteLine(new string('-', padding) + loginText + new string('-', padding));
                    }

                    for (int i = 0; i < menuItems.Length; i++)
                    {
                        if (i == currentIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(menuItems[i]);

                        Console.ResetColor();
                    }

                    ConsoleKeyInfo keyInfo;
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                    } while (keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow && keyInfo.Key != ConsoleKey.Enter);

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (currentIndex < menuItems.Length - 1)
                        {
                            currentIndex++;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        switch (menuItems[currentIndex])
                        {
                            case "Login/Register":
                                Console.Clear();
                                currentuser = Login();
                                if (currentuser != null)
                                {
                                    if ("Admin" == "Admin")
                                    {
                                        menuItems = menuItemsAdmin;
                                    }
                                    else
                                    {
                                        menuItems = menuItemsUser;
                                    }
                                    currentIndex = 0;
                                }
                                Console.Clear();
                                break;
                            case "Logout":
                                currentuser = null;
                                menuItems = menuItemsGuest;
                                currentIndex = 0;
                                break;
                            case "Exit":
                                return;
                            case "View Flights":
                                Console.Clear();
                                FlightsPresentation flightsPresentation = new FlightsPresentation();
                                List<Flight> flights = flightsPresentation.ShowFlights();

                                int currentOption = 0;
                                string filterFlightsText = " Filter Flights ";
                                string removeFiltersText = " Remove Filters ";
                                List<string> options = new List<string> { filterFlightsText };
                                options.AddRange(flights.Select(f => f.ToString()));

                                bool isFilterActive = false;

                                while (true)
                                {
                                    Console.Clear();

                                    for (int i = 0; i < options.Count; i++)
                                    {
                                        if (i == currentOption)
                                        {
                                            Console.BackgroundColor = ConsoleColor.Gray;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                        }

                                        if (i == 0) // Filter Flights
                                        {
                                            int padding = (Console.WindowWidth - filterFlightsText.Length) / 2;
                                            Console.WriteLine(new string('-', padding) + filterFlightsText + new string('-', padding));
                                        }
                                        else if (i == 1 && isFilterActive) // Remove Filters
                                        {
                                            int padding = (Console.WindowWidth - removeFiltersText.Length) / 2;
                                            Console.WriteLine(new string('-', padding) + removeFiltersText + new string('-', padding));
                                        }
                                        else
                                        {
                                            Console.WriteLine(options[i]);
                                        }
                                        Console.ResetColor();
                                    }

                                    ConsoleKeyInfo optionKeyInfo = Console.ReadKey(true);

                                    switch (optionKeyInfo.Key)
                                    {
                                        case ConsoleKey.UpArrow:
                                            if (currentOption > 0) currentOption--;
                                            break;
                                        case ConsoleKey.DownArrow:
                                            if (currentOption < options.Count - 1) currentOption++;
                                            break;
                                        case ConsoleKey.Enter:
                                            if (currentOption == 0) // Filter Flights
                                            {
                                                flights = Flight.FilterFlights();
                                                isFilterActive = true;
                                                options = new List<string> { filterFlightsText, removeFiltersText };
                                                options.AddRange(flights.Select(f => f.ToString()));
                                                currentOption = 0;
                                            }
                                            else if (currentOption == 1 && isFilterActive) // Remove Filters
                                            {
                                                flights = Flight.GetFlights(); // prints all flights (removes filter)
                                                isFilterActive = false;
                                                options = new List<string> { filterFlightsText };
                                                options.AddRange(flights.Select(f => f.ToString()));
                                                currentOption = 0;
                                            }
                                            else // Select Flight
                                            {
                                                Flight selectedFlight = flights[currentOption - (isFilterActive ? 2 : 1)];
                                                Console.Clear();
                                                Seat seat = new Seat();
                                                seat.lay_out();
                                                seat.ToonMenu();
                                                Console.ReadLine();
                                            }
                                            break;
                                    }
                                }
                            case "Flight History":
                                Console.Clear();
                                // Call your method to view flight history
                                break;
                            case "Update Flight":
                                Console.Clear();
                                List<Flight> adminFlights = Flight.GetFlightsAdmin();
                                foreach (Flight flight in adminFlights)
                                {
                                    Console.WriteLine($"ID: {flight.FlightId}, Flight: {flight.ToString()}");
                                }
                                Flight.AdminUpdateFlight();
                                break;
                            case "Add Flight":
                                Console.Clear();
                                // Call your method to add a flight
                                break;
                        }
                    }
                }
            }
            public static Flight SelectFlight(List<Flight> flights)
            {
                int currentIndex = 0;

                while (true)
                {
                    Console.Clear();

                    for (int i = 0; i < flights.Count; i++)
                    {
                        if (i == currentIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(flights[i]);

                        Console.ResetColor();
                    }

                    ConsoleKeyInfo keyInfo;
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                    } while (keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow && keyInfo.Key != ConsoleKey.Enter);

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (currentIndex < flights.Count - 1)
                        {
                            currentIndex++;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        return flights[currentIndex];
                    }
                }
            }
            private static CurrentUser Login()
            {
                CurrentUser currentuser = null;
                return currentuser = LoginRegistrations.LoginScreen();
            }
        }
    }
    // public class Project_B
    // {
        // static void Main()
        // {
        //     DataAccess.Database();
        //     CurrentUser currentuser = Login();
        //     try
        //     {
        //         Console.WriteLine($"Name: {currentuser.Name} Logged in: {currentuser.LoggedIn}");
        //     }
        //     catch(Exception ex) { }
            // Seat seat = new Seat();
            // seat.lay_out();
            // seat.ToonMenu();
        //     //lay_out lay_out = new lay_out();
        //     //lay_out.ToonMenu();

        //     DataAccess.CreateFlightsTable();
        //     Flight.CreateTestFlights();
        //     Flight.AdminUpdateFlight();
        //     Flight.FlightInformation();
        //     Flight.FilterFlights();
        //     Console.ReadLine();

        // }
        // // log in function to connect it with the login/registrations page. 

    // }
}