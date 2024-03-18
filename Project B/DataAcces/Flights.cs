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

    public static void AddFlight(Flight flight)
    {
        if (flight.FlightExists(flight.FlightNumber))
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
        SQLiteCommand sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM Flights";
        using (SQLiteDataReader reader = sqlite_cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var flight = new Flight
                {
                    FlightNumber = reader["FlightNumber"].ToString(),
                    DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString()),
                    Terminal = reader["Terminal"].ToString(),
                    AircraftType = reader["AircraftType"].ToString(),
                    Seats = int.Parse(reader["Seats"].ToString())
                };
                flights.Add(flight);
            }
        }
    }
    return flights;
}
    public override string ToString()
    {
        return $"{DepartureTime} {Terminal}";
    }
}
}