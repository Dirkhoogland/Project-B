public class Flight
{
    public DateTime DepartureTime { get; set; }
    public string Terminal { get; set; }
    public string FlightNumber { get; set; }
    public string AircraftType { get; set; }
    public int Seats { get; set; }
    public int AvailableSeats { get; set; }
    public string Destination { get; set; }
    public string Origin { get; set; }
    public string Airline { get; set; }
    public string Status { get; set; }
    public string Gate { get; set; }

    static void CreateTable(SQLiteConnection conn)
    {
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
    public bool FlightExists(string flightNumber)
    {
        using (var conn = CreateConnection())
        {
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT 1 FROM Flights WHERE FlightNumber = @FlightNumber";
            sqlite_cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);
            return sqlite_cmd.ExecuteScalar() != null;
        }
    }

    public void AddFlight(Flight flight)
    {
        if (FlightExists(flight.FlightNumber))
        {
            return;
        }

        using (var conn = CreateConnection())
        {
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Flights (FlightNumber, DepartureTime, Terminal, AircraftType, Seats) VALUES (@FlightNumber, @DepartureTime, @Terminal, @AircraftType, @Seats)";
            sqlite_cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
            sqlite_cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
            sqlite_cmd.Parameters.AddWithValue("@Terminal", flight.Terminal);
            sqlite_cmd.Parameters.AddWithValue("@AircraftType", flight.AircraftType);
            sqlite_cmd.Parameters.AddWithValue("@Seats", flight.Seats);
            sqlite_cmd.ExecuteNonQuery();
        }
    }
    public static Flight GenerateRandomFlight()
    {
        var random = new Random();
        var flight = new Flight
        {
            DepartureTime = DateTime.Now.AddDays(random.Next(1, 365)),
            Terminal = $"Terminal {random.Next(1, 6)}",
            FlightNumber = $"EU{random.Next(1000, 9999)}",
            AircraftType = "Airbus A330",
            Seats = 345
        };
        return flight;
    }

    public Flight(DateTime departureTime, string terminal)
    {
        DepartureTime = departureTime;
        Terminal = terminal;
    }
    public Flight()
    {

    }
    public static List<Flight> GetFlights()
    {
        var flights = new List<Flight>();
        using (var conn = CreateConnection())
        {
            // Read flight data from the database
        }
        return flights;
    }
    public override string ToString()
    {
        return $"{DepartureTime} {Terminal}";
    }
    
    static void Main(string[] args)
    {
        CreateTable(CreateConnection());
        var flight = GenerateRandomFlight();
        AddFlight(flight);
        var flights = GetFlights();
        foreach (var f in flights)
        {
            Console.WriteLine(f);
        }
    }
}