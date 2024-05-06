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
                DataAccess.DropStatusColumn();
                string[] menuItemsGuest = { "Login/Register", "Exit" };
                string[] menuItemsUser = { "View Flights", "Flight History", "Logout", "Exit" };
                string[] menuItemsAdmin = { "View Flights", "Flight History", "Update Flight", "Add Flight", "Logout", "Exit" };

                string[] menuItems = menuItemsGuest; // Default to guest menu
                int currentIndex = 0;
                CurrentUser currentuser = null;
                bool isFilterActive = false;
                bool isBackSelected = false;
                int currentOption = 0;
                string filterFlightsText = " Filter Flights ";
                string removeFiltersText = " Remove Filters ";
                string backText = " Back ";
                List<string> options = new List<string> { filterFlightsText };
                List<string> adminNames = new List<string> { "Dirk", "Berat", "Mitchel", "Talha", "Badr" };

                while (true)
                {
                    Console.Clear();

                    if (currentuser == null)
                    {
                        PrintImages();
                    }
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
                                    if (adminNames.Contains(currentuser.Name))
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
                                isBackSelected = false;
                                Console.Clear();
                                List<Flight> flights = Flight.GetFlights(); // get the list of flights without any filters
                                options = new List<string> { filterFlightsText, backText }; // reset the options
                                options.AddRange(flights.Select(f => f.ToString())); // add the flights to the options

                                while (true)
                                {
                                    Console.Clear();

                                    if (isFilterActive)
                                    {
                                        options[1] = removeFiltersText;
                                    }
                                    else
                                    {
                                        options[1] = backText;
                                    }
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
                                        else if (i == 1 && !isFilterActive) // Back
                                        {
                                            int padding = (Console.WindowWidth - backText.Length) / 2;
                                            Console.WriteLine(new string('-', padding) + backText + new string('-', padding));
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
                                                flights = Flight.GetFlights(); // get the list of flights without any filters
                                                options = new List<string> { filterFlightsText, backText }; // reset the options
                                                options.AddRange(flights.Select(f => f.ToString())); // add the flights to the options
                                                isFilterActive = false; // reset the filter flag
                                                currentOption = 0; // reset the current option
                                            }
                                            else if (currentOption == 1 && !isFilterActive) // Back
                                            {
                                                menuItems = menuItemsUser;
                                                currentIndex = 0;
                                                isBackSelected = true;
                                            }
                                            else // Select Flight
                                            {
                                                if (currentOption >= 2) // Check if 'currentOption' is within the bounds of the 'flights' list
                                                {
                                                    Flight selectedFlight = flights[currentOption - 2];
                                                    Console.Clear();
                                                    Seat seat = new Seat();
                                                    seat.lay_out();
                                                    seat.ToonMenu();
                                                    Console.ReadLine();
                                                }
                                            }
                                            break;
                                    }

                                    if (isBackSelected)
                                    {
                                        break;
                                    }
                                }

                                // After the while loop, check if the user is an admin
                                if (adminNames.Contains(currentuser.Name))
                                {
                                    menuItems = menuItemsAdmin;
                                }
                                else
                                {
                                    menuItems = menuItemsUser;
                                }

                                if (isBackSelected)
                                {
                                    break; // Break out of the "View Flights" case
                                }
                                else
                                {
                                    continue; // Continue with the next iteration of the main menu loop
                                }
                            case "Flight History":
                                Console.Clear();
                                // Call your method to view flight history
                                break;
                            case "Update Flight":
                                Console.Clear();
                                Flight.AdminUpdateFlight();
                                break;
                            case "Add Flight":
                                Console.Clear();
                                Flight.AdminAddFlight();
                                flights = Flight.GetFlights(); // Refresh the 'flights' list
                                options = new List<string> { filterFlightsText, backText }; // reset the options
                                options.AddRange(flights.Select(f => f.ToString())); // add the flights to the options
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

                    Console.WriteLine(line1.PadRight(image1.Max(s => s.Length)) + "    " + line2);
                }
            }
            private static CurrentUser Login()
            {
                CurrentUser currentuser = null;
                return currentuser = LoginRegistrations.LoginScreen();
            }
        }
    }
}