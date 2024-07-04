using System.Globalization;
using Project_B.BusinessLogic;
using Project_B.DataAcces;
using Spectre.Console;
namespace Project_B.Presentation
{
    public class Flightlisting
    {
        public static void AdminUpdateFlight()
        {

            List<Flight> flights = FlightLogic.getadminflights();
            int selectedRow = 0;
            int currentPage = 0;
            int rowsPerPage = 20;

            while (true)
            {
                AnsiConsole.MarkupLine("[bold green]Update Flight[/]");
                var flightTable = new Table().Border(TableBorder.Rounded);
                flightTable.AddColumn("Time");
                flightTable.AddColumn("Origin");
                flightTable.AddColumn("Destination");
                flightTable.AddColumn("Flight Number");
                flightTable.AddColumn("Gate");
                flightTable.AddColumn("Terminal");

                int startRow = currentPage * rowsPerPage;
                int endRow = Math.Min(startRow + rowsPerPage, flights.Count);

                for (int i = startRow; i < endRow; i++)
                {
                    var flight = flights[i];
                    if (i == selectedRow)
                    {
                        flightTable.AddRow($"[green]{flight.DepartureTime}[/]", $"[green]{flight.Origin}[/]", $"[green]{flight.Destination}[/]", $"[green]{flight.FlightNumber}[/]", $"[green]{flight.Gate}[/]", $"[green]{flight.Terminal}[/]");
                    }
                    else
                    {
                        flightTable.AddRow(flight.DepartureTime.ToString(), flight.Origin, flight.Destination, flight.FlightNumber, flight.Gate, flight.Terminal);
                    }
                }

                AnsiConsole.Render(flightTable);

                // Display navigation instructions and current page number
                AnsiConsole.MarkupLine($"Page [green]{currentPage + 1}[/] of [green]{(flights.Count - 1) / rowsPerPage + 1}[/]");
                AnsiConsole.MarkupLine("[blue]Options:[/]");
                AnsiConsole.MarkupLine("[green]Up Arrow[/]: Move selection up");
                AnsiConsole.MarkupLine("[green]Down Arrow[/]: Move selection down");
                AnsiConsole.MarkupLine("[green]Enter[/]: Select option");
                AnsiConsole.MarkupLine("[blue]Navigation:[/]");
                AnsiConsole.MarkupLine("[green]N[/]: Next page");
                AnsiConsole.MarkupLine("[green]P[/]: Previous page");
                AnsiConsole.MarkupLine("[green]B[/]: Back to previous menu");

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedRow = Math.Max(0, selectedRow - 1);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedRow = Math.Min(flights.Count - 1, selectedRow + 1);
                }
                else if (key.Key == ConsoleKey.P)
                {
                    currentPage = Math.Max(0, currentPage - 1);
                    selectedRow = currentPage * rowsPerPage;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    currentPage = Math.Min((flights.Count - 1) / rowsPerPage, currentPage + 1);
                    selectedRow = currentPage * rowsPerPage;
                }
                else if (key.Key == ConsoleKey.B)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    // Handle selection
                    Flight selectedFlight = flights[selectedRow];
                    // Continue with your update logic here...
                    Flight flightToUpdate = new Flight
                    {
                        FlightId = selectedFlight.FlightId,
                        FlightNumber = selectedFlight.FlightNumber,
                        Destination = selectedFlight.Destination,
                        Origin = selectedFlight.Origin,
                        DepartureTime = selectedFlight.DepartureTime,
                        Terminal = selectedFlight.Terminal,
                        Gate = selectedFlight.Gate,
                        AircraftType = selectedFlight.AircraftType
                    };

                    AnsiConsole.Clear();

                    string[] properties = { "Flight Number", "Destination", "Origin", "Departure Time", "Terminal", "Gate" };

                    foreach (string property in properties)
                    {
                        var updatePropertyPrompt = new SelectionPrompt<string>()
                            .Title($"Do you want to update the {property}?")
                            .AddChoices(new List<string> { "Yes", "No" });

                        var updateProperty = AnsiConsole.Prompt(updatePropertyPrompt) == "Yes";

                        if (updateProperty)
                        {
                            // Update the property
                            switch (property)
                            {
                                case "Flight Number":
                                    // Update flight number
                                    while (true)
                                    {
                                        string flightNumberInput = AnsiConsole.Ask<string>("[blue]Enter new flight number (1000-9999): [/]");
                                        if (int.TryParse(flightNumberInput, out int flightNumber) && flightNumber >= 1000 && flightNumber <= 9999)
                                        {
                                            flightToUpdate.FlightNumber = flightNumberInput;
                                            break;
                                        }
                                        else
                                        {
                                            AnsiConsole.MarkupLine("[red]Invalid input. Please enter a number between 1000 and 9999.[/]");
                                        }
                                    }
                                    break;

                                case "Destination":
                                    // Update destination
                                    while (true)
                                    {
                                        string destinationInput = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter new destination: [/]")
                                            .Validate(value => value.All(char.IsLetter), "[red]Destination should only contain letters.[/]"));
                                        destinationInput = char.ToUpper(destinationInput[0]) + destinationInput.Substring(1).ToLower();
                                        flightToUpdate.Destination = destinationInput;
                                        break;
                                    }
                                    break;

                                case "Origin":
                                    // Update origin
                                    while (true)
                                    {
                                        string originInput = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter new origin: [/]")
                                            .Validate(value => value.All(char.IsLetter), "[red]Origin should only contain letters.[/]"));
                                        originInput = char.ToUpper(originInput[0]) + originInput.Substring(1).ToLower();
                                        flightToUpdate.Origin = originInput;
                                        break;
                                    }
                                    break;

                                case "Departure Time":
                                    // Update departure time
                                    while (true)
                                    {
                                        string departureTimeString = AnsiConsole.Ask<string>("[blue]Enter new departure time (dd/MM/yyyy HH:mm): [/]");
                                        if (DateTime.TryParseExact(departureTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureTime))
                                        {
                                            if (departureTime > DateTime.Now)
                                            {
                                                flightToUpdate.DepartureTime = departureTime;
                                                break;
                                            }
                                            else
                                            {
                                                AnsiConsole.MarkupLine("[red]Invalid input. The departure time should be in the future.[/]");
                                            }
                                        }
                                        else
                                        {
                                            AnsiConsole.MarkupLine("[red]Invalid input. Please enter a valid date and time in the format dd/MM/yyyy HH:mm.[/]");
                                        }
                                    }
                                    break;

                                case "Terminal":
                                    // Update terminal
                                    while (true)
                                    {
                                        string terminalInput = AnsiConsole.Ask<string>("[blue]Enter new terminal (1-4): [/]");
                                        if (int.TryParse(terminalInput, out int terminal) && terminal >= 1 && terminal <= 4)
                                        {
                                            flightToUpdate.Terminal = terminal.ToString();
                                            break;
                                        }
                                        else
                                        {
                                            AnsiConsole.MarkupLine("[red]Invalid input. Please enter a number between 1 and 4.[/]");
                                        }
                                    }
                                    break;

                                case "Gate":
                                    // Update gate
                                    while (true)
                                    {
                                        string gateInput = AnsiConsole.Ask<string>("[blue]Enter new gate (1-24): [/]");
                                        if (int.TryParse(gateInput, out int gate) && gate >= 1 && gate <= 24)
                                        {
                                            flightToUpdate.Gate = gate.ToString();
                                            break;
                                        }
                                        else
                                        {
                                            AnsiConsole.MarkupLine("[red]Invalid input. Please enter a number between 1 and 24.[/]");
                                        }
                                    }
                                    break;

                            }
                        }
                    }

                    // Display the original and updated flight information
                    Console.Clear();
                    AnsiConsole.MarkupLine($"[red]Original flight information:[/]\n{selectedFlight}\n");
                    AnsiConsole.MarkupLine($"[green]Updated flight information:[/]\n{flightToUpdate}");

                    // Ask the user if they want to save the changes
                    var saveChangesPrompt = new SelectionPrompt<string>()
                        .Title("\nDo you want to save the changes?")
                        .AddChoices(new List<string> { "Yes", "No" });

                    var saveChanges = AnsiConsole.Prompt(saveChangesPrompt) == "Yes";

                    if (saveChanges)
                    {
                        // Update the original flight with the new information
                        selectedFlight.FlightNumber = flightToUpdate.FlightNumber;
                        selectedFlight.Destination = flightToUpdate.Destination;
                        selectedFlight.Origin = flightToUpdate.Origin;
                        selectedFlight.DepartureTime = flightToUpdate.DepartureTime;
                        selectedFlight.Terminal = flightToUpdate.Terminal;
                        selectedFlight.Gate = flightToUpdate.Gate;

                        // Save the updated flight information
                        FlightLogic.UpdateFlight(selectedFlight);

                        AnsiConsole.MarkupLine("[green]Flight updated successfully![/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Changes not saved.[/]");
                    }

                    break;
                }

                AnsiConsole.Clear();
            }
        }

        public static List<Flight> FilterFlights()
        {
            List<Flight> flights = FlightLogic.GetFlights();
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold green]Filter Flights[/]");


            if (flights == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Failed to get flights.[/]");
                return new List<Flight>(); // return an empty list if there's an error
            }

            var yesNoOptions = new[] { "Yes", "No" };

            var filterByDestination = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to filter by destination?")
                    .AddChoices(yesNoOptions));

            if (filterByDestination == "Yes")
            {   
                var destinations = flights.Select(f => f.Destination).Distinct().OrderBy(d => d).ToList();
                var selectedDestination = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select a destination:")
                        .PageSize(10)
                        .AddChoices(destinations));

                flights = FlightLogic.FilterFlightsdestination(flights, selectedDestination);
            }

            var filterByDepartureTime = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to filter by departure time?")
                    .AddChoices(yesNoOptions));

            if (filterByDepartureTime == "Yes")
            {
                var departureTimes = flights.Select(f => f.DepartureTime.ToString()).Distinct().OrderBy(d => d).ToList();
                var selectedDepartureTime = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select a departure time:")
                        .PageSize(10)
                        .AddChoices(departureTimes));

                flights = FlightLogic.FilterFlightsdeparture(flights, selectedDepartureTime);
            }

            return flights;
        }

        public static void AdminAddFlight()
        {

            AnsiConsole.Clear();

            string title = "Add Flight";
            AnsiConsole.MarkupLine($"[bold green]{title}[/]");

            var flightNumber = AnsiConsole.Prompt(new TextPrompt<int>("[blue]Enter flight number: (1000-9999)[/]")
                .Validate(value => value >= 1000 && value <= 9999, "[red]Please enter a number between 1000 and 9999[/]"));

            var destination = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter destination:[/]")
                .Validate(value => !int.TryParse(value, out _), "[red]Destination cannot be a number.[/]"));
            destination = char.ToUpper(destination[0]) + destination.Substring(1).ToLower();

            var origin = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter origin:[/]")
                .Validate(value => !int.TryParse(value, out _), "[red]Origin cannot be a number.[/]"));
            origin = char.ToUpper(origin[0]) + origin.Substring(1).ToLower();

            var departureTimeString = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter departure time: (dd/MM/yyyy HH:mm)[/]")
                .Validate(value => DateTime.TryParseExact(value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) && date > DateTime.Now, "[red]Please enter a future date and time in the format dd/MM/yyyy HH:mm[/]"));

            DateTime departureTime = DateTime.ParseExact(departureTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            var terminal = AnsiConsole.Prompt(new TextPrompt<int>("[blue]Enter terminal: (1-4)[/]")
                .Validate(value => value >= 1 && value <= 4, "[red]Please enter a number between 1 and 4[/]"));

            var gate = AnsiConsole.Prompt(new TextPrompt<int>("[blue]Enter gate: (1-24)[/]")
                .Validate(value => value >= 1 && value <= 24, "[red]Please enter a number between 1 and 24[/]"));

            var aircraftTypeOptions = new[] { "Boeing 787", "Boeing 737", "Airbus 330", "Exit" };
            var aircraftType = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[blue]Enter aircraft type:[/]")
                .AddChoices(aircraftTypeOptions));
            if (aircraftType == "Exit") return;

            int seats = 0, availableSeats = 0;
            if (aircraftType == "Boeing 787")
            {
                seats = availableSeats = 219;
            }
            else if (aircraftType == "Boeing 737")
            {
                seats = availableSeats = 186;
            }
            else if (aircraftType == "Airbus 330")
            {
                seats = availableSeats = 345;
            }

            string airline = "New South";

            Flight newFlight = new Flight
            {
                FlightNumber = flightNumber.ToString(),
                Destination = destination,
                Origin = origin,
                DepartureTime = departureTime,
                Terminal = terminal.ToString(),
                AircraftType = aircraftType,
                Gate = gate.ToString(),
                Seats = seats,
                AvailableSeats = availableSeats,
                Airline = airline,
                Distance = 100
            };

            AnsiConsole.Clear();
            // Show the flight information
            AnsiConsole.MarkupLine($"[blue]Flight Number: [/][green]{newFlight.FlightNumber}[/]");
            AnsiConsole.MarkupLine($"[blue]Destination: [/][green]{newFlight.Destination}[/]");
            AnsiConsole.MarkupLine($"[blue]Origin: [/][green]{newFlight.Origin}[/]");
            AnsiConsole.MarkupLine($"[blue]Departure Time: [/][green]{newFlight.DepartureTime}[/]");
            AnsiConsole.MarkupLine($"[blue]Terminal: [/][green]{newFlight.Terminal}[/]");
            AnsiConsole.MarkupLine($"[blue]Gate: [/][green]{newFlight.Gate}[/]");
            AnsiConsole.MarkupLine($"[blue]Aircraft Type: [/][green]{newFlight.AircraftType}[/]");
            AnsiConsole.MarkupLine($"[blue]Seats: [/][green]{newFlight.Seats}[/]");
            AnsiConsole.MarkupLine($"[blue]Available Seats: [/][green]{newFlight.AvailableSeats}[/]");

            // Ask the user if they really want to add the flight
            var confirmationOptions = new[] { "Yes", "No" };
            var confirmation = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Do you really want to add this flight?")
                .AddChoices(confirmationOptions));

            if (confirmation == "Yes")
            {
                FlightLogic.createflight(newFlight);

                AnsiConsole.MarkupLine("[green]Flight added successfully![/]");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Flight not added.[/]");
                System.Threading.Thread.Sleep(3000);
            }
        }
    }
}
