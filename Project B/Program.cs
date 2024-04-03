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

            DataAccess.Database();
            CurrentUser currentuser = Login();
            Console.WriteLine($"Name: {currentuser.Name} Logged in: {currentuser.LoggedIn}");
            //lay_out lay_out = new lay_out();
            //lay_out.ToonMenu();
            // Verwijder de CreateFlightsTable() als je niet Database wilt aanmaken, dan kan ook 3 eronder niet gebruikt worden. Verwijder changes als je database hebt aangemaakt ivm bugs.
            //DataAccess.CreateFlightsTable();
            //Flight.CreateFlights();
            //Flight.AdminUpdateFlight();
            //FlightInformation();


        }
        // log in function to connect it with the login/registrations page. 
        private static CurrentUser Login()
        {
            CurrentUser currentuser = null;
            return currentuser = LoginRegistrations.LoginScreen();
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