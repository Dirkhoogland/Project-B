// DONT TOUCH...


// using Project_B.BusinessLogic;
// using Project_B.DataAcces;
// using Spectre.Console;
// using Spectre.Console.Cli;

// namespace Project_B.Presentation
// {
//     public class Seat
//     {
//         public string Class { get; set; }
//         public decimal Price { get; set; }
//         public bool IsReserved { get; set; }
    
//         static Seat[,] seats = new Seat[33, 6]; 
//         public void lay_out()
//         {
//             for (int i = 0; i < 33; i++)
//             {
//                 for (int j = 0; j < 6; j++)
//                 {
//                     if (j == 0 || j == 5) 
//                     {
//                         seats[i, j] = new Seat { Class = "Economy", Price = 125m, IsReserved = false };
//                     }
//                     else
//                     {
//                         seats[i, j] = new Seat { Class = "Economy", Price = 100m, IsReserved = false };
//                     }
//                     if (i == 15 || i == 16) 
//                     {
//                         seats[i, j] = new Seat { Class = "Business", Price = 200m, IsReserved = false };
//                     }
//                 }
//             }
//         }

//         public void DisplaySeats()
//         {
//             string[,] seats = new string[34, 7]
//             {
//                 {"", "A", "B", "C", "D", "E", "F"},
//                 {"1", "0", "0", "0", "0", "0", "0"},
//                 {"2", "0", "0", "0", "0", "0", "0"},
//                 {"3", "0", "0", "0", "0", "0", "0"},
//                 {"4", "0", "0", "0", "0", "0", "0"},
//                 {"5", "0", "0", "0", "0", "0", "0"},
//                 {"6", "0", "0", "0", "0", "0", "0"},
//                 {"7", "0", "0", "0", "0", "0", "0"},
//                 {"8", "0", "0", "0", "0", "0", "0"},
//                 {"9", "0", "0", "0", "0", "0", "0"},
//                 {"10", "0", "0", "0", "0", "0", "0"},
//                 {"11", "0", "0", "0", "0", "0", "0"},
//                 {"12", "0", "0", "0", "0", "0", "0"},
//                 {"13", "0", "0", "0", "0", "0", "0"},
//                 {"14", "0", "0", "0", "0", "0", "0"},
//                 {"15", "0", "0", "0", "0", "0", "0"},
//                 {"16", "0", "0", "0", "0", "0", "0"},
//                 {"17", "0", "0", "0", "0", "0", "0"},
//                 {"18", "0", "0", "0", "0", "0", "0"},
//                 {"19", "0", "0", "0", "0", "0", "0"},
//                 {"20", "0", "0", "0", "0", "0", "0"},
//                 {"21", "0", "0", "0", "0", "0", "0"},
//                 {"22", "0", "0", "0", "0", "0", "0"},
//                 {"23", "0", "0", "0", "0", "0", "0"},
//                 {"24", "0", "0", "0", "0", "0", "0"},
//                 {"25", "0", "0", "0", "0", "0", "0"},
//                 {"26", "0", "0", "0", "0", "0", "0"},
//                 {"27", "0", "0", "0", "0", "0", "0"},
//                 {"28", "0", "0", "0", "0", "0", "0"},
//                 {"29", "0", "0", "0", "0", "0", "0"},
//                 {"30", "0", "0", "0", "0", "0", "0"},
//                 {"31", "0", "0", "0", "0", "0", "0"},
//                 {"32", "0", "0", "0", "0", "0", "0"},
//                 {"33", "0", "0", "0", "0", "0", "0"}
//             };

//             List<(int, int)> selectedSeats = new List<(int, int)>();
//             int currentRow = 1;
//             int currentColumn = 1;

//             while (true)
//             {
//                 AnsiConsole.Clear();

//                 // Display information at the top
//                 AnsiConsole.Write(new Rule("[yellow]Airplane Seating Plan[/]"));
//                 AnsiConsole.MarkupLine("[blue]You are currently viewing the seating layout of the airplane. Seats marked with 'X' are selected.[/]");
//                 AnsiConsole.WriteLine();

//                 for (int i = 0; i < seats.GetLength(0); i++)
//                 {
//                     for (int j = 0; j < seats.GetLength(1); j++)
//                     {
//                         // Add extra spaces before the seat letters
//                         string seat = seats[i, j].ToString();
//                         if (i == 0 && j > 0)
//                         {
//                             seat = "" + seat;
//                             if (j == 1) // Add an additional space before 'A'
//                             {
//                                 seat = "  " + seat;
//                             }
//                         }

//                         // Add a space before the row number for the first 9 rows
//                         if (i > 0 && i < 10 && j == 0)
//                         {
//                             seat = " " + seat;
//                         }

//                         // Add a space to simulate the hallway between 'C' and 'D'
//                         if (j == 4)
//                         {
//                             seat = " " + seat;
//                         }

//                         // Change the color of the seats
//                         if (i == 16 || i == 17)
//                         {
//                             seat = "[yellow]" + seat + "[/]";
//                         }

//                         // Mark selected seats with an 'X'
//                         if (selectedSeats.Contains((i, j)))
//                         {
//                             seat = "[green]X[/]";
//                         }

//                         if (i == currentRow && j == currentColumn)
//                         {
//                             AnsiConsole.Markup("[[" + seat + "]]");
//                         }
//                         else
//                         {
//                             AnsiConsole.Markup(" " + seat + " ");
//                         }
//                     }
//                     AnsiConsole.WriteLine();
//                 }

//                 // Display navigation instructions
//                 AnsiConsole.MarkupLine("[blue]Legend:[/]");
//                 AnsiConsole.MarkupLine("Economy Class");
//                 AnsiConsole.MarkupLine("[yellow]Business Class[/]");
//                 AnsiConsole.MarkupLine("[green]Selected seats[/]\n");
//                 AnsiConsole.MarkupLine("[blue]Navigation:[/]");
//                 AnsiConsole.MarkupLine("[green]Up Arrow[/]: Move selection up");
//                 AnsiConsole.MarkupLine("[green]Down Arrow[/]: Move selection down");
//                 AnsiConsole.MarkupLine("[green]Right Arrow[/]: Move selection right");
//                 AnsiConsole.MarkupLine("[green]Left Arrow[/]: Move selection left");
//                 AnsiConsole.MarkupLine("[green]Enter[/]: Select option");
//                 AnsiConsole.MarkupLine("[green]P[/]: Purchase selected seats");
//                 AnsiConsole.MarkupLine("[green]B[/]: Go back to previous menu");

//                 ConsoleKeyInfo key = Console.ReadKey();

//                 switch (key.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                         if (currentRow > 1) currentRow--;
//                         break;
//                     case ConsoleKey.DownArrow:
//                         if (currentRow < seats.GetLength(0) - 1) currentRow++;
//                         break;
//                     case ConsoleKey.LeftArrow:
//                         if (currentColumn > 1) currentColumn--;
//                         break;
//                     case ConsoleKey.RightArrow:
//                         if (currentColumn < seats.GetLength(1) - 1) currentColumn++;
//                         break;
//                     case ConsoleKey.Enter:
//                         // Add the selected seat to the list
//                         if (!selectedSeats.Contains((currentRow, currentColumn)))
//                         {
//                             selectedSeats.Add((currentRow, currentColumn));
//                         }
//                         break;
//                 }
//             }
//         }

//         public void ToonMenu(CurrentUser current, int flightid)
//         {
//             DisplaySeats();
//             int currentOption = 0;
//             string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart" };

//             ConsoleKeyInfo key;
//             Console.Clear();

//             FlightLogic flightLogic = new FlightLogic();

//             do
//             {
//                 Console.SetCursorPosition(0, 0);
//                 Console.WriteLine("Welcome to the seat reservation system!");
//                 Console.WriteLine("Available options:");

//                 for (int i = 0; i < menuOptions.Length; i++)
//                 {
//                     if (i == currentOption)
//                     {
//                         Console.BackgroundColor = ConsoleColor.Gray;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }

//                     Console.WriteLine($"{i + 1}. {menuOptions[i]}");

//                     Console.ResetColor();
//                 }

