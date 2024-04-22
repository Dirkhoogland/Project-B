using Project_B.DataAcces;

namespace Project_B.BusinessLogic
{
    public class Userhistorylogic
    {
        public static List<Bookinghistory> returnuserhistory(int id)
        {
            List<Bookinghistory> userhistory = Bookinghistory.GetUserHistory(id);
            return userhistory; 
        }
    }
}
