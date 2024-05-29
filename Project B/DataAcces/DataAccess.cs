using System;
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

        public static string databasePath
        {
           get
            {
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
         }

        static void CreateTable()
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";

            try
            {
                string sql = "CREATE TABLE Users(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email VARCHAR(255)," +
                    "Name VARCHAR(255)," +
                    "Password VARCHAR(225)," +
                    "Rank INTEGER)";
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
               "Airline VARCHAR(255)," +
               "Distance INTEGER)";
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
                "Origin VARCHAR(255)," +
                "Distance INTEGER," +
                "Extranotes VARCHAR(255)," +
                "FOREIGN KEY(FlightID) REFERENCES Flights(FlightID)" +
                "FOREIGN KEY(UserID) REFERENCES Users(ID))";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql_tickets, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                string sqlFlyPoints = "CREATE TABLE IF NOT EXISTS FlyPoints (" +
                                      "UserId INTEGER PRIMARY KEY," +
                                      "Points INTEGER)";
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sqlFlyPoints, c))
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
                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Origin, Distance, Extranotes ) VALUES('Email','{time}', 'Dirk','3', 'Business', 1, 1, '11', '{time}', 'Berlin', 'Amsterdam', 150 , '-');";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Origin, Distance, Extranotes ) VALUES('Email1','{time}', 'Berat', '2','Business', 1, 2, '11', '{time}','Berlin', 'Amsterdam',150 ,  '-');";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,  Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Origin, Distance, Extranotes ) VALUES('Email','{time}', 'Dirk', '1','Business', 1, 1, '11', '{time}', 'Berlin', 'Amsterdam', 150  , '-');";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }
                           

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID ,Gate, Departuretime, Destination, Origin, Distance, Extranotes) VALUES('Email','{time}', 'Dirk','4','Business', 1, 1, '11', '{time}', 'Berlin', 'Amsterdam',150 , '-')";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }

                           

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID ,Gate, Departuretime, Destination,Origin, Distance ,Extranotes) VALUES('Email','{time}', 'Dirk','5','Business', 1, 1, '11', '{time}', 'Berlin', 'Amsterdam', 150 , '-');";
                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID, Gate, Departuretime, Destination,Origin ,Extranotes) VALUES('Email','{time}', 'Airk','6','Business', 5, 1, '11', '{time}', 'Berlin', 'Amsterdam', '-');";

                            using (SQLiteCommand cmd1 = new SQLiteCommand(sql, c))
                            {
                                cmd1.ExecuteNonQuery();
                            }
                          

                            sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name,Seat, SeatClass,FlightID, UserID, Gate, Departuretime, Destination,Origin, Distance ,Extranotes) VALUES('Email','{time}', 'Dirk','6','Business', 1, 1, '11', '{time}', 'Berlin', 'Amsterdam', 150 , '-');";
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

        public static void ReserveSeat(string email, string name, int seat, string seatClass, int flightId, int userId, string gate, DateTime departureTime, string destination, string origin, string extranotes)
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = $"INSERT INTO Tickets(Email, PurchaseTime, Name, Seat, SeatClass, FlightID, UserID, Gate, Departuretime, Destination, Origin, Extranotes ) VALUES(@Email, @PurchaseTime, @Name, @Seat, @SeatClass, @FlightID, @UserID, @Gate, @Departuretime, @Destination, @Origin, @Extranotes)";

            DateTime time = DateTime.Now;

            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
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
                    cmd.Parameters.AddWithValue("@Origin", origin);
                    cmd.Parameters.AddWithValue("@Extranotes", extranotes);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ReadData()
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

        public void UpdateFlyPoints(int userId, int flyPoints)
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertOrUpdateQuery = "INSERT OR REPLACE INTO FlyPoints (UserId, Points) VALUES (@UserId, @Points)";
                using (var command = new SQLiteCommand(insertOrUpdateQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Points", flyPoints);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetFlyPoints(int userId)
        {
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Points FROM FlyPoints WHERE UserId = @UserId";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0;
        }
    }
}
