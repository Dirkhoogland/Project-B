using Project_B.DataAcces;

namespace Project_B
{
    public class Project_B
    {
        static void Main()
        {
            TicketManager.AddTicket("bjarouialt@gmail.com", DateTime.Now);
            TicketManager.CancelTicket();
            //DataAccess.Database();
        }
    }
}
