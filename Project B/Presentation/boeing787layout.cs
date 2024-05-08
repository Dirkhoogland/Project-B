// namespace Project_B.Presentation
// {
//     public class BoeingSeat
//     {
//         public string Class { get; set; }
//         public decimal Price { get; set; }
//         public bool IsReserved { get; set; }
    

//     static BoeingSeat [,] boeingseats = new BoeingSeat[50, 9]; 
//     public void lay_out()
//     {
//     for (int i = 0; i < 50; i++)
//     {
//         for (int j = 0; j < 9; j++)
//         {
//             boeingseats[i, j] = new BoeingSeat { Class = "Business", Price = 200m, IsReserved = false };
            
//             if (i < 2) // If the current row is one of the first two rows, set the class to "Club Class"
//             {
//                 boeingseats[i, j] = new BoeingSeat { Class = "Club Class", Price = 200m, IsReserved = false };
//             }
//             else if (i == 3 || i == 13 || i == 35) // If the current row is the 4th, 14th, or 36th row, set the class to "Seat with Extra Space"
//             {
//                 boeingseats[i, j] = new BoeingSeat { Class = "Seat with Extra Space", Price = 200m, IsReserved = false };
//             }
//             else if (i >= 4 && i <= 8) // If the current row is one of the rows 5-9, set the class to "Deluxe"
//             {
//                 boeingseats[i, j] = new BoeingSeat { Class = "Deluxe", Price = 200m, IsReserved = false };
//             }
//             if (i >= 43 && i <= 48 && (j <= 1 || j >= 7)) // If the current row is one of the rows 44-49 and the seat is one of the left 2 or right 2, set the class to "Duo Combo Seat"
//             {
//                 boeingseats[i, j] = new BoeingSeat { Class = "Duo Combo Seat", Price = 200m, IsReserved = false };
//             }
            
//         }
//     }
//     }
//     public void ToonMenu()
//     {
//         int currentOption = 0;
//         string[] menuOptions = new string[] { "Reserveer een stoel", "Bekijk de stoelindeling", "Verlaat het programma" };

//         ConsoleKeyInfo key;
//         Console.Clear();

//         do
//         {
//             Console.SetCursorPosition(0, 0);
//             Console.WriteLine("Welkom bij het stoelreserveringssysteem voor de airbus!");
//             Console.WriteLine("Beschikbare opties:");

//             for (int i = 0; i < menuOptions.Length; i++)
//             {
//                 if (i == currentOption)
//                 {
//                     Console.BackgroundColor = ConsoleColor.Gray;
//                     Console.ForegroundColor = ConsoleColor.Black;
//                 }

//                 Console.WriteLine($"{i + 1}. {menuOptions[i]}");

//                 Console.ResetColor();
//             }

//             key = Console.ReadKey(true);
//             switch (key.Key)
//             {
//                 case ConsoleKey.UpArrow:
//                     currentOption = Math.Max(0, currentOption - 1);
//                     break;
//                 case ConsoleKey.DownArrow:
//                     currentOption = Math.Min(menuOptions.Length - 1, currentOption + 1);
//                     break;
//             }
//         } while (key.Key != ConsoleKey.Enter);

//         switch (currentOption)
//         {
//             case 0:
//                 ChooseSeatWithArrowKeys();
//                 break;
//             case 1:
//                 DisplaySeatLayoutAirbus();
//                 break;
//             case 2:
//                 Console.WriteLine("Bedankt voor het gebruik van het stoelreserveringssysteem. Tot ziens!");
//                 return;
//         }

//         Console.WriteLine();
//     }

//         public static void ChooseSeatWithArrowKeys()
//         {
//             Console.Clear();
//             int row = 0;
//             int seat = 0;

//             ConsoleKeyInfo key;

//             do
//             {
//                 Console.SetCursorPosition(0,0);
//                 DisplaySeatLayoutAirbus(row, seat);

//                 key = Console.ReadKey(true);

//                 switch (key.Key)
//                 {
//                     case ConsoleKey.UpArrow:
//                         row = Math.Max(0, row - 1);
//                         break;
//                     case ConsoleKey.DownArrow:
//                         row = Math.Min(50, row + 1);
//                         break;
//                     case ConsoleKey.LeftArrow:
//                         seat = Math.Max(0, seat - 1);
//                         break;
//                     case ConsoleKey.RightArrow:
//                         seat = Math.Min(8, seat + 1);
//                         break;
//                     case ConsoleKey.Enter:
//                         ReserveSeat(row, seat);
//                         return;
//                 }
//             } while (key.Key != ConsoleKey.Escape);
//         }

//         public static void ReserveSeat(int row, int seat)
//         {
//             BoeingSeat chosenSeat = boeingseats[row, seat];
//             if (chosenSeat.IsReserved)
//             {
//                 Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
//                 return;
//             }

//             Console.WriteLine($"Je hebt deze stoel gekozen: {row + 1}{(char)(seat + 'A')}. Klasse: {chosenSeat.Class}, Prijs: {chosenSeat.Price}");

//             int currentOption = 0;
//             string[] yesNoOptions = new string[] { "ja", "nee" };

//             Console.WriteLine("Wil je deze stoel selecteren?");
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
//                 chosenSeat.IsReserved = true;
//                 Console.WriteLine("Stoel succesvol gereserveerd!");
//             }
//             else
//             {
//                 Console.WriteLine("je hebt je stoel gecanceled.");
//             }
//         }

//         public static void DisplaySeatLayoutAirbus(int selectedRow = -1, int selectedSeat = -1)
//         {
            

//             Console.WriteLine("Stoelindeling:");
//             Console.WriteLine("Rij   Stoelen");
//             Console.WriteLine("      A B C  D E F  G H I");

//             for (int row = 0; row < 50; row++)
//             {
//                 if (row < 9)
//                 {
//                     Console.Write(" ");
//                 }

//                 if (row < 2) // If the current row is one of the first two rows, change the color to gray
//                 {
//                     Console.ForegroundColor = ConsoleColor.Gray;
//                 }

//                 if (row == 3 || row == 13 || row == 35) // If the current row is the fourth row, change the color to magenta
//                 {
//                     Console.ForegroundColor = ConsoleColor.Magenta;
//                 }
                

//                 if (row >= 4 && row <= 8) // If the current row is one of the rows 5-9, change the color to dark magenta
//                 {
//                     Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                 }
                
//                 Console.Write($"{row + 1}    ");

//                 if (row == 2 || row == 10 || row == 11 || row == 12 || row == 33 || row == 34) // If the current row is the third row, skip the iteration
//                 {
//                     Console.WriteLine();
//                     continue;
//                 }

//                 for (int seat = 0; seat < 9; seat++) // Adjusted to 9 seats
//                 {
//                     if ((row >= 43 && row <= 48) && (seat == 2 || seat == 6)) // If the current row is one of the rows 44-48 and the seat is the 3rd or 7th, skip the iteration
//                     {
//                         Console.Write("  ");
//                         continue;
//                     }
//                     if (row < 2 && (seat == 0 || seat == 8)) // If the current row is one of the first two rows and the seat is the first or last, skip the iteration
//                     {
//                         Console.Write("  ");
//                         continue;
//                     }
//                     if (row == 9) // If the current row is the 10th row
//                     {
//                         if (seat < 3 || seat > 5) // If the seat is not one of the 4th, 5th, or 6th, skip the iteration
//                         {
//                             Console.Write("  ");
//                             continue;
//                         }
//                     }

//                     if (row >= 43 && row <= 48) // If the current row is one of the rows 44-49
//                     {
//                         if (seat <= 1 || seat >= 7) // If the seat is one of the left 2 or right 2, change the color to blue
//                         {
//                             Console.ForegroundColor = ConsoleColor.Blue;
//                         }
//                     }

//                     if (row == 3) // If the current row is the fourth row, change the color to magenta
//                     {
//                         Console.ForegroundColor = ConsoleColor.Magenta;
//                     }

//                     if (row == 13) // If the current row is the fourth row, change the color to magenta
//                     {
//                         Console.ForegroundColor = ConsoleColor.Magenta;
//                     }

//                     if (row == 35) // If the current row is the fourth row, change the color to magenta
//                     {
//                         Console.ForegroundColor = ConsoleColor.Magenta;
//                     }

//                     if (row >= 4 && row <= 8) // If the current row is between 5 and 9, change the color to dark magenta
//                     {
//                         Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                     }

//                     if (row < 2) // If the current row is either 1 or 2, change the color to gray
//                     {
//                         Console.ForegroundColor = ConsoleColor.Gray;
//                     }

//                     if (row == 49 && (seat < 3 || seat > 5)) // If the current row is the 50th row and the seat is one of the first 3 or last 3, skip the iteration
//                     {
//                         Console.Write("  ");
//                         continue;
//                     }

//                     if (row == selectedRow && seat == selectedSeat)
//                     {
//                         Console.BackgroundColor = ConsoleColor.White;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }

//                     if (boeingseats[row, seat].IsReserved)
//                     {
//                         Console.Write("X");
//                     }
//                     else
//                     {
//                         Console.Write("O");
//                     }

//                     if (seat == 2 || seat == 5) // Add a space after the 3rd and 6th seats
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
//     }
// }