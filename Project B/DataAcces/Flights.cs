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
}
}
