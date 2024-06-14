using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Project_B.Presentation;
using Spectre.Console;
using Spectre.Console.Cli;
namespace Project_B.DataAcces
{
    public class LoadFromCSV
    {
        public static void LoadFlightsFromCSV(string inputConnectionString)
        {
            string databasePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "DataSource");
            string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string[] aircraftTypes = new string[] { "Boeing 787", "Boeing 737", "Airbus 330" };
            int[] seatsForTypes = new int[] { 219, 186, 345 };
            string csvFilePath;
            string projectRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string csvDirectory = Path.Combine(projectRoot, "..", "..", "..", "DataAcces", "Data");
            Random random = new Random();
            while (true)
            {
                // Ask the user for the name of the CSV file
                string csvFileName = AnsiConsole.Ask<string>("[blue]Enter the name of the CSV file to load (or type 'exit' to cancel): [/]");

                // If the user types 'exit', break the loop and return from the function
                if (csvFileName.ToLower() == "exit")
                {
                    return;
                }

                // Construct the full path to the CSV file in the specified directory
                csvFilePath = Path.Combine(csvDirectory, csvFileName);

                // Check if the file exists before trying to open it
                if (!File.Exists(csvFilePath))
                {
                    AnsiConsole.MarkupLine($"[red]File {csvFileName} not found in the specified directory.[/]");
                    Console.WriteLine($"Checked path: {csvFilePath}");
                }
                else
                {
                    break;
                }
            }

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    using (StreamReader sr = new StreamReader(csvFilePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length >= 6)
                            {
                                string sql = $@"INSERT INTO Flights (FlightNumber, Origin, Destination, DepartureTime, Terminal, AircraftType, Gate, Seats, AvailableSeats, Airline, Distance) 
                                                VALUES (@FlightNumber, @Origin, @Destination, @DepartureTime, @Terminal, @AircraftType, @Gate, @Seats, @AvailableSeats, @Airline, @Distance)";
                                cmd.CommandText = sql;
                                cmd.Parameters.AddWithValue("@FlightNumber", Regex.Replace(parts[0], "[^0-9]", ""));
                                cmd.Parameters.AddWithValue("@Origin", parts[1]);
                                cmd.Parameters.AddWithValue("@Destination", parts[2]);
                                cmd.Parameters.AddWithValue("@DepartureTime", parts[3]);
                                cmd.Parameters.AddWithValue("@Terminal", random.Next(1, 5));
                                int aircraftIndex = random.Next(0, aircraftTypes.Length);
                                cmd.Parameters.AddWithValue("@AircraftType", aircraftTypes[aircraftIndex]);
                                cmd.Parameters.AddWithValue("@Gate", random.Next(1, 25));
                                int seats = seatsForTypes[aircraftIndex];
                                cmd.Parameters.AddWithValue("@Seats", seats);
                                cmd.Parameters.AddWithValue("@AvailableSeats", seats);
                                cmd.Parameters.AddWithValue("@Airline", "New South");
                                cmd.Parameters.AddWithValue("@Distance", parts[5]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                AnsiConsole.MarkupLine("[green]Data has been loaded.[/]");
                AnsiConsole.MarkupLine("[green]Press enter to continue.[/]");
                Console.ReadLine();
            }
        }
    }
}