using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_B.BusinessLogic;
using Project_B.DataAcces;
namespace Project_B.Presentation
{
    public class Userhistory
    {
        public static void presentuserhistory(Users user)
        {
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(user.Id);
            foreach (Bookinghistory history in userhistory)
            {
                Console.WriteLine($"Flight: {history.FlightId}, Seat: {history.Seat}, Class: {history.SeatClass}, Origin: {history.Origin}, Destination: {history.Destination}");
            }
        }
    }
}
