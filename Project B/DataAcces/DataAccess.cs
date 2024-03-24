using System.Data.SQLite;
namespace Project_B.DataAcces
{
    public class DataAccess
    {

        public static void Database()
        {
            CreateTable();
            InsertData();
            ReadData();
        }
        // this function gets the path to the database for use in this application
        public static string databasePath
        {
            get
            {   // gets the path to where ever its currently on your pc/laptop and then into a DataSource file, which if its correctly downloaded from github it should find.
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }
        static void CreateTable()
        {
            // creates the user table with a ID, Email Name And Password, the ID is with an Primary key and Autoincrement.
            try
            {
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = "CREATE TABLE Users(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email VARCHAR(255)," +
                    "Name VARCHAR(255)," +
                    "Password VARCHAR(225))";
                // using statements are used to confine the use of the connection to only this function, so the database remains useable outside of it since its automatially closed and does not remain open on a function when it shouldnt be
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

        public void CreateFlightsTable()
        {
            try
            {
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string[] sqlCommands = new string[]
                {
                    "CREATE TABLE IF NOT EXISTS Flights(" +
                    "FlightID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "FlightNumber VARCHAR(255)," +
                    "Destination VARCHAR(255)," +
                    "Origin VARCHAR(255)," +
                    "DepartureTime DATETIME," +
                    "Status VARCHAR(255)," +
                    "Terminal VARCHAR(255)," +
                    "AircraftType VARCHAR(255)," +
                    "Gate VARCHAR(255)," +
                    "Seats INTEGER," +
                    "AvailableSeats INTEGER," +
                    "Airline VARCHAR(255))",
                    "ALTER TABLE Flights ADD COLUMN Origin VARCHAR(255)",
                    "ALTER TABLE Flights ADD COLUMN Status VARCHAR(255)",
                    "ALTER TABLE Flights ADD COLUMN Gate VARCHAR(255)",
                    "ALTER TABLE Flights ADD COLUMN AvailableSeats INTEGER"
                };

                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    foreach (var sql in sqlCommands)
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        // the insert data function is a temple for inserting data into the sqlite DB, using the current Users database.
        // the connectionstring will need a reference to the DataAccess file if used outside of it.
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


                            // an sql query for inserting
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
        // this function reads all data from the users table, its a template to use in other functions
        static void ReadData()
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "SELECT * FROM Users"; 
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {   // opens the database connection
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {   // this works as a for loop for each row in the database
                        while (rdr.Read())
                        {   // this part is to use the database data to put it into strings and ints to work in with c# there are premade functions for most datatypes
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
