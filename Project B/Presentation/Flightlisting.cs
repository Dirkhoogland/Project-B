using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
