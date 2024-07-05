using System;
using System.Collections.Generic;
using System.Data;
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
    class Revenue
    {

        public static List<Flight> GetRevenueDetailsByDestination(DateTime startDate, DateTime endDate)
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True;";

            var dataList = new List<Flight>();
            var query = @"
                SELECT * FROM Flights WHERE DepartureTime BETWEEN @StartDate AND @EndDate
                GROUP BY Destination";

            try
            {
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (var command = new SQLiteCommand(query, c))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No data found for the given date range.");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    Flight flight = new Flight();
                                    flight.FlightId = reader.GetInt32(reader.GetOrdinal("FlightId"));
                                    flight.Gate = reader.GetString(reader.GetOrdinal("Gate"));
                                    flight.AircraftType = reader.GetString(reader.GetOrdinal("AircraftType"));
                                    flight.Destination = reader.GetString(reader.GetOrdinal("Destination"));
                                    flight.Origin = reader.GetString(reader.GetOrdinal("Origin"));
                                    flight.DepartureTime = reader.GetDateTime(reader.GetOrdinal("DepartureTime"));
                                    flight.Distance = reader.GetInt32(reader.GetOrdinal("Distance"));
                                    flight.Revenue = reader.GetInt32(reader.GetOrdinal("Revenue"));
                                    dataList.Add(flight);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching revenue details: {ex.Message}");
            }
            return dataList;
        }
    }
}

    
