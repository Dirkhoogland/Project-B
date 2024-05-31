﻿using Project_B.BusinessLogic;
using Project_B.DataAcces;

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

            while (true)
            {
                var page = userlist.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                foreach (Users user in page)
                {
                    string rank = "user";
                    if (user.rank == 1) { rank = "Admin"; }
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Rank: {rank}");
                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, (userlist.Count + pageSize - 1) / pageSize);
                Console.WriteLine("Press N for next page, P for previous page, or any other key to return.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N && pageNumber < (userlist.Count + pageSize - 1) / pageSize - 1)
                {
                    pageNumber++;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                }
                else
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

            int pageSize = 20; // Number of tickets to display at a time
            int pageNumber = 0; // Current page number

            while (true)
            {
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                foreach (Bookinghistory history in page)
                {
                    Console.WriteLine($"Flight: {history.FlightId}, Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}");
                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, (userhistory.Count + pageSize - 1) / pageSize);
                Console.WriteLine("Press N for next page, P for previous page, or any other key to return.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N && pageNumber < (userhistory.Count + pageSize - 1) / pageSize - 1)
                {
                    pageNumber++;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                }
                else
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

            int pageSize = 20; // Number of tickets to display at a time
            int pageNumber = 0; // Current page number

            while (true)
            {
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                foreach (Bookinghistory history in page)
                {
                    Console.WriteLine($"Flight: {history.FlightId}, User: {history.UserId} Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}, Notes: {history.extranotes}");
                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, (userhistory.Count + pageSize - 1) / pageSize);
                Console.WriteLine("Press N for next page, P for previous page, or any other key to return.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N && pageNumber < (userhistory.Count + pageSize - 1) / pageSize - 1)
                {
                    pageNumber++;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                }
                else
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

            while (true)
            {
                var page = userhistory.Skip(pageNumber * pageSize).Take(pageSize).ToList();

                foreach (Bookinghistory history in page)
                {
                    Console.WriteLine($"Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}, Notes: {history.extranotes}");
                }

                Console.WriteLine("\nPage {0} of {1}", pageNumber + 1, (userhistory.Count + pageSize - 1) / pageSize);
                Console.WriteLine("Press N for next page, P for previous page, or any other key to return.");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N && pageNumber < (userhistory.Count + pageSize - 1) / pageSize - 1)
                {
                    pageNumber++;
                }
                else if (key == ConsoleKey.P && pageNumber > 0)
                {
                    pageNumber--;
                }
                else
                {
                    break;
                }

                Console.Clear();
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
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Rank: {rank}");
        }

        // add Fly Points to a user
        public static void AddFlyPointsToUser()
        {
            Console.WriteLine("Enter the user ID to add Fly Points to:");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the number of kilometers flown:");
            int kilometers = Convert.ToInt32(Console.ReadLine());

            FlightLogic flightLogic = new FlightLogic();
            flightLogic.AddFlyPoints(userId, kilometers);

            Console.WriteLine("Fly Points added successfully.");
        }
    }
}
