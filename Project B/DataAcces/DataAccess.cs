using System.Data.SQLite;

namespace Project_B.DataAcces
{
    public class DataAccess
    {

        public static void Database()
        {
            CreateTable();
            InsertData();
            //ReadData();
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
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            // creates the user table with a ID, Email Name And Password, the ID is with an Primary key and Autoincrement.
            try
            {
                string sql = "CREATE TABLE Users(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email VARCHAR(255)," +
                    "Name VARCHAR(255)," +
                    "Password VARCHAR(225)," +
                    "Rank INTEGER)";
                // using statements are used to confine the use of the connection to only this function, so the database remains useable outside of it since its automatially closed and does not remain open on a function when it shouldnt be
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                string sqlCommands =
               "CREATE TABLE IF NOT EXISTS Flights(" +
               "FlightID INTEGER PRIMARY KEY AUTOINCREMENT," +
               "FlightNumber VARCHAR(255)," +
               "Destination VARCHAR(255)," +
               "Origin VARCHAR(255)," +
               "DepartureTime DATETIME," +
               "Terminal VARCHAR(255)," +
               "AircraftType VARCHAR(255)," +
               "Gate VARCHAR(255)," +
               "Seats INTEGER," +
               "AvailableSeats INTEGER," +
               "Airline VARCHAR(255))";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sqlCommands, c))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
                
                string sql_tickets = "CREATE TABLE Tickets(" +
                "TicketID INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Email VARCHAR(255)," +
                "PurchaseTime DATETIME," +
                "Name VARCHAR(255)," +
                "Seat VARCHAR(255)," +
                "SeatClass VARCHAR(255)," +
                "FlightID INTEGER," +
                "UserID INTEGER," +
                "Gate VARCHAR(255)," +
                "Departuretime DATETIME," +
                "Destination VARCHAR(255)," +
                "Retour VARCHAR(255)," +
                "Origin VARCHAR(255)," +
                "Extranotes VARCHAR(255)," +
                "FOREIGN KEY(FlightID) REFERENCES Flights(FlightID)" +
                "FOREIGN KEY(UserID) REFERENCES Users(ID))";
                // using statements are used to confine the use of the connection to only this function, so the database remains useable outside of it since its automatially closed and does not remain open on a function when it shouldnt be
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql_tickets, c))
                    {
                        cmd.ExecuteNonQuery();
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
                            sql = "INSERT INTO Users(Email, Name, Password, Rank) VALUES('Email','Dirk', 'Password', 1);";

                             using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                             {
                                    cmd1.ExecuteNonQuery();
                             }
                            
                            sql = "INSERT INTO Users(Email, Name, Password, Rank) VALUES('Email1','Berat', 'Password', 1); ";


                            using (SQLiteCommand cmd2 = new SQLiteCommand(sql, c))
                            {
                                    cmd2.ExecuteNonQuery();
                            }
                            
                            sql = "INSERT INTO Users(Email, Name, Password, Rank) VALUES('Email2','Mitchel', 'Password', 0); ";
                            using (SQLiteCommand cmd3 = new SQLiteCommand(sql, c))
                            {
                                    cmd3.ExecuteNonQuery();
                            }
                            DateTime time = DateTime.Now;
                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Retour, Origin, Extranotes ) VALUES('Email','{time}', 'Dirk','3', 'Business', 1, 1, '11', '{time}', 'Berlin', 'No', 'Amsterdam', '-');";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Retour, Origin, Extranotes ) VALUES('Email1','{time}', 'Berat', '2','Business', 1, 2, '11', '{time}','Berlin', 'No', 'Amsterdam', '-');";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }
                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,  Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Retour, Origin, Extranotes ) VALUES('Email','{time}', 'Dirk', '1','Business', 1, 1, '11', '{time}', 'Berlin', 'No', 'Amsterdam', '-');";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }
                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID ,Gate, Departuretime, Destination, Retour, Origin, Extranotes) VALUES('Email','{time}', 'Dirk','4','Business', 1, 1, '11', '{time}', 'Berlin', 'No', 'Amsterdam', '-')";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }
                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID ,Gate, Departuretime, Destination, Retour, Origin ,Extranotes) VALUES('Email','{time}', 'Dirk','5','Business', 1, 1, '11', '{time}', 'Berlin', 'No', 'Amsterdam', '-');";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }
                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID, Gate, Departuretime, Destination, Retour, Origin ,Extranotes) VALUES('Email','{time}', 'Dirk','6','Business', 1, 1, '11', '{time}', 'Berlin', 'No', 'Amsterdam', '-');";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }

                            Flight.CreateFlightBoeing737();
                            CreateTestFlights();
                            CreateUsers();
                        }
                        }
                    }
                }
        }
        // this function is a template for creating users in the database, it creates 20 users with random data
        public static void CreateUsers()
        {

                Random random = new Random();
                for (int i = 0; i < 20; i++)
                {
                string name = Guid.NewGuid().ToString();
                string digits = random.Next(1000, 9999).ToString();
                string email = name + digits + "@example.com";
                string password = name + digits;
                bool check = Users.Newuser(email, name, password);
                }

        }
        public static void CreateTestFlights()
        {
            for (int i = 0; i < 20; i++)
            {
                Flight.CreateFlightBoeing737();
                Flight.CreateFlightBoeing787();
                Flight.CreateFlightAirbus330();
            }
        }
        public static string GetAircraftType(int flightId)
        {
            string aircraftType = null;
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT AircraftType FROM Flights WHERE FlightID = @FlightID", c))
                {
                    command.Parameters.AddWithValue("@FlightID", flightId);

                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        aircraftType = reader["AircraftType"].ToString();
                    }
                }
            }

            return aircraftType;
        }
        public static void AddExtraNotes(int ticketId, string extraNotes)
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
        }
        public static void ReserveSeat(string email, string name, int seat, string seatClass, int flightId, int userId, string gate, DateTime departureTime, string destination, string retourstatus, string origin, string extranotes)
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Retour, Origin, Extranotes ) VALUES(@Email, @PurchaseTime, @Name, @Seat, @SeatClass, @FlightID, @UserID, @Gate, @Departuretime, @Destination, @Retour , @Origin, @Extranotes)";

            DateTime time = DateTime.Now;

            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();

                //using (SQLiteCommand command = new SQLiteCommand("UPDATE Tickets SET ExtraNotes = @ExtraNotes WHERE TicketID = @TicketID", c))
                //{
                //    command.Parameters.AddWithValue("@ExtraNotes", extranotes);
                //    command.Parameters.AddWithValue("@TicketID", ticketId);
                //
                //    command.ExecuteNonQuery();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PurchaseTime", time);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Seat", seat);
                    cmd.Parameters.AddWithValue("@SeatClass", seatClass);
                    cmd.Parameters.AddWithValue("@FlightID", flightId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Gate", gate);
                    cmd.Parameters.AddWithValue("@Departuretime", departureTime);
                    cmd.Parameters.AddWithValue("@Destination", destination);
                    cmd.Parameters.AddWithValue("@Retour", retourstatus);
                    cmd.Parameters.AddWithValue("@Origin", origin);
                    cmd.Parameters.AddWithValue("@Extranotes", extranotes);

                    cmd.ExecuteNonQuery();
                }
                //}
            }
        }
        // this function reads all data from the users table, its a template to use in other functions
        public static void ReadData()
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
