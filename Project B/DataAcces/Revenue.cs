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
        public string Destination { get; set; }
        public int FlightsCount { get; set; }
        public int TotalSeatsSold { get; set; }
        public int EconomySeatsSold { get; set; }
        public int BusinessSeatsSold { get; set; }
        public int DeluxeSeatsSold { get; set; }
        public int ExtraSpaceSeatsSold { get; set; }
        public int DuoComboSeatsSold { get; set; }
        public decimal TotalProfit { get; set; }
        private static string databasePath
        {
            get
            {
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }

        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection($"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ");
            try 
            { 
                sqlite_conn.Open(); 
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Failed to open SQLite connection: {ex.Message}");
            }
            return sqlite_conn;
        }
        public static List<Revenue> GetRevenueDetailsByDestination(DateTime startDate, DateTime endDate)
        {
            var dataList = new List<Revenue>();
            var query = @"
                SELECT 
                    Destination,
                    COUNT(DISTINCT TicketID) AS FlightsCount,
                    COUNT(TicketID) AS TotalSeatsSold,
                    SUM(CASE WHEN SeatClass = 'Economy' THEN 1 ELSE 0 END) AS EconomySeatsSold,
                    SUM(CASE WHEN SeatClass = 'Business' THEN 1 ELSE 0 END) AS BusinessSeatsSold,
                    SUM(CASE WHEN SeatClass = 'Deluxe' THEN 1 ELSE 0 END) AS DeluxeSeatsSold,
                    SUM(CASE WHEN SeatClass = 'Extra Space' THEN 1 ELSE 0 END) AS ExtraSpaceSeatsSold,
                    SUM(CASE WHEN SeatClass = 'Duo Combo' THEN 1 ELSE 0 END) AS DuoComboSeatsSold,
                    SUM(CASE WHEN SeatClass = 'Economy' THEN 100 ELSE 200 END) * COUNT(TicketID) AS TotalProfit
                FROM Tickets
                WHERE PurchaseTime BETWEEN @StartDate AND @EndDate
                GROUP BY Destination";

            try
            {
                using (var connection = CreateConnection())
                {
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.ToString("dd-MM-yyyy 00:00:00"));
                        command.Parameters.AddWithValue("@EndDate", endDate.ToString("dd-MM-yyyy 23:59:59"));

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
                                    var revenue = new Revenue
                                    {
                                        Destination = reader["Destination"].ToString(),
                                        FlightsCount = Convert.ToInt32(reader["FlightsCount"]),
                                        TotalSeatsSold = Convert.ToInt32(reader["TotalSeatsSold"]),
                                        EconomySeatsSold = Convert.ToInt32(reader["EconomySeatsSold"]),
                                        BusinessSeatsSold = Convert.ToInt32(reader["BusinessSeatsSold"]),
                                        DeluxeSeatsSold = Convert.ToInt32(reader["DeluxeSeatsSold"]),
                                        ExtraSpaceSeatsSold = Convert.ToInt32(reader["ExtraSpaceSeatsSold"]),
                                        DuoComboSeatsSold = Convert.ToInt32(reader["DuoComboSeatsSold"]),
                                        TotalProfit = Convert.ToInt32(reader["TotalProfit"])
                                    };
                                    dataList.Add(revenue);
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

    
