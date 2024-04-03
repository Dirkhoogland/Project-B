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

            // DataAccess.Database();
            // Login();
            // lay_out lay_out = new lay_out();
            // lay_out.ToonMenu();
            DataAccess.CreateFlightsTable();
            Flight.CreateFlights();
            // Flight.AdminUpdateFlight();
            FlightInformation();


        }
        // log in function to connect it with the login/registrations page. 
        private static void Login()
        {
            LoginRegistrations.LoginScreen();
        }
        
        // functie om de vlucht informatie te laten zien
        private static void FlightInformation()
        {
            var flights = Flight.GetFlights();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}