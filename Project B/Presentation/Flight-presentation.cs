using System;
namespace Project_B.DataAcces;

public class FlightsPresentation
{
    public List<Flight> ShowFlights()
    {
        var flights = Flight.GetFlights();
        foreach (var flight in flights)
        {
            Console.WriteLine($"FlightNumber: {flight.FlightNumber}, DepartureTime: {flight.DepartureTime}, Destination: {flight.Destination}, Origin: {flight.Origin}");
        }
        return flights;
    }

    public void CreateFlights()
    {
        DataAccess.CreateTestFlights();
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
    
    public void FilterFlights()
    {
        var flightLogic = new FlightLogic();

        Console.WriteLine("Do you want to filter by destination? (yes/no)");
        string destination = null;
        if (Console.ReadLine().ToLower() == "yes")
        {
            Console.WriteLine("Enter the destination you want to filter on: ");
            destination = Console.ReadLine().ToLower();
        }

        Console.WriteLine("Do you want to filter by departure time? (yes/no)");
        DateTime? departureDate = null;
        if (Console.ReadLine().ToLower() == "yes")
        {
            Console.WriteLine("Enter the departure date you want to filter on (yyyy-MM-dd): ");
            DateTime temp;
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out temp))
            {
                departureDate = temp;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }

        Console.WriteLine("Do you want to filter by airline? (yes/no)");
        string airline = null;
        if (Console.ReadLine().ToLower() == "yes")
        {
            Console.WriteLine("Enter the airline you want to filter on: ");
            airline = Console.ReadLine();
        }

        var flights = flightLogic.FilterFlights(destination, departureDate, airline);

        if (flights.Count == 0)
        {
            Console.WriteLine("No flights found with the given filters.");
        }
        else
        {
            foreach (Flight flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}