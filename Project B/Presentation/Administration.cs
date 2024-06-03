using Project_B.BusinessLogic;
using Project_B.DataAcces;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Project_B.Presentation
{
    public class Administration
    {


        // prints all users to the console
        public static void presentallusers()
        {
            var userlist = Adminlogic.Getusersfromdb();
            int pageSize = 20; // Number of users to display at a time
            int pageNumber = 0; // Current page number
            int totalPages = (userlist.Count + pageSize - 1) / pageSize; // Total number of pages

            while (true)
            {
                var page = userlist.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                // Create a table and add columns
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("ID")
                    .AddColumn("Name")
                    .AddColumn("Email")
                    .AddColumn("Rank");

                // Add rows to the table
                foreach (Users user in page)
                {
                    string rank = user.rank == 1 ? "Admin" : "User";
                    table.AddRow(user.Id.ToString(), user.Name, user.Email, rank);
                }

                // Render the table
                AnsiConsole.Render(table);

                AnsiConsole.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                AnsiConsole.WriteLine("Press N for next page, P for previous page, B to go back.");

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

                AnsiConsole.Clear();
            }
        }
        // gets all tickets from a user and the database and presents them to the screen
        public static void presentallticketsfromuser()
        {
            AnsiConsole.WriteLine("Fill in user id");
            int userid = AnsiConsole.Prompt(new TextPrompt<int>("User ID: ")
                .Validate(value => value > 0, "[red]Please enter a positive number.[/]"));

            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(userid);

            int pageSize = 20;
            int pageNumber = 0;
            int totalPages = (userhistory.Count + pageSize - 1) / pageSize;

            while (true)
            {
                AnsiConsole.WriteLine($"Tickets from user: {userid}");
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();


                // Create a table and add columns
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("Flight")
                    .AddColumn("Seat")
                    .AddColumn("Class")
                    .AddColumn("Origin")
                    .AddColumn("Destination")
                    .AddColumn("Departure Time");

                // Add rows to the table
                foreach (Bookinghistory history in page)
                {
                    table.AddRow(history.FlightId.ToString(), history.Seat, history.SeatClass, history.Origin, history.Destination, history.Departuretime.ToString());

                }

                // Render the table
                AnsiConsole.Render(table);

                AnsiConsole.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                AnsiConsole.WriteLine("Press N for next page, P for previous page, B to go back.");

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

                AnsiConsole.Clear();
            }
        }
        // gets all tickets from the database and presents them
        public static void presentalltickets()
        {
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory();


            int pageSize = 20;
            int pageNumber = 0;
            int totalPages = (userhistory.Count + pageSize - 1) / pageSize;


            while (true)
            {
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();


                // Create a table and add columns
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("Flight")
                    .AddColumn("User")
                    .AddColumn("Seat")
                    .AddColumn("Class")
                    .AddColumn("Origin")
                    .AddColumn("Destination")
                    .AddColumn("Departure Time")
                    .AddColumn("Notes");

                // Add rows to the table
                foreach (Bookinghistory history in page)
                {
                    table.AddRow(history.FlightId.ToString(), history.UserId.ToString(), history.Seat, history.SeatClass, history.Origin, history.Destination, history.Departuretime.ToString(), history.extranotes);

                }

                // Render the table
                AnsiConsole.Render(table);

                AnsiConsole.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                AnsiConsole.WriteLine("Press N for next page, P for previous page, B to go back.");

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

                AnsiConsole.Clear();
            }
        }
        // gets all tickets from a specific flight
        public static void presentallticketsfromflight()
        {
            AnsiConsole.WriteLine("Which flight do you want the tickets of?");
            int flightid = AnsiConsole.Prompt(new TextPrompt<int>("Flight ID: ")
                .Validate(value => value > 0, "[red]Please enter a positive number.[/]"));
            List<Bookinghistory> userhistory = Userhistorylogic.GetflightHistorybyflightid(flightid);

            int pageSize = 20;
            int pageNumber = 0;
            int totalPages = (userhistory.Count + pageSize - 1) / pageSize;

            while (true)
            {
                AnsiConsole.WriteLine($"Tickets from flight: {flightid}");
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();


                // Create a table and add columns
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("Seat")
                    .AddColumn("Class")
                    .AddColumn("Origin")
                    .AddColumn("Destination")
                    .AddColumn("Departure Time")
                    .AddColumn("Notes");

                // Add rows to the table
                foreach (Bookinghistory history in page)
                {
                    table.AddRow(history.Seat, history.SeatClass, history.Origin, history.Destination, history.Departuretime.ToString(), history.extranotes);

                }

                // Render the table
                AnsiConsole.Render(table);

                AnsiConsole.WriteLine("\nPage {0} of {1}", pageNumber + 1, totalPages);
                AnsiConsole.WriteLine("Press N for next page, P for previous page, B to go back.");

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

                AnsiConsole.Clear();
            }
        }
        // gets the user information from one user
        public static void presentuserwithID()
        {
            AnsiConsole.WriteLine("Which user do you want the information of?");
            int userid = AnsiConsole.Prompt(new TextPrompt<int>("User ID: ")
                .Validate(value => value > 0, "[red]Please enter a positive number.[/]"));
            Users user = Adminlogic.getuserbyId(userid);
            string rank = user.rank == 1 ? "Admin" : "User";

            // Create a table and add columns
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("User ID")
                .AddColumn("Name")
                .AddColumn("Email")
                .AddColumn("Rank");

            // Add a row to the table
            table.AddRow(userid.ToString(), user.Name, user.Email, rank);

            // Render the table
            AnsiConsole.Render(table);
        }
    }
}
