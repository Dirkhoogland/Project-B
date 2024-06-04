using Project_B.DataAcces;
using System.Collections.Generic;
using System.Linq;

namespace Project_B.BusinessLogic
{
    public class FlightLogic
    {
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

        public static void Reserveseat(int flightid, int userid, string seat, string seatclass, bool retourstatus, string extranotes)
        {
            Flight.reserveseat(flightid, userid, seat, seatclass, retourstatus, extranotes);
        }

        public int GetFlyPoints(int userId)
        {
            return Users.GetFlyPoints(userId);
        }

        public void AddFlyPoints(int userId, int kilometers)
        {
            int flyPoints = Users.GetFlyPoints(userId);
            flyPoints += kilometers / 2000; // Uçuş mesafesine göre puan ekleyin, 2000 km başına 1 puan
            Users.UpdateFlyPoints(userId, flyPoints);
        }

        public bool RedeemFlyPoints(int userId)
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
    }
}
