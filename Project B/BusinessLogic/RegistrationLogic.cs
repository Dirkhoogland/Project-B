using Project_B.DataAcces;

namespace Project_B.BusinessLogic
{
    public class Registration
    {   // this function is for checking if the user already exists, and if not, to create a new user with the given Email, name and password.
        public static bool RegistrationLogic(string Email, string Name, string Password)
        {

            List<Users> users = Users.Getusers();
            foreach(Users user in users) 
            {   
                if(user.Email == Email)
                {
                    return false;
                }
            }
            Users.Newuser(Email, Name, Password);
            return true;
        }
    }
}
