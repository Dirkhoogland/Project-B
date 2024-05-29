using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Project_B.BusinessLogic;
using Project_B.DataAcces;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Project_B.Presentation
{
    public class Userhistory
    {
        public static void presentuserhistory(Users user)
        {
            // Collects all tickets from the logged in user
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(user.Id);
            userhistory = userhistory.OrderBy(history => DateTime.Parse(history.Departuretime)).ToList();
            int flightsPerPage = 10;
            int totalFlights = userhistory.Count;
            int totalPages = (totalFlights + flightsPerPage - 1) / flightsPerPage; // Round up division
            int currentPage = 1;
            int selectedHistoryIndex = 0;

            while (true)
            {
                AnsiConsole.Clear();

                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn(new TableColumn("[u]Flight ID[/]").Centered())
                    .AddColumn(new TableColumn("[u]Seat[/]").Centered())
                    .AddColumn(new TableColumn("[u]Class[/]").Centered())
                    .AddColumn(new TableColumn("[u]Origin[/]").Centered())
                    .AddColumn(new TableColumn("[u]Destination[/]").Centered())
                    .AddColumn(new TableColumn("[u]Departure Time[/]").Centered());

                int start = (currentPage - 1) * flightsPerPage;
                int end = Math.Min(start + flightsPerPage, totalFlights);

                for (int i = start; i < end; i++)
                {
                    if (i == selectedHistoryIndex)
                    {
                        table.AddRow(
                            $"[green]{userhistory[i].FlightId}[/]",
                            $"[green]{userhistory[i].Seat}[/]",
                            $"[green]{userhistory[i].SeatClass}[/]",
                            $"[green]{userhistory[i].Origin}[/]",
                            $"[green]{userhistory[i].Destination}[/]",
                            $"[green]{userhistory[i].Departuretime}[/]"
                        );
                    }
                    else
                    {
                        table.AddRow(
                            $"{userhistory[i].FlightId}",
                            $"{userhistory[i].Seat}",
                            $"{userhistory[i].SeatClass}",
                            $"{userhistory[i].Origin}",
                            $"{userhistory[i].Destination}",
                            $"{userhistory[i].Departuretime}"
                        );
                    }
                }

                AnsiConsole.Render(table);

                AnsiConsole.MarkupLine("[blue]Options:[/]");
                AnsiConsole.MarkupLine("[green]N[/]: Next page");
                AnsiConsole.MarkupLine("[green]P[/]: Previous page");
                AnsiConsole.MarkupLine("[green]B[/]: Back to previous menu");

                // Display navigation instructions
                AnsiConsole.MarkupLine("[blue]Navigation:[/]");
                AnsiConsole.MarkupLine("[green]Up Arrow[/]: Move selection up");
                AnsiConsole.MarkupLine("[green]Down Arrow[/]: Move selection down");
                AnsiConsole.MarkupLine("[green]Enter[/]: Select option");

                AnsiConsole.WriteLine($"Page {currentPage} of {totalPages}");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedHistoryIndex > start) selectedHistoryIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedHistoryIndex < end - 1) selectedHistoryIndex++;
                        break;
                    case ConsoleKey.Enter:
                        while (true)
                        {
                            AnsiConsole.Clear();

                            // Display flight information
                            AnsiConsole.MarkupLine($"[green]Flight ID:[/] {userhistory[selectedHistoryIndex].FlightId}");
                            AnsiConsole.MarkupLine($"[green]Seat:[/] {userhistory[selectedHistoryIndex].Seat}");
                            AnsiConsole.MarkupLine($"[green]Seat Class:[/] {userhistory[selectedHistoryIndex].SeatClass}");
                            AnsiConsole.MarkupLine($"[green]Origin:[/] {userhistory[selectedHistoryIndex].Origin}");
                            AnsiConsole.MarkupLine($"[green]Destination:[/] {userhistory[selectedHistoryIndex].Destination}");
                            AnsiConsole.MarkupLine($"[green]Departure Time:[/] {userhistory[selectedHistoryIndex].Departuretime}");

                            // Display menu
                            var options = new[] { "Cancel ticket", "Back to previous menu" };
                            var prompt = new SelectionPrompt<string>().AddChoices(options).PageSize(10).UseConverter(s => s);
                            string option = AnsiConsole.Prompt(prompt);

                            switch (option)
                            {
                                case "Cancel ticket":
                                    DateTime departureTime = DateTime.Parse(userhistory[selectedHistoryIndex].Departuretime);
                                    if (departureTime > DateTime.Now.AddHours(3))
                                    {
                                        // Ask for confirmation before cancelling
                                        AnsiConsole.MarkupLine("[yellow]Are you sure you want to cancel?[/]");
                                        var confirmOptions = new[] { "Yes", "No" };
                                        var confirmPrompt = new SelectionPrompt<string>().AddChoices(confirmOptions).PageSize(10).UseConverter(s => s);
                                        string confirmOption = AnsiConsole.Prompt(confirmPrompt);

                                        if (confirmOption == "Yes")
                                        {
                                            // Cancel ticket
                                            // VOEG HIER DE CODE TOE OM DE TICKET TE CANCELLEN
                                            userhistory.RemoveAt(selectedHistoryIndex);
                                            AnsiConsole.MarkupLine("[red]Ticket cancelled.[/]");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        AnsiConsole.MarkupLine("[red]Can't cancel ticket. Flight is less than 3 hours away.[/]");
                                        System.Threading.Thread.Sleep(2000);
                                        return;
                                    }
                                    break;
                                case "Back to previous menu":
                                    return;
                            }
                        }
                    case ConsoleKey.N:
                        if (currentPage < totalPages)
                        {
                            currentPage++;
                            selectedHistoryIndex = (currentPage - 1) * flightsPerPage; // Reset selected index to start of new page
                        }
                        break;
                    case ConsoleKey.P:
                        if (currentPage > 1)
                        {
                            currentPage--;
                            selectedHistoryIndex = (currentPage - 1) * flightsPerPage; // Reset selected index to start of new page
                        }
                        break;
                    case ConsoleKey.B:
                        return;
                }
            }
        }
    }
}
