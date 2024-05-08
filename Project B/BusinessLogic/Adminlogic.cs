using Project_B.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_B.BusinessLogic
{
    public class Adminlogic
    {

        public static List<Users> Getusersfromdb()
        { List<Users> users = Users.Getusers();
            return users;
        }

        public static Users getuserbyId(int id)
        {
            Users user = Users.GetuserbyId(id);
            return user;
        }
    }
}
