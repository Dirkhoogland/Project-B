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
            // CurrentUser currentuser = Login();
            // try
            // {
            //     Console.WriteLine($"Name: {currentuser.Name} Logged in: {currentuser.LoggedIn}");
            // }
            // catch(Exception ex) { }
            // Seat seat = new Seat();
            // seat.lay_out();
            // seat.ToonMenu();
            //lay_out lay_out = new lay_out();
            //lay_out.ToonMenu();
            // DataAccess.CreateFlightsTable();
            // Flight.CreateTestFlights();
            Flight.AdminUpdateFlight();
            Flight.FlightInformation();
            Flight.FilterFlights();
            Console.ReadLine();

        }
        // log in function to connect it with the login/registrations page. 
        private static CurrentUser Login()
        {
            CurrentUser currentuser = null;
            return currentuser = LoginRegistrations.LoginScreen();
        }
        
    }
}