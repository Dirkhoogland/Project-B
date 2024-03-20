using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Project_B.DataAcces
{
    public class DataAccess
    {
        // string[] args
        public static void Database()
        {
            CreateTable();
            InsertData();
            ReadData();
        }
        public static string databasePath
        {
            get
            {
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }
        static void CreateTable()
        {
            // creates the user table with a ID, Email Password And name
            try
            {
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = "CREATE TABLE Users(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email VARCHAR(255)," +
                    "Name VARCHAR(255)," +
                    "Password VARCHAR(225))";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) { }

        }

        static void InsertData()
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "SELECT * FROM Users";
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read() == false)
                        {



                            sql = "INSERT INTO Users(Email, Name, Password) VALUES('Email','Dirk', 'Password');";

                             using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                             {
                                    cmd1.ExecuteNonQuery();
                             }
                            
                            sql = "INSERT INTO Users(Email, Name, Password) VALUES('Email1','Berat', 'Password'); ";


                            using (SQLiteCommand cmd2 = new SQLiteCommand(sql, c))
                            {
                                    cmd2.ExecuteNonQuery();
                            }
                            
                            sql = "INSERT INTO Users(Email, Name, Password) VALUES('Email2','Mitchel', 'Password'); ";
                            using (SQLiteCommand cmd3 = new SQLiteCommand(sql, c))
                            {
                                    cmd3.ExecuteNonQuery();
                            }
                            }
                        }
                    }
                }
        }

        static void ReadData()
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
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
                            Console.WriteLine(Id + " "+ Email + " "+ Name + " "+ Password);
                        }

                    }
                }
            }
        }

    }
}
