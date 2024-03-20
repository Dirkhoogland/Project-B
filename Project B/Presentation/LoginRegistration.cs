using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_B.BusinessLogic;

namespace Project_B.Presentation
{
    public class LoginRegistrations
    {
        // checks if the user wants to log in or register 
        public static void LoginScreen()
        {   bool check = false;
            while (check is false)
            {
                Console.WriteLine("Wil je in loggen of Registreren \n Login/Register/Guest");
                string? input = Console.ReadLine();
                input.ToLower();
                if (input == "login") { Registrationscreen(); }
                if (input == "register") { Login(); }
                else if (input == "guest") { Guest(); }
            }
        }
        // registration asks for user details to then send to the business logic side
        public static void Registrationscreen()
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
                if (Email != null || Password != null || Name != null) { check = true; }
            }
            bool successful = Registration.RegistrationLogic(Email, Password, Name);
        }

        public static void Login()
        {
            Console.WriteLine();
        }

        public static void Guest()
        {

        }
    }
}