//                 key = Console.ReadKey(true);
//                 switch (key.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                         currentOption = Math.Max(0, currentOption - 1);
//                         break;
//                     case ConsoleKey.DownArrow:
//                         currentOption = Math.Min(menuOptions.Length - 1, currentOption + 1);
//                         break;
//                 }
//             } while (key.Key != ConsoleKey.Enter);

//             switch (currentOption)
//             {
//                 case 0:
//                     ChooseSeatWithArrowKeys(current, flightid);
//                     break;
//                 case 1:
//                     DisplaySeatLayoutBoeing737();
//                     break;
//                 case 2:
//                     Console.WriteLine("Thank you for using the seat reservation system. Bye!");
//                     return;
//                 case 3:
//                     int points = flightLogic.GetFlyPoints(current.Id);
//                     Console.WriteLine($"Current Fly points balance: {points}");
//                     break;
//                 case 4:
//                     if (flightLogic.RedeemFlyPoints(current.Id))
//                     {
//                         Console.WriteLine("20 Fly points redeemed for a 10% discount on your next booking.");
//                     }
//                     else
//                     {
//                         Console.WriteLine("Not enough Fly points to redeem. Minimum 20 points required.");
//                     }
//                     int newPoints = flightLogic.GetFlyPoints(current.Id);
//                     Console.WriteLine($"New Fly points balance: {newPoints}");
//                     break;
//                 default:
//                     Console.WriteLine("Invalid choice");
//                     break;
//             }

//             Console.WriteLine();
//         }

//         public void ChooseSeatWithArrowKeys(CurrentUser current, int flightid)
//         {
//             Console.Clear();
//             int row = 0;
//             int seat = 0;

//             ConsoleKeyInfo key;

//             do
//             {
//                 Console.SetCursorPosition(0,0);
//                 DisplaySeatLayoutBoeing737(row, seat);

//                 key = Console.ReadKey(true);

//                 switch (key.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                         row = Math.Max(0, row - 1);
//                         break;
//                     case ConsoleKey.DownArrow:
//                         row = Math.Min(32, row + 1);
//                         break;
//                     case ConsoleKey.LeftArrow:
//                         seat = Math.Max(0, seat - 1);
//                         break;
//                     case ConsoleKey.RightArrow:
//                         seat = Math.Min(5, seat + 1);
//                         break;
//                     case ConsoleKey.Enter:
//                         ReserveSeat(row, seat, current, flightid);
//                         return;
//                 }
//             } while (key.Key != ConsoleKey.Escape);
//         }

//         public void ReserveSeat(int row, int seat, CurrentUser current, int flightid)
//         {
//             Seat chosenSeat = seats[row, seat];
//             if (chosenSeat.IsReserved)
//             {
//                 Console.WriteLine("This seat has already been reserved. Choose another seat.");
//                 return;
//             }

//             Console.WriteLine($"You have chosen this seat: {row + 1}{(char)(seat + 'A')}. Class: {chosenSeat.Class}, Price: {chosenSeat.Price}");

//             int currentOption = 0;
//             string[] yesNoOptions = new string[] { "yes", "no" };

//             Console.WriteLine("Do you want to select this seat?");
//             Console.WriteLine();
//             Console.WriteLine();
//             ConsoleKeyInfo key;

//             do
//             {
//                 Console.SetCursorPosition(0, Console.CursorTop - yesNoOptions.Length);
//                 for (int i = 0; i < yesNoOptions.Length; i++)
//                 {
//                     if (i == currentOption)
//                     {
//                         Console.BackgroundColor = ConsoleColor.Gray;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }

//                     Console.WriteLine(yesNoOptions[i]);

//                     Console.ResetColor();
//                 }

//                 key = Console.ReadKey(true);
//                 switch (key.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                     case ConsoleKey.LeftArrow:
//                         currentOption = Math.Max(0, currentOption - 1);
//                         break;
//                     case ConsoleKey.DownArrow:
//                     case ConsoleKey.RightArrow:
//                         currentOption = Math.Min(yesNoOptions.Length - 1, currentOption + 1);
//                         break;
//                 }
//             } while (key.Key != ConsoleKey.Enter);

//             if (currentOption == 0)
//             {
//                 string[] options = { "Extra Notes (Allergies, Wheelchair, etc.)", "Continue" };
//                 string selectedOption;
//                 string extraNotes = string.Empty;

//                 do
//                 {
//                     selectedOption = AskQuestionWithMenu(options);

//                     if (selectedOption == "Extra Notes (Allergies, Wheelchair, etc.)")
//                     {
//                         if (!string.IsNullOrEmpty(extraNotes))
//                         {
//                             Console.WriteLine($"Your previous notes were: {extraNotes}");
//                         }

//                         Console.WriteLine("Please enter your extra notes:");
//                         extraNotes = Console.ReadLine();
//                         // Add extraNotes to the database...
//                     }
//                 } while (selectedOption != "Continue");

//                 Console.WriteLine("If you select a seat, you have a max baggage limit of 20 kg. If you have more, you have to pay extra.");

//                 string[] baggageOptions = { "yes", "no" };
//                 int selectedIndex = 0;
//                 string baggageResponse = string.Empty;

//                 while (true)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("Do you want more baggage?");

//                     for (int i = 0; i < baggageOptions.Length; i++)
//                     {
//                         if (i == selectedIndex)
//                         {
//                             Console.BackgroundColor = ConsoleColor.Gray;
//                             Console.ForegroundColor = ConsoleColor.Black;
//                         }

//                         Console.WriteLine(baggageOptions[i]);

//                         Console.ResetColor();
//                     }

//                     ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

//                     switch (consoleKeyInfo.Key)
//                     {
//                         case ConsoleKey.UpArrow:
//                             selectedIndex = (selectedIndex - 1 + baggageOptions.Length) % baggageOptions.Length;
//                             break;
//                         case ConsoleKey.DownArrow:
//                             selectedIndex = (selectedIndex + 1) % baggageOptions.Length;
//                             break;
//                         case ConsoleKey.Enter:
//                             baggageResponse = baggageOptions[selectedIndex];
//                             goto EndLoop;
//                     }
//                 }

//             EndLoop:
//                 int extraKg = 0;
//                 int extraCost = 0;
//                 if (baggageResponse.ToLower() == "yes")
//                 {
//                     while (true)
//                     {
//                         Console.Write("How many kg do you want extra: ");
//                         string input = Console.ReadLine();
//                         if (int.TryParse(input, out extraKg) && extraKg >= 0)
//                         {
//                             break; // Exit the loop if the input is a valid number and not negative
//                         }
//                         else
//                         {
//                             Console.WriteLine("Invalid input. Please enter a valid number.");
//                         }
//                     }

//                     extraCost = extraKg * 4; // 4 euros per extra kg

//                     // Confirmation step
//                     string[] confirmationOptions = { "Yes", "No" };
//                     int confirmationIndex = 0;
//                     while (true)
//                     {
//                         Console.Clear();
//                         Console.WriteLine($"The extra cost for baggage is {extraCost} euros. Total cost: {chosenSeat.Price + extraCost} euros."); 
//                         Console.WriteLine("Do you really want to purchase with the extra baggage cost?");

//                         for (int i = 0; i < confirmationOptions.Length; i++)
//                         {
//                             if (i == confirmationIndex)
//                             {
//                                 Console.BackgroundColor = ConsoleColor.Gray;
//                                 Console.ForegroundColor = ConsoleColor.Black;
//                             }

//                             Console.WriteLine(confirmationOptions[i]);

//                             Console.ResetColor();
//                         }

//                         ConsoleKeyInfo confirmationKeyInfo = Console.ReadKey();

//                         switch (confirmationKeyInfo.Key)
//                         {
//                             case ConsoleKey.UpArrow:
//                                 if (confirmationIndex > 0)
//                                 {
//                                     confirmationIndex--;
//                                 }
//                                 break;
//                             case ConsoleKey.DownArrow:
//                                 if (confirmationIndex < confirmationOptions.Length - 1)
//                                 {
//                                     confirmationIndex++;
//                                 }
//                                 break;
//                             case ConsoleKey.Enter:
//                                 if (confirmationOptions[confirmationIndex] == "Yes")
//                                 {
//                                     chosenSeat.Price += extraCost; // Add extra cost to seat price only if 'Yes' is selected
//                                     goto EndConfirmation;
//                                 }

//                                 else if (confirmationOptions[confirmationIndex] == "No")
//                                 {
//                                     Console.WriteLine("You have cancelled your seat.");
//                                     return;
//                                 }
//                                 break;
//                         }

//                         Console.Clear();
//                     }

//                 EndConfirmation:

//                     Console.WriteLine($"Your total cost before discount is {chosenSeat.Price} euros.");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Your total cost before discount is {chosenSeat.Price} euros.");
//                 }

//                 // Apply discount if the user wants to redeem Fly Points
//                 string[] discountOptions = { "Yes", "No" };
//                 int discountIndex = 0;
//                 string discountResponse = string.Empty;
//                 bool appliedDiscount = false;

//                 while (true)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("Do you want to apply your discount now?");

//                     for (int i = 0; i < discountOptions.Length; i++)
//                     {
//                         if (i == discountIndex)
//                         {
//                             Console.BackgroundColor = ConsoleColor.Gray;
//                             Console.ForegroundColor = ConsoleColor.Black;
//                         }

//                         Console.WriteLine(discountOptions[i]);

//                         Console.ResetColor();
//                     }

//                     ConsoleKeyInfo discountKeyInfo = Console.ReadKey();

//                     switch (discountKeyInfo.Key)
//                     {
//                         case ConsoleKey.UpArrow:
//                             discountIndex = (discountIndex - 1 + discountOptions.Length) % discountOptions.Length;
//                             break;
//                         case ConsoleKey.DownArrow:
//                             discountIndex = (discountIndex + 1) % discountOptions.Length;
//                             break;
//                         case ConsoleKey.Enter:
//                             discountResponse = discountOptions[discountIndex];
//                             goto EndDiscountLoop;
//                     }
//                 }

//             EndDiscountLoop:

//                 if (discountResponse.ToLower() == "yes")
//                 {
//                     FlightLogic flightLogic = new FlightLogic();
//                     if (flightLogic.RedeemFlyPoints(current.Id))
//                     {
//                         chosenSeat.Price *= 0.9m; // Apply 10% discount
//                         appliedDiscount = true;
//                     }
//                     else
//                     {
//                         Console.WriteLine("Not enough Fly points to redeem. Minimum 20 points required.");
//                     }
//                 }

//                 if (appliedDiscount)
//                 {
//                     Console.WriteLine($"Your total cost after discount is {chosenSeat.Price} euros.");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
//                 }

//                 // counts where the seat is in the plane with numbers that customers understand
//                 string seatplace = "";
//                 int newseat = seat + 1;
//                 if (newseat == 1)
//                 {
//                     seatplace = (row + 1).ToString() + " - " + "A";
//                 }
//                 else if (newseat == 2)
//                 {
//                     seatplace = (row + 1).ToString() + " - " + "B";
//                 }
//                 else if (newseat == 3)
//                 {
//                     seatplace = (row + 1).ToString() + " - " + "C";
//                 }
//                 else if (newseat == 4)
//                 {
//                     seatplace = (row + 1).ToString() + " - " + "D";
//                 }
//                 else if (newseat == 5)
//                 {
//                     seatplace = (row + 1).ToString() + " - " + "E";
//                 }
//                 else if (newseat == 6)
//                 {
//                     seatplace = (row + 1).ToString() + " - " + "F";
//                 }
//                 // checks if there are notes to be added to the ticket
//                 string notes = "";
//                 if (extraCost > 0 || extraNotes != null)
//                 {
//                     notes = extraNotes + " And extra baggage of:" + extraCost + " Euro With a weight of" + extraKg;
//                 }

//                 // asks if user wants to reserve the seat
//                 Console.WriteLine("Do you want to reserve this seat?");
//                 string[] reserveOptions = { "Yes", "No" };
//                 int reserveIndex = 0;

//                 while (true)
//                 {
//                     Console.Clear();
//                     Console.WriteLine("Do you want to reserve this seat?");

//                     for (int i = 0; i < reserveOptions.Length; i++)
//                     {
//                         if (i == reserveIndex)
//                         {
//                             Console.BackgroundColor = ConsoleColor.Gray;
//                             Console.ForegroundColor = ConsoleColor.Black;
//                         }

//                         Console.WriteLine(reserveOptions[i]);

//                         Console.ResetColor();
//                     }

//                     ConsoleKeyInfo reserveKeyInfo = Console.ReadKey();

//                     switch (reserveKeyInfo.Key)
//                     {
//                         case ConsoleKey.UpArrow:
//                             reserveIndex = (reserveIndex - 1 + reserveOptions.Length) % reserveOptions.Length;
//                             break;
//                         case ConsoleKey.DownArrow:
//                             reserveIndex = (reserveIndex + 1) % reserveOptions.Length;
//                             break;
//                         case ConsoleKey.Enter:
//                             if (reserveOptions[reserveIndex] == "Yes")
//                             {
//                                 FlightLogic.Reserveseat(flightid, current.Id, seatplace, chosenSeat.Class, notes);
//                                 chosenSeat.IsReserved = true;
//                                 Console.WriteLine("Seat successfully reserved!");
//                                 break;
//                             }
//                             else
//                             {
//                                 Console.WriteLine("You have cancelled your seat.");
//                                 break;
//                             }
//                     }
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("You have cancelled your seat.");
//             }
//         }

//         public static void DisplaySeatLayoutBoeing737(int selectedRow = -1, int selectedSeat = -1)
//         {
//             Console.ForegroundColor = ConsoleColor.Cyan;
            
//             Console.WriteLine("If you select a seat, you have a max bagage limit of 20 kg. If you have more, you have to pay extra.");
//             Console.WriteLine("Seat plan:");
//             Console.WriteLine("Seat   row");
//             Console.WriteLine("      A B C  D E F");

//             for (int row = 0; row < 33; row++)
//             {
//                 if (row < 9)
//                 {
//                     Console.Write(" ");
//                 }

//                 if (row == 15 || row == 16)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Yellow;
//                 }
//                 else
//                 {
//                     Console.ForegroundColor = ConsoleColor.Cyan;
//                 }

//                 Console.Write($"{row + 1}    ");

//                 for (int seat = 0; seat < 6; seat++)
//                 {
//                     if (row == selectedRow && seat == selectedSeat)
//                     {
//                         Console.BackgroundColor = ConsoleColor.White;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }

//                     if (row == 15 || row == 16)
//                     {
//                         Console.ForegroundColor = ConsoleColor.Yellow;
//                     }
//                     else
//                     {
//                         Console.ForegroundColor = ConsoleColor.Cyan;
//                     }

//                     if (seats[row, seat].IsReserved)
//                     {
//                         Console.Write("X");
//                     }
//                     else
//                     {
//                         Console.Write("O");
//                     }

//                     if (seat == 2)
//                     {
//                         Console.Write("  ");
//                     }
//                     else
//                     {
//                         Console.Write(" ");
//                     }

//                     Console.ResetColor();
//                 }

//                 Console.WriteLine();
//             }
//         }

//         public string AskQuestionWithMenu(string[] options)
//         {
//             int currentOption = 0;

//             while (true)
//             {
//                 Console.Clear();

//                 for (int i = 0; i < options.Length; i++)
//                 {
//                     if (i == currentOption)
//                     {
//                         Console.BackgroundColor = ConsoleColor.Gray;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }

//                     Console.WriteLine(options[i]);

//                     Console.ResetColor();
//                 }

//                 ConsoleKeyInfo keyInfo = Console.ReadKey();

//                 if (keyInfo.Key == ConsoleKey.UpArrow)
//                 {
//                     if (currentOption > 0)
//                     {
//                         currentOption--;
//                     }
//                 }
//                 else if (keyInfo.Key == ConsoleKey.DownArrow)
//                 {
//                     if (currentOption < options.Length - 1)
//                     {
//                         currentOption++;
//                     }
//                 }
//                 else if (keyInfo.Key == ConsoleKey.Enter)
//                 {
//                     Console.Clear();
//                     return options[currentOption];
//                 }
//             }
//         }
//     }
// }



// DONT TOUCH...