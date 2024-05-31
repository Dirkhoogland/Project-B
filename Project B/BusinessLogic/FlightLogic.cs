using Project_B.DataAcces;

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

        public static void Reserveseat(int flightid, int userid, string seat, string seatclass, string extranotes)
        {
            Flight.reserveseat(flightid, userid, seat, seatclass, extranotes);
        }

        public static void AddFlyPoints(int userId, int kilometers)
        {
            int flyPoints = new DataAccess().GetFlyPoints(userId);
            flyPoints += kilometers / 2000;
            DataAccess dataAccess = new DataAccess();
            dataAccess.UpdateFlyPoints(userId, flyPoints);
        }

        public static bool RedeemFlyPoints(int userId)
        {
            DataAccess dataAccess = new DataAccess();
            int flyPoints = dataAccess.GetFlyPoints(userId);

            if (flyPoints >= 20)
            {
                flyPoints -= 20;
                dataAccess.UpdateFlyPoints(userId, flyPoints);
                return true;
            }

            return false;
        }

        public static int GetFlyPoints(int userId)
        {
            DataAccess dataAccess = new DataAccess();
            return dataAccess.GetFlyPoints(userId);
        }
        public static bool CheckIfUserCanRedeemFlyPoints(int userId)
        {
            DataAccess dataAccess = new DataAccess();
            int flyPoints = dataAccess.GetFlyPoints(userId);
            return flyPoints >= 20;
        }
    }
}