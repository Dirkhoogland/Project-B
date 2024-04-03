using System;
namespace Project_B.DataAcces;

public class FlightsPresentation
{
    public void ShowFlights()
    {
        var flights = Flight.GetFlights();
        foreach (var flight in flights)
        {
            Console.WriteLine($"FlightNumber: {flight.FlightNumber}, DepartureTime: {flight.DepartureTime}, Destination: {flight.Destination}, Origin: {flight.Origin}");
        }
    }

    public void CreateFlights()
    {
        Flight.CreateFlights();
        Console.WriteLine("Flights created successfully.");
    }

    public void DeleteFlights()
    {
        Flight.DeleteRow();
        Console.WriteLine("Flights deleted successfully.");
    }
    public void UpdateFlight()
    {
        var flightToUpdate = new Flight
        {
            
        };
        Flight.UpdateFlight(flightToUpdate);
        Console.WriteLine("Flight updated successfully.");
    }

    public void GetFlightById()
    {
        var flightId = 1;
        var flight = Flight.GetFlightById(flightId);
        Console.WriteLine($"FlightNumber: {flight.FlightNumber}, DepartureTime: {flight.DepartureTime}, Destination: {flight.Destination}, Origin: {flight.Origin}");
    }
}