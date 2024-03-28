using Project_B.DataAcces;

namespace Project_B
{
    public class Project_B
    {
        static void Main()
        {
            TicketManager.AddTicket("TestMail", "TestName", DateTime.Now);
            DataAccess.Database();
        }
    }
}