using Project_B.DataAcces;
using System.Collections.Generic;

namespace Project_B.BusinessLogic
{
    public class Userhistorylogic
    {
        public static List<Bookinghistory> returnuserhistory(int id)
        {
            return Bookinghistory.GetUserHistory(id);
        }

        public static List<Bookinghistory> returnuserhistory()
        {
<<<<<<< Updated upstream
            List<Bookinghistory> userhistory = Bookinghistory.GetUserHistory();
            var historyQuery = from uh in userhistory  orderby uh.FlightId group uh by uh.FlightId;
            return userhistory;
=======
            return Bookinghistory.GetUserHistory();
>>>>>>> Stashed changes
        }

        public static List<Bookinghistory> GetflightHistorybyflightid(int flightid)
        {
            return Bookinghistory.GetflightHistorybyflightid(flightid);
        }
    }
}
