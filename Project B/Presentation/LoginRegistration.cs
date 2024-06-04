using Project_B.BusinessLogic;
using Project_B.DataAcces;
using System.Xml.Linq;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Project_B.Presentation
{
    public class LoginRegistrations
    {
        // checks if the user wants to log in or register 
        public static CurrentUser LoginScreen()
        {
            CurrentUser currentUser = null;

            var prompt = new SelectionPrompt<string>()
                .Title("Please choose an option:")
                .PageSize(10)
                .AddChoices(new[] { "Login", "Register", "Guest" });

            while (currentUser == null)
            {
                string response = AnsiConsole.Prompt(prompt);

                switch (response)
                {
                    case "Login":
                        currentUser = Loginscreen();
                        break;
                    case "Register":
                        currentUser = Registrationscreen();
                        break;
                    case "Guest":
                        currentUser = Guestscreen();
                        break;
                }
            }

            return currentUser;
        }

        // registration asks for user details to then send to the business logic side
        private static CurrentUser Registrationscreen()
        {
            string email = AnsiConsole.Ask<string>("Please fill in your email: ");
            string nameInput = AnsiConsole.Ask<string>("Please fill in your first name: ");
            string name = char.ToUpper(nameInput[0]) + nameInput.Substring(1).ToLower();
            string password = AnsiConsole.Prompt(new TextPrompt<string>("Please fill in your password:").Secret());

            bool successful = Registration.RegistrationLogic(email, name, password);
            if (!successful)
            {
                AnsiConsole.MarkupLine("[red]Email already exists: {0}[/]", email);
                return null;
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Successfully created user: {0} with Email: {1}[/]", name, email);
            }

            Login.LoginLogic(email, password);
            Users user = Users.Getuser(email);
            CurrentUser currentUser = new CurrentUser(user.Id, user.Email, user.Name, user.Password, user.rank, user.FlyPoints, true);
            return currentUser;
        }

        private static CurrentUser Loginscreen()
        {
            string email = AnsiConsole.Ask<string>("[blue]Email:[/] ");
            string password = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Password:[/] ").Secret());

            bool successful = Login.LoginLogic(email, password);
            if (!successful)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[red]Wrong Email or Password[/]");
                return null;
            }
            else
            {
                Users user = Users.Getuser(email);
                AnsiConsole.WriteLine($"[green]Successfully logged in user: {user.Name}[/]");
                CurrentUser currentUser = new CurrentUser(user.Id, user.Email, user.Name, user.Password, user.rank, user.FlyPoints, true);
                return currentUser;
            }
        }

        private static CurrentUser Guestscreen()
        {
            string email = AnsiConsole.Ask<string>("Please fill in your Email: ");
            CurrentUser user = Login.GuestLogin(email);
            return user;
        }
    }
}
