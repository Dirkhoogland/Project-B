using Project_B.BusinessLogic;
using Project_B.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_B.Presentation
{
    public class Administration
    {


        // prints all users to the console
        public static void presentallusers()
        {
            List<Users> userlist = Adminlogic.Getusersfromdb();
            foreach (Users user in userlist)
            { string rank = "user";
                if(user.rank == 1) { rank = "Admin"; }
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Rank: {rank}");
            }
        }

        public static void presentallticketsfromuser()
        {
            Console.WriteLine("Fill in user id");
            string userid = Console.ReadLine();
            int Id = Int32.Parse(userid);
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(Id);
            foreach (Bookinghistory history in userhistory)
            {
                Console.WriteLine($"Flight: {history.FlightId}, Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}");
            }
        }

        public static void presentalltickets()
        {
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory();
            foreach (Bookinghistory history in userhistory)
            {
                Console.WriteLine($"Flight: {history.FlightId}, User: {history.UserId} Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}");
            }
        }
        
        public static void presentallticketsfromflight()
        {
            Console.WriteLine("Which flight do you want the tickets of?");
            int flightid = Convert.ToInt32(Console.ReadLine());
            List<Bookinghistory> userhistory = Userhistorylogic.GetflightHistorybyflightid(flightid);
            foreach (Bookinghistory history in userhistory)
            {
                Console.WriteLine($"Flight: {history.FlightId}, Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}, Departure time: {history.Departuretime}");
            }
        }

        public static void presentuserwithID()
        {
            Console.WriteLine("Which user do you want the information of?");
            int userid = Convert.ToInt32(Console.ReadLine());
            Users user = Adminlogic.getuserbyId(userid);
            string rank = "User";
            if (user.rank == 1) { rank = "Admin"; }
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Rank: {rank}  ");
        }



    }
}
