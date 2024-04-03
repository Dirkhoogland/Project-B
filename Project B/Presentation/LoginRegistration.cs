using Project_B.BusinessLogic;
using Project_B.DataAcces;
using System.Xml.Linq;

namespace Project_B.Presentation
{
    public class LoginRegistrations
    {

        // checks if the user wants to log in or register 
        public static CurrentUser LoginScreen()
        {   bool check = false;
            CurrentUser currentUser = null;
            while (check is false)
            {   
                Console.WriteLine("Wil je in loggen of Registreren \n Login/Register/Guest");
                string? inputunchecked = Console.ReadLine();
                string input = inputunchecked.ToLower();
                if (input == "login") { check = true; currentUser = Loginscreen(); }
                else if (input == "register") { check = true; Registrationscreen(); }
                else if (input == "guest") { check = true;  Guestscreen(); }
            }
            return currentUser;
        }
        // registration asks for user details to then send to the business logic side
        private static void Registrationscreen()
        {   bool check = false;
            string? Email = null;
            string? Password = null;
            string? Name = null;
            // Asks users email and password for registration, the strings are nullable so the function/responses will loop if not both filled in.
            while (check is false)
            {
                Console.WriteLine("Please fill in your Email");
                Email = Console.ReadLine();
                Console.WriteLine("Please fill in your Name");
                Name = Console.ReadLine();
                Console.WriteLine("Please fill in your Password");
                Password = Console.ReadLine();
                if (Email != null && Password != null && Name != null) { check = true; }
            }
            // the bool is to see if the user passed the already existing check, users can have the same name but not the same Email
            bool successful = Registration.RegistrationLogic(Email, Name, Password);
            if ( successful == false)
            {
                Console.WriteLine($"Email already exists: {Email}");
            }
            else
            {
                Console.WriteLine($"Successfully created user: {Name} with Email: {Email}");
            }
        }

        private static CurrentUser Loginscreen()
        {
            bool check = false;
            string? Email = null;
            string? Password = null;
            // Asks users email and password for registration, the strings are nullable so the function/responses will loop if not both filled in.
            while (check is false)
            {
                Console.WriteLine("Please fill in your Email");
                Email = Console.ReadLine();
                Console.WriteLine("Please fill in your Password");
                Password = Console.ReadLine();
                if (Email != null && Password != null) { check = true; }
            }
            // the bool is to see if the user passed the already existing check, users can have the same name but not the same Email
            bool successful = Login.LoginLogic(Email, Password);
            if (successful == false)
            {
                Console.WriteLine($"Wrong Email or Password");
                return null;
            }
            else
            {
                Console.WriteLine($"Successfully logged in user: {Email}");
                Users User = Users.Getuser(Email);
                CurrentUser currentUser = new CurrentUser(User.Id, User.Email, User.Name, User.Password, true);
                return currentUser;
            }
        }

        private static void Guestscreen()
        {

        }
    }
}
