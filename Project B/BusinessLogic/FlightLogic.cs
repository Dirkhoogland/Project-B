using Project_B.DataAcces;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Project_B.BusinessLogic
{
    public class FlightLogic
    {
        public static List<Flight> getadminflights()
        {
            List<Flight> Flights = Flight.Adminflightlist();
            return Flights;
        }
        public static List<Flight> GetFlights()
        {
            return Flight.GetFlights();
        }

        public static void CreateFlights()
        {
            DataAccess.CreateTestFlights();
        }

        public static void DeleteFlights()
        {
            Flight.DeleteRow();
        }

        public static void UpdateFlight(Flight flightToUpdate)
        {
            Flight.UpdateFlight(flightToUpdate);
        }

        public static Flight GetFlightById(int flightId)
        {
            return Flight.GetFlightById(flightId);
        }

        public List<Flight> FilterFlights(string destination, DateTime? departureDate, string airline)
        {
            List<Flight> flights = Flight.GetFlights();

            if (!string.IsNullOrEmpty(destination))
            {
                flights = flights.Where(f => f.Destination.ToLower() == destination.ToLower()).ToList();
            }

            if (departureDate.HasValue)
            {
                flights = flights.Where(f => f.DepartureTime.Date == departureDate.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(airline))
            {
                flights = flights.Where(f => f.Airline.ToLower() == airline.ToLower()).ToList();
            }

            return flights;
        }

        public static void Reserveseat(int flightid, int userid, string seat, string seatclass, string extranotes)
        {
            Flight.reserveseat(flightid, userid, seat, seatclass, extranotes);
        }

        public static int GetFlyPoints(int userId)
        {
            return Users.GetFlyPoints(userId);
        }

        public void AddFlyPoints(int userId, int kilometers)
        {
            int flyPoints = Users.GetFlyPoints(userId);
            flyPoints += kilometers / 2000; // 1 fly point per 2000 kilometers
            Users.UpdateFlyPoints(userId, flyPoints);
        }

        public static bool RedeemFlyPoints(int userId)
        {
            int flyPoints = Users.GetFlyPoints(userId);

            if (flyPoints >= 20)
            {
                flyPoints -= 20;
                Users.UpdateFlyPoints(userId, flyPoints);
                return true;
            }

            return false;
        }

        public static bool CanRedeemFlyPoints(int userId)
        {
            int flyPoints = Users.GetFlyPoints(userId);
            return flyPoints >= 20;
        }

        public static bool RefundFlyPoints(int userId)
        {
            int flyPoints = Users.GetFlyPoints(userId);

            flyPoints += 20; // Refund the 20 points

            Users.UpdateFlyPoints(userId, flyPoints);
            return true;
        }

    }
}
