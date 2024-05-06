using Project_B.DataAcces;

namespace Project_B.BusinessLogic
{
    public class Userhistorylogic
    {
        // returns a list of user history
        public static List<Bookinghistory> returnuserhistory(int id)
        {
            List<Bookinghistory> userhistory = Bookinghistory.GetUserHistory(id);
            return userhistory; 
        }
    }
}
