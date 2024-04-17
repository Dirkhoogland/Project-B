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
                                    if ("" == "Admin")
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
                                Flight.FlightInformation();
                                Console.ReadLine();
                                break;
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
                                // Call your method to add a flight
                                break;
                        }
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
        //     Seat seat = new Seat();
        //     seat.lay_out();
        //     seat.ToonMenu();
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