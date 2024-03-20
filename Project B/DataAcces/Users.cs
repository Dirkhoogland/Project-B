using System.Data.SQLite;

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
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "SELECT * FROM Users";
            List<Users> Userslist = new List<Users>();
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                { 
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int Id = rdr.GetInt32(0);
                            string Email = rdr.GetString(1);
                            string Name = rdr.GetString(2);
                            string Password = rdr.GetString(3);
                            Users user = new Users(Id, Email, Name, Password);
                            Userslist.Add(user);
                        }
                    }
                }
            }
                
            return Userslist;
         }
        // function to send user data to the database
        public static bool Newuser(string Email, string Name, string Password)
        {
         string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
         string sql = $"INSERT INTO Users(Email, Name, Password) VALUES('{Email} ','{Name}', '{Password}'); ";
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
              c.Open();
              using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
              {
               cmd.ExecuteNonQuery(); 
              }
            }
         return true;
        }
    }
}
