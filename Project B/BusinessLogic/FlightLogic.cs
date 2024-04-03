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
}