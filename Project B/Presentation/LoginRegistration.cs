using Project_B.BusinessLogic;
using Project_B.DataAcces;
using System.Xml.Linq;

namespace Project_B.Presentation
{
    public class LoginRegistrations
    {

        // checks if the user wants to log in or register 
        public static CurrentUser LoginScreen()
        {
            bool check = false;
            CurrentUser currentUser = null;
            string[] loginOptions = { "Login", "Register", "Guest" };
            int currentIndex = 0;

            while (!check)
            {
                Console.Clear();

                for (int i = 0; i < loginOptions.Length; i++)
                {
                    if (i == currentIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(loginOptions[i]);

                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo;
                do
                {
                    keyInfo = Console.ReadKey(true);
                } while (keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow && keyInfo.Key != ConsoleKey.Enter);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (currentIndex > 0)
                    {
                        currentIndex--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (currentIndex < loginOptions.Length - 1)
                    {
                        currentIndex++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    switch (loginOptions[currentIndex])
                    {
                        case "Login":
                            check = true;
                            currentUser = Loginscreen();
                            break;
                        case "Register":
                            check = true;
                            currentUser = Registrationscreen();
                            break;
                        case "Guest":
                            check = true;
                            currentUser = Guestscreen();
                            break;
                    }
                }
            }

            return currentUser;
        }
        // registration asks for user details to then send to the business logic side
        private static CurrentUser Registrationscreen()
        {   bool check = false;
            string? Email = null;
            string? Password = null;
            string? Name = null;
            // Asks users email and password for registration, the strings are nullable so the function/responses will loop if not both filled in.
            while (check is false)
            {
                Console.WriteLine("Please fill in your email:");
                Email = Console.ReadLine().ToLower();
                Console.WriteLine("Please fill in your first name:");
                string nameInput = Console.ReadLine();
                Name = char.ToUpper(nameInput[0]) + nameInput.Substring(1).ToLower();
                Console.WriteLine("Please fill in your password:");
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
            Login.LoginLogic(Email, Password);
            Users User = Users.Getuser(Email);
            CurrentUser currentUser = new CurrentUser(User.Id, User.Email, User.Name, User.Password, 0, true);
            return currentUser;
        }

        private static CurrentUser Loginscreen()
        {
            bool check = false;
            string? Email = null;
            string? Password = null;
            // Asks users email and password for registration, the strings are nullable so the function/responses will loop if not both filled in.
            while (check is false)
            {
                Console.Clear();
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
                Users User = Users.Getuser(Email);
                Console.WriteLine($"Successfully logged in user: {User.Name}");

                CurrentUser currentUser = new CurrentUser(User.Id, User.Email, User.Name, User.Password, User.rank, true);
                return currentUser;
            }
        }

        private static CurrentUser Guestscreen()
        {
            bool check = false;
            string? Email = null;

            // Asks users email and password for registration, the strings are nullable so the function/responses will loop if not both filled in.
            while (check is false)
            {
                Console.WriteLine("Please fill in your Email");
                Email = Console.ReadLine();
                if (Email != null) { check = true; }
            }
            CurrentUser user = Login.Guestlogin(Email);
            return user;
        }
    }
}
