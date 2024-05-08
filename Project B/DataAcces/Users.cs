using System.Collections.Generic;
using System.Data.SQLite;

namespace Project_B.DataAcces
{
    public class Users
    {
        public int Id;
        public string Email;
        public string Name;
        public string Password;
        public int rank;


        public Users(int Id, string Email, string Name, string Password, int rank)
        {
            this.Id = Id;
            this.Email = Email;
            this.Name = Name;
            this.Password = Password;
            this.rank = rank;
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
                            int rank = rdr.GetInt32(4);
                            Users user = new Users(Id, Email, Name, Password, rank);
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
                string sql = $"INSERT INTO Users(Email, Name, Password, Rank) VALUES('{Email} ','{Name}', '{Password}', 0); ";
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
        // gets users out of the database
        public static Users Getuser(string Email)
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = @"SELECT * FROM Users WHERE Email = $Email LIMIT 1";
            int Id = 0;
            int Rank = 0;
            string UserEmail = string.Empty;
            string Name = string.Empty;
            string Password = string.Empty;
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("$Email", Email);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Id = rdr.GetInt32(0);
                            UserEmail = rdr.GetString(1);
                            Name = rdr.GetString(2);
                            Password = rdr.GetString(3);
                            Rank = rdr.GetInt32(4);
                        }
                    }
                }
            }
            try
            {
                Users user = new Users(Id, UserEmail, Name, Password, Rank);
                return user;
            }
            catch(Exception ex) { return null; }
            

        }
        public static Users GetuserbyId(int id)
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = @"SELECT * FROM Users WHERE ID = $id LIMIT 1";
            int Id = 0;
            int Rank = 0;
            string UserEmail = string.Empty;
            string Name = string.Empty;
            string Password = string.Empty;
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("$id", id);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Id = rdr.GetInt32(0);
                            UserEmail = rdr.GetString(1);
                            Name = rdr.GetString(2);
                            Password = rdr.GetString(3);
                            Rank = rdr.GetInt32(4);
                        }
                    }
                }
            }
            try
            {
                Users user = new Users(Id, UserEmail, Name, Password, Rank);
                return user;
            }
            catch (Exception ex) { return null; }


        }// removes users from the database with email
        public static bool RemoveUser(string Email)
        {
            try
            {
                string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = @"DELETE FROM Users WHERE Email = $Email";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("$Email", Email);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch(Exception ex) { }
            return false;
        }
        // removes users from database by ID
        public static bool RemoveUser(int Id)
        {
            try
            {
                string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = @"DELETE FROM Users WHERE ID = $Id";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.Parameters.AddWithValue("$ID", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex) {  }
            return false;
        }
    }
}
