using Project_B.DataAcces;
using Project_B.Presentation;
using System.Drawing.Printing;
using System.Xml.Linq;
namespace Project_B

{
    public class Project_B
    {
        static void Main()
        {
            DataAccess.CreateFlightsTable();
            Flight.CreateTestFlights();
            Flight.AdminUpdateFlight();
            Flight.FlightInformation();
            Flight.FilterFlights();
            Console.ReadLine();
        }
        // log in function to connect it with the login/registrations page. 
        private static void Login()
        {
            LoginRegistrations.LoginScreen();
        }
        
    }
}