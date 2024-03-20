using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Project_B.DataAcces
{
    public class Users
    {
        public int Id;
        public string Email;
        public string Name;
        public string Password;


        public Users(int Id, string Email, string Name, string Password) 
        {
            this.Id = Id;
            this.Email = Email;
            this.Name = Name;
            this.Password = Password;

        }

        // retrieves all users into a list 
        public static List<Users> Getusers()
         {
            SQLiteConnection sqlite_conn = DataAccess.CreateConnection();
            List<Users> Userslist = new List<Users>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Users";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {   // haalt het exact er uit 
                int Id = sqlite_datareader.GetInt32(0);
                string Email = sqlite_datareader.GetTextReader(1).ReadToEnd();
                string Name = sqlite_datareader.GetTextReader(2).ReadToEnd();
                string Password = sqlite_datareader.GetTextReader(3).ReadToEnd();
                Users user = new Users(Id, Email, Name, Password);
                Userslist.Add(user);
            }
            sqlite_conn.Close();
            return Userslist;
         }
        // function to send user data to the database
        public static bool Newuser(string Email, string Name, string Password)
        {
            SQLiteConnection sqlite_conn = DataAccess.CreateConnection();
            SQLiteCommand sqlite_cmd;
            try
            {

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Users(Email, Name, Password) VALUES('Email ','Dirk', 'Password'); ";
                sqlite_cmd.ExecuteNonQuery();
                sqlite_conn.Close();
                return true;
            }
            finally
            {

            }
            return false;


        }
         
         
    }
}
