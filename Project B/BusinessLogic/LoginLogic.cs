using Project_B.DataAcces;

namespace Project_B.BusinessLogic
{
    public class Login
    {
        // This function is for checking if the user already exists, and if not, to create a new user with the given Email, name, and password.
        public static bool LoginLogic(string Email, string Password)
        {
            Users user = Users.Getuser(Email);
            if (user == null) { return false; }

            if (user.Email == Email && user.Password == Password)
            {
                return true;
            }
            else { return false; }
        }

        public static CurrentUser GuestLogin(string Email)
        {
            CurrentUser user = new CurrentUser(0, Email, "Guest", "Guest", 0, 0, true);
            return user;
        }

        public static CurrentUser LoginUser(string Email, string Password)
        {
            Users user = Users.Getuser(Email);
            if (user == null || user.Password != Password)
            {
                return null;
            }

            CurrentUser currentUser = new CurrentUser(user.Id, user.Email, user.Name, user.Password, user.rank, user.FlyPoints, true);
            return currentUser;
        }
    }
}
