using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Project_B.DataAcces
{
    public class DataAccess
    {
        // string[] args
        public static void Database()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            InsertData(sqlite_conn);
            //ReadData(sqlite_conn);
        }
        private static string databasePath
        {
            get
            {
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try { sqlite_conn.Open(); }
            catch (Exception ex) { }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {
            // creates the user table with a ID, Email And name
            try
            {
                SQLiteCommand sqlite_cmd;
                string Createsql = "CREATE TABLE Users(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email VARCHAR(255)," +
                    "Name VARCHAR(225))";
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { }
        try
        {
            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS Flights(" +
                "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                "FlightNumber VARCHAR(255)," +
                "DepartureTime DATETIME," +
                "Terminal VARCHAR(255)," +
                "AircraftType VARCHAR(255)," +
                "Seats INTEGER)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }
        catch (Exception ex) {}
        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Users";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            // inserts test data of 3 users.
            if (sqlite_datareader.Read() == false)
            {
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Users(Email, Name) VALUES('Email ','Dirk'); ";
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = "INSERT INTO Users(Email, Name) VALUES('Email1 ','Berat'); ";
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = "INSERT INTO Users(Email, Name) VALUES('Email2 ','Mitchel'); ";
                sqlite_cmd.ExecuteNonQuery();
            }

        }

        static void ReadData(SQLiteConnection conn)
        {
            // reads data of the current user table 
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Users";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {   // haalt het exact er uit 
                string Id = sqlite_datareader.GetInt32(0).ToString();
                string Email = sqlite_datareader.GetTextReader(1).ReadToEnd();
                string Name = sqlite_datareader.GetTextReader(2).ReadToEnd();
                Console.WriteLine( Id + " " +  Email + " " +  Name);

                // loopt er overheen om de data te printen
                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    var temp = sqlite_datareader.GetValue(i);
                    Console.Write(temp);
                }
                Console.WriteLine();
                // var myreader = sqlite_datareader.GetValues();
                //var test = myreader.GetKey(1);
                //var test2 = myreader.GetKey(2);
                //Console.WriteLine(test.ToString() + test2.ToString());
            }
            conn.Close();
        }
    }
}
