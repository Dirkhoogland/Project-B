using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_B.BusinessLogic;

namespace Project_B.Presentation
{
    public class LoginRegistration
    {
        // checks if the user wants to log in or register 
        public void RegistrationorLogin()
        {   bool check = false;
            while (check is false)
            {
                Console.WriteLine("Wil je in loggen of Registreren \n Login/Register");
                string? input = Console.ReadLine();
                input.ToLower();
                if (input == "login") { Registrationscreen(); }
                if (input == "Register") { Login(); }
            }
        }
        // registration asks for user details to then send to the business logic side
        public void Registrationscreen()
        {   bool check = false;
            string? Email = null;
            string? Password = null;
            // Asks users email and password for registration, the strings are nullable so the function/responses will loop if not both filled in.
            while (check is false)
            {
                Console.WriteLine("Please fill in your Email");
                Email = Console.ReadLine();
                Console.WriteLine("Please fill in your Password");
                Password = Console.ReadLine();
                if (Email != null || Password != null) { check = true; }
            }
            bool successful = BusinessLogic.Registration.RegistrationLogic(Email, Password);
        }

        public void Login()
        {
            Console.WriteLine();
        }
    }
}
