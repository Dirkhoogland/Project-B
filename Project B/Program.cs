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

            //DataAccess.Database();
            Login();
            Seat seat = new Seat();
            seat.lay_out();
            seat.ToonMenu();
            // Verwijder de CreateFlightsTable() als je niet Database wilt aanmaken, dan kan ook 3 eronder niet gebruikt worden. Verwijder changes als je database hebt aangemaakt ivm bugs.
            DataAccess.CreateFlightsTable();
            Flight.CreateFlights();
            Flight.AdminUpdateFlight();
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