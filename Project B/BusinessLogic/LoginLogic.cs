using Project_B.DataAcces;

namespace Project_B.BusinessLogic
{
    public class Login
    {   // this function is for checking if the user already exists, and if not, to create a new user with the given Email, name and password.
        public static bool LoginLogic(string Email, string Password)
        {
            Users user = Users.Getuser(Email);
            if (user == null) { return false; }

            if ( user.Email == Email && user.Password == Password )
            {
                return true;
            }
            else { return false; }

        }

        public static CurrentUser Guestlogin(string Email)
        {
            CurrentUser user = new(0, Email, "Guest", "Guest", true);
            return user;
        }
    }
}