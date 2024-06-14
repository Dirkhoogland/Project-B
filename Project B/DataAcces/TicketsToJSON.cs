using System.Data;
using System.Data.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B.DataAcces
{
    public class JSONconversion
    {
        public int TicketId {get; private set;}

        public string Email {get; private set;}

        public string PurchaseTime {get; private set;}

        public string Name {get; private set;}

        public string Seat {get; private set;}

        public string SeatClass {get; private set;}

        public int FlightId {get; private set;}

        public int UserId {get; private set;}

        public string Gate {get; private set;}

        public string Destination {get; private set;}

        public string Origin {get; private set;}

        public string extranotes {get; private set;}

        public string Departuretime {get; private set;}

        public int Retour {get; private set;}

        public int Distance {get; private set;}

        public JSONconversion(int ticketId, string email, string purchaseTime, string name, string seat, string seatClass, int flightId, int userId, string gate, string departureTime, string destination, int retour, string origin, int distance, string extraNotes)
        {
            TicketId = ticketId;
            Email = email;
            PurchaseTime = purchaseTime;
            Name = name;
            Seat = seat;
            SeatClass = seatClass;
            FlightId = flightId;
            UserId = userId;
            Gate = gate;
            Departuretime = departureTime;
            Destination = destination;
            Retour = retour;
            Origin = origin;
            Distance = distance;
            extranotes = extraNotes;
        }

        public static List<JSONconversion> Convert_to_Json(string ConnectionString)
        {
            // Create list for tickets to store them in a JSON file
            List<JSONconversion> tickets = new List<JSONconversion>();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Tickets";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        // Read all data from database, assign them to variables and adds them to the list
                        while (rdr.Read())
                        {
                            int TicketId = rdr.GetInt32(0);
                            string Email = rdr.GetString(1);
                            string PurchaseTime = rdr.GetString(2);
                            string Name = rdr.GetString(3);
                            string Seat = rdr.GetString(4);
                            string SeatClass = rdr.GetString(5);
                            int FlightId = rdr.GetInt32(6);
                            int UserId = rdr.GetInt32(7);
                            string Gate = rdr.GetString(8);
                            string Departuretime = rdr.GetString(9);
                            string Destination = rdr.GetString(10);
                            int Retour = rdr.GetInt32(11);
                            string Origin = rdr.GetString(12);
                            int Distance = rdr.GetInt32(13);
                            string Extranotes = rdr.GetString(14);

                            JSONconversion history = new JSONconversion(TicketId, Email, PurchaseTime, Name, Seat, SeatClass, FlightId, UserId, Gate, Departuretime, Destination, Retour, Origin, Distance, Extranotes);
                            tickets.Add(history);
                        }
                    }
                }
                Console.WriteLine("Data retrieved.");
                return tickets;
            }
        }

        // Receives all table names from database and returns a list to iterate over in json creation
        // Function not used, but can be used to implement all tables in one json file if needed
        public static List<String> GetTableNames(SQLiteConnection conn)
        {
            List<string> tableNames = new List<string>();
            DataTable tables = conn.GetSchema("tables");

            foreach (DataRow row in tables.Rows)
            {
                tableNames.Add(row["TABLE_NAME"].ToString());
            }

            return tableNames;
        }

        // Creates a JSON file from the list of tickets
        public static void Create_Json()
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; DateTimeFormat=Custom;DateTimeStringFormat=dd-MM-yyyy HH:mm:ss";
            List<JSONconversion> tickets = Convert_to_Json(ConnectionString);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(tickets, options);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, "NewSouthTicketData.json");
            File.WriteAllText(filePath, json);
        }
    }
}