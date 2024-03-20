using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_B.DataAcces;

namespace Project_B.BusinessLogic
{
    public class Registration
    {
        public static bool RegistrationLogic(string Email, string Password, string Name)
        {

            List<Users> users = Users.Getusers();
            foreach (Users user in users) 
            { 
                if (user.Email == Email)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
