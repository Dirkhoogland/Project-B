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
        public string Name;
        public string Password;
        public string Email;

        public Users(int Id, string Name, string Password, string Email) 
        {
            this.Id = Id;
            this.Name = Name;
            this.Password = Password;
            this.Email = Email;
        }

         public static List<Users> Getusers()
         {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection($"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
            List<Users> userslist = new List<Users>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Users";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {   // haalt het exact er uit 
                string Id = sqlite_datareader.GetInt32(0).ToString();
                string Email = sqlite_datareader.GetTextReader(1).ReadToEnd();
                string Name = sqlite_datareader.GetTextReader(2).ReadToEnd();
                string Password = sqlite_datareader.GetTextReader(3).ReadToEnd();
            }
            return userslist;
         }
         
         
    }
}
