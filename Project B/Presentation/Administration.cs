using Project_B.BusinessLogic;
using Project_B.DataAcces;

namespace Project_B.Presentation
{
    public class Administration
    {


        // prints all users to the console
        public static void presentallusers()
        {
            var userlist =  Adminlogic.Getusersfromdb();
            int pageSize = 20; // Number of users to display at a time
            int pageNumber = 0; // Current page number
            int totalPages = (userlist.Count + pageSize - 1) / pageSize; // Total number of pages

            while (true)
            {
                var page = userlist.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                foreach (Users user in page)
                {
                    string rank = "user";
                    if(user.rank == 1) { rank = "Admin"; }
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Rank: {rank}");
                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                Console.WriteLine("Press N for next page, P for previous page, B to go back.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N && pageNumber < totalPages - 1) // Check if there's a next page
                {
                    pageNumber++; // Go to the next page
                }
                else if (key == ConsoleKey.P && pageNumber > 0) // Check if there's a previous page
                {
                    pageNumber--; // Go to the previous page
                }
                else if (key == ConsoleKey.B)
                {
                    break;
                }

                Console.Clear();
            }
        }
        // gets all tickets from a user and the database and presents them to the screen
        public static void presentallticketsfromuser()
        {
            Console.WriteLine("Fill in user id");
            string userid = Console.ReadLine();
            int Id = Int32.Parse(userid);
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(Id);
            Console.WriteLine($"Tickets from user: {userid}");

            int pageSize = 20;
            int pageNumber = 0;
            int totalPages = (userhistory.Count + pageSize - 1) / pageSize;
            int ticketIndex = 0;

            while (true)
            {
                Console.WriteLine($"Tickets from user: {userid}");
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                for (int i = 0; i < page.Count; i++)
                {

                    
                    if (i == ticketIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Bookinghistory history = page[i];
                    Console.WriteLine($"Flight: {history.FlightId}, Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}");

                    Console.ResetColor();

                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                Console.WriteLine("Press N for next page, P for previous page, B to go back.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.DownArrow && ticketIndex < page.Count - 1)
                {
                    ticketIndex++;
                }
                else if (key == ConsoleKey.UpArrow && ticketIndex > 0)
                {
                    ticketIndex--;
                }
                else if (key == ConsoleKey.N && pageNumber < totalPages - 1)
                {
                    pageNumber++;
                    ticketIndex = 0;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                    ticketIndex = 0;
                }
                else if (key == ConsoleKey.Enter)
                {
                    int optionIndex = 0;

                    while (true)
                    {
                        Console.Clear();

                        string[] options = { "Delete ticket", "Back" };

                        for (int i = 0; i < options.Length; i++)
                        {
                            if (i == optionIndex)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine(options[i]);

                            Console.ResetColor();
                        }

                        var optionKey = Console.ReadKey(true).Key;

                        if (optionKey == ConsoleKey.DownArrow && optionIndex < options.Length - 1)
                        {
                            optionIndex++;
                        }
                        else if (optionKey == ConsoleKey.UpArrow && optionIndex > 0)
                        {
                            optionIndex--;
                        }
                        else if (optionKey == ConsoleKey.Enter)
                        {
                            if (optionIndex == 0)
                            {
                                // Delete the selected ticket
                            }
                            else if (optionIndex == 1)
                            {
                                break;
                            }
                        }
                    }
                }
                else if (key == ConsoleKey.B)
                {
                    break;
                }

                Console.Clear();
            }
        }
        // gets all tickets from the database and presents them
        public static void presentalltickets()
        {
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory();


            int pageSize = 20;
            int pageNumber = 0;
            int totalPages = (userhistory.Count + pageSize - 1) / pageSize;
            int ticketIndex = 0;


            while (true)
            {
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                for (int i = 0; i < page.Count; i++)
                {


                   
                    if (i == ticketIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Bookinghistory history = page[i];
                    Console.WriteLine($"Flight: {history.FlightId}, User: {history.UserId} Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}, Notes: {history.extranotes}");

                    Console.ResetColor();


                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                Console.WriteLine("Press N for next page, P for previous page, B to go back.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.DownArrow && ticketIndex < page.Count - 1)
                {
                    ticketIndex++;
                }
                else if (key == ConsoleKey.UpArrow && ticketIndex > 0)
                {
                    ticketIndex--;
                }
                else if (key == ConsoleKey.N && pageNumber < totalPages - 1)
                {
                    pageNumber++;
                    ticketIndex = 0;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                    ticketIndex = 0;
                }
                else if (key == ConsoleKey.Enter)
                {
                    int optionIndex = 0;

                    while (true)
                    {
                        Console.Clear();

                        string[] options = { "Delete ticket", "Back" };

                        for (int i = 0; i < options.Length; i++)
                        {
                            if (i == optionIndex)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine(options[i]);

                            Console.ResetColor();
                        }

                        var optionKey = Console.ReadKey(true).Key;

                        if (optionKey == ConsoleKey.DownArrow && optionIndex < options.Length - 1)
                        {
                            optionIndex++;
                        }
                        else if (optionKey == ConsoleKey.UpArrow && optionIndex > 0)
                        {
                            optionIndex--;
                        }
                        else if (optionKey == ConsoleKey.Enter)
                        {
                            if (optionIndex == 0)
                            {
                                // Delete the selected ticket
                            }
                            else if (optionIndex == 1)
                            {
                                break;
                            }
                        }
                    }
                }
                else if (key == ConsoleKey.B)
                {
                    break;
                }

                Console.Clear();
            }
        }
        // gets all tickets from a specific flight
        public static void presentallticketsfromflight()
        {
            Console.WriteLine("Which flight do you want the tickets of?");
            int flightid = Convert.ToInt32(Console.ReadLine());
            List<Bookinghistory> userhistory = Userhistorylogic.GetflightHistorybyflightid(flightid);
            Console.WriteLine($"Tickets from flight: {flightid}");

            int pageSize = 20; // Number of tickets to display at a time
            int pageNumber = 0; // Current page number
            int totalPages = (userhistory.Count + pageSize - 1) / pageSize; // Total number of pages
            int ticketIndex = 0; // Index of the selected ticket

            while (true)
            {
                Console.Clear();
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                for (int i = 0; i < page.Count; i++)
                {

                    
                    if (i == ticketIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Bookinghistory history = page[i];
                    Console.WriteLine($"Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}, Notes: {history.extranotes}");

                    Console.ResetColor();

                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                Console.WriteLine("Press N for next page, P for previous page, B to go back.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.DownArrow && ticketIndex < page.Count - 1)
                {
                    ticketIndex++;
                }
                else if (key == ConsoleKey.UpArrow && ticketIndex > 0)
                {
                    ticketIndex--;
                }
                else if (key == ConsoleKey.N && pageNumber < totalPages - 1)
                {
                    pageNumber++;
                    ticketIndex = 0;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                    ticketIndex = 0;
                }
                else if (key == ConsoleKey.B)
                {
                    break;
                }
                else if (key == ConsoleKey.Enter)
                {
                    int optionIndex = 0;

                    while (true)
                    {
                        Console.Clear();

                        string[] options = { "Delete ticket", "Back" };

                        for (int i = 0; i < options.Length; i++)
                        {
                            if (i == optionIndex)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine(options[i]);

                            Console.ResetColor();
                        }

                        var optionKey = Console.ReadKey(true).Key;

                        if (optionKey == ConsoleKey.DownArrow && optionIndex < options.Length - 1)
                        {
                            optionIndex++;
                        }
                        else if (optionKey == ConsoleKey.UpArrow && optionIndex > 0)
                        {
                            optionIndex--;
                        }
                        else if (optionKey == ConsoleKey.Enter)
                        {
                            if (optionIndex == 0)
                            {
                                // Delete the selected ticket
                            }
                            else if (optionIndex == 1)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        // gets the user information from one user
        public static void presentuserwithID()
        {
            Console.WriteLine("Which user do you want the information of?");
            int userid = Convert.ToInt32(Console.ReadLine());
            Users user = Adminlogic.getuserbyId(userid);
            string rank = "User";
            if (user.rank == 1) { rank = "Admin"; }
            Console.WriteLine($"Information of user: {userid}");
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Rank: {rank}  ");
        }



    }
}
