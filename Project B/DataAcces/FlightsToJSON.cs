using System.Data;
using System.Data.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B.DataAcces
{
    public class JSONconversionFlights
    {
        public long FlightID { get; private set; }
        public string FlightNumber { get; private set; }
        public string Destination { get; private set; }
        public string Origin { get; private set; }
        public string DepartureTime { get; private set; }
        public string Terminal { get; private set; }
        public string AircraftType { get; private set; }
        public string Gate { get; private set; }
        public int Seats { get; private set; }
        public int AvailableSeats { get; private set; }
        public string Airline { get; private set; }
        public int Distance { get; private set; }

        public JSONconversionFlights(long flightID, string flightNumber, string destination, string origin, string departureTime, string terminal, string aircraftType, string gate, int seats, int availableSeats, string airline, int distance)
        {
            FlightID = flightID;
            FlightNumber = flightNumber;
            Destination = destination;
            Origin = origin;
            DepartureTime = departureTime;
            Terminal = terminal;
            AircraftType = aircraftType;
            Gate = gate;
            Seats = seats;
            AvailableSeats = availableSeats;
            Airline = airline;
            Distance = distance;
        }

        public static List<JSONconversionFlights> Convert_to_Json_Flights(string ConnectionString)
        {
            // Create list for flights to store them in a JSON file
            List<JSONconversionFlights> flights = new List<JSONconversionFlights>();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Flights";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        // Read all data from database, assign them to variables and adds them to the list
                        while (rdr.Read())
                        {
                            long flightID = rdr.GetInt64(0);
                            string flightNumber = rdr.GetString(1);
                            string destination = rdr.GetString(2);
                            string origin = rdr.GetString(3);
                            string departureTime = rdr.GetString(4);
                            string terminal = rdr.GetString(5);
                            string aircraftType = rdr.GetString(6);
                            string gate = rdr.GetString(7);
                            int seats = rdr.GetInt32(8);
                            int availableSeats = rdr.GetInt32(9);
                            string airline = rdr.GetString(10);
                            int distance = rdr.GetInt32(11);

                            JSONconversionFlights flight = new JSONconversionFlights(flightID, flightNumber, destination, origin, departureTime, terminal, aircraftType, gate, seats, availableSeats, airline, distance);
                            flights.Add(flight);
                        }
                    }
                }
                Console.WriteLine("Data retrieved.");
                return flights;
            }
        }

        // Receives all table names from database and returns a list to iterate over in json creation
        // Function not used, but can be used to implement all tables in one json file if needed
        public static List<String> GetTableNames_Flights(SQLiteConnection conn)
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
        public static void Create_Json_flights()
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; DateTimeFormat=Custom;DateTimeStringFormat=dd-MM-yyyy HH:mm:ss";
            List<JSONconversionFlights> flights = Convert_to_Json_Flights(ConnectionString);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(flights, options);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, "NewSouthFlightData.json");
            File.WriteAllText(filePath, json);
        }
    }
}