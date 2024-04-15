namespace Project_B.DataAcces;

public class FlightLogic
{
    public List<Flight> GetFlights()
    {
        return Flight.GetFlights();
    }

    public void CreateFlights()
    {
        Flight.CreateFlights();
    }

    public void DeleteFlights()
    {
        Flight.DeleteRow();
    }

    public void UpdateFlight(Flight flightToUpdate)
    {
        Flight.UpdateFlight(flightToUpdate);
    }

    public Flight GetFlightById(int flightId)
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
}