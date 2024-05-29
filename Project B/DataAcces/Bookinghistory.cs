using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Project_B.DataAcces
{
    public class Bookinghistory
    {
        public readonly int TicketId;

        public readonly string Email;

        public readonly string PurchaseTime;

        public readonly string Name;

        public readonly string Seat;

        public readonly string SeatClass;

        public readonly int FlightId;

        public readonly int UserId;

        public readonly string Gate;

        public readonly string Destination;

        public readonly string Origin;

        public readonly string extranotes;

        public readonly string Departuretime;
        // booking history constructor with all ticket information
        public Bookinghistory(int ticketid, string email, string pt, string name,string seat, string seatclass, int Fid, int Uid, string gate, string departuretime, string Origin, string destination,string extranotes)
        {
            this.TicketId = ticketid;
            this.Email = email;
            this.PurchaseTime = pt;
            this.Name = name;
            this.Seat = seat;
            this.SeatClass = seatclass;
            this.FlightId = Fid;
            this.UserId = Uid;
            this.Gate = gate;
            this.Departuretime = departuretime;
            this.Origin = Origin;
            this.Destination = destination;
            this.extranotes = extranotes;
        }
        // gets user history by their ID 
        public static List<Bookinghistory> GetUserHistory(int userid)
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "SELECT * FROM Tickets WHERE UserID = $userid";
            List<Bookinghistory> Userhistory = new List<Bookinghistory>();
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {   // opens the database connection
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("$userid", userid);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {   // this works as a for loop for each row in the database
                        while (rdr.Read())
                        {   // this part is to use the database data to put it into strings and ints to work in with c# there are premade functions for most datatypes
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
                            string Origin = rdr.GetString(11);
                            string Extranotes = rdr.GetString(12);
                            // puts it into the list to then post to the logical side
                            Bookinghistory history = new Bookinghistory(TicketId, Email, PurchaseTime, Name, Seat, SeatClass, FlightId, UserId, Gate,Departuretime, Origin, Destination, Extranotes);
                            Userhistory.Add(history);
                        }
                    }
                }
            }
            return Userhistory;
        }// this function collects all tickets made and sorts them by flight idea, so each flight should be listed with their tickets together
        public static List<Bookinghistory> GetUserHistory()
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "SELECT * FROM Tickets ORDER BY flightid, Name, Seat";
            List<Bookinghistory> Userhistory = new List<Bookinghistory>();
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {   // opens the database connection
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {   // this works as a for loop for each row in the database
                        while (rdr.Read())
                        {   // this part is to use the database data to put it into strings and ints to work in with c# there are premade functions for most datatypes
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
                            string Origin = rdr.GetString(11);
                            string Extranotes = rdr.GetString(12);
                            // puts it into the list to then post to the logical side
                            Bookinghistory history = new Bookinghistory(TicketId, Email, PurchaseTime, Name, Seat, SeatClass, FlightId, UserId, Gate, Departuretime, Origin, Destination, Extranotes);
                            Userhistory.Add(history);
                        }
                    }
                }
            }
            return Userhistory;
        }
        // gets all tickets from one specific flight not yet used
        public static List<Bookinghistory> GetflightHistorybyflightid(int flightid)
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "SELECT * FROM Tickets WHERE FlightID = $FlightId ORDER BY Name, Seat\"";
            List<Bookinghistory> Userhistory = new List<Bookinghistory>();
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {   // opens the database connection
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("$FlightId", flightid);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {   // this works as a for loop for each row in the database
                        while (rdr.Read())
                        {   // this part is to use the database data to put it into strings and ints to work in with c# there are premade functions for most datatypes
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
                            string Origin = rdr.GetString(11);
                            string Extranotes = rdr.GetString(12);
                            // puts it into the list to then post to the logical side
                            Bookinghistory history = new Bookinghistory(TicketId, Email, PurchaseTime, Name, Seat, SeatClass, FlightId, UserId, Gate, Departuretime, Origin, Destination, Extranotes);
                            Userhistory.Add(history);
                        }
                    }
                }
            }
            return Userhistory;
        }
    }
}
