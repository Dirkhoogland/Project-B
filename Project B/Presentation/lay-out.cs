// namespace Project_B.Presentation
// {
//     public class lay_out
//     {
//         static bool[,] seats = new bool[33, 6];
//         public void ToonMenu()
//         {
//             Console.WriteLine("Welkom bij het stoelreserveringssysteem voor het vliegtuig!");
//             Console.WriteLine("Beschikbare opties:");
//             Console.WriteLine("1. Reserveer een stoel");
//             Console.WriteLine("2. Bekijk de stoelindeling");
//             Console.WriteLine("3. Verlaat het programma");

//             Console.Write("Voer uw keuze in (1-3): ");
//             int choice = Convert.ToInt32(Console.ReadLine());

//             switch (choice)
//             {
//                 case 1:
//                     ReserveSeat();
//                     break;
//                 case 2:
//                     DisplaySeatLayout();
//                     break;
//                 case 3:
//                     Console.WriteLine("Bedankt voor het gebruik van het stoelreserveringssysteem. Tot ziens!");
//                     return;
//                 default:
//                     Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
//                     break;
//             }

//             Console.WriteLine();
//         }
    
//         public static void ReserveSeat()
//         {
//             Console.WriteLine("Beschikbare stoelen:");
//             DisplaySeatLayout();

//             Console.Write("Voer de rijnummer in (1-33): ");
//             int row = Convert.ToInt32(Console.ReadLine());

//             Console.Write("Voer de stoelnummer in (A-F): ");
//             char seat = Convert.ToChar(Console.ReadLine().ToUpper());

//             int rowIndex = row - 1;
//             int seatIndex = seat - 'A';

//             if (rowIndex < 0 || rowIndex >= 33 || seatIndex < 0 || seatIndex >= 6)
//             {
//                 Console.WriteLine("Ongeldige stoel. Probeer het opnieuw.");
//                 return;
//             }

//             if (seats[rowIndex, seatIndex])
//             {
//                 Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
//                 return;
//             }

//             seats[rowIndex, seatIndex] = true;
//             Console.WriteLine("Stoel succesvol gereserveerd!");
//         }

//         public static void DisplaySeatLayout()
//         {
//             Console.WriteLine("Stoelindeling:");
//             Console.WriteLine("Rij  Stoelen");
//             Console.WriteLine("     A B C  D E F");

//             for (int row = 0; row < 33; row++)
//             {
//                 Console.Write($"{row + 1}    ");

//                 for (int seat = 0; seat < 6; seat++)
//                 {
//                     if (seats[row, seat])
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
//                 }

//                 Console.WriteLine();
//             }
//         }
//     }
// }

namespace Project_B.Presentation
{
    public class Seat
    {
        public string Class { get; set; }
        public decimal Price { get; set; }
        public bool IsReserved { get; set; }
    

    static Seat[,] seats = new Seat[33, 6]; 
    public void lay_out()
    {
    for (int i = 0; i < 33; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (j == 0 || j == 5) 
                {
                    seats[i, j] = new Seat { Class = "Economy", Price = 125m, IsReserved = false };
                }
                else
                {
                    seats[i, j] = new Seat { Class = "Economy", Price = 100m, IsReserved = false };
                }
                 if (i == 15 || i == 16) 
                {
                    seats[i, j] = new Seat { Class = "Business", Price = 200m, IsReserved = false };
                }
            }
        }
    }
    public void ToonMenu()
    {
        int currentOption = 0;
        string[] menuOptions = new string[] { "Reserveer een stoel", "Bekijk de stoelindeling", "Verlaat het programma" };

        ConsoleKeyInfo key;
        Console.Clear();

        do
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Welkom bij het stoelreserveringssysteem voor het vliegtuig!");
            Console.WriteLine("Beschikbare opties:");

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == currentOption)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{i + 1}. {menuOptions[i]}");

                Console.ResetColor();
            }

            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    currentOption = Math.Max(0, currentOption - 1);
                    break;
                case ConsoleKey.DownArrow:
                    currentOption = Math.Min(menuOptions.Length - 1, currentOption + 1);
                    break;
            }
        } while (key.Key != ConsoleKey.Enter);

        switch (currentOption)
        {
            case 0:
                ChooseSeatWithArrowKeys();
                break;
            case 1:
                DisplaySeatLayout();
                break;
            case 2:
                Console.WriteLine("Bedankt voor het gebruik van het stoelreserveringssysteem. Tot ziens!");
                return;
        }

        Console.WriteLine();
    }

        public static void ChooseSeatWithArrowKeys()
        {
            Console.Clear();
            int row = 0;
            int seat = 0;

            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0,0);
                DisplaySeatLayout(row, seat);

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        row = Math.Max(0, row - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        row = Math.Min(32, row + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        seat = Math.Max(0, seat - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        seat = Math.Min(5, seat + 1);
                        break;
                    case ConsoleKey.Enter:
                        ReserveSeat(row, seat);
                        return;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        public static void ReserveSeat(int row, int seat)
        {
            Seat chosenSeat = seats[row, seat];
            if (chosenSeat.IsReserved)
            {
                Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
                return;
            }

            Console.WriteLine($"Je hebt deze stoel gekozen: {row + 1}{(char)(seat + 'A')}. Klasse: {chosenSeat.Class}, Prijs: {chosenSeat.Price}");

            int currentOption = 0;
            string[] yesNoOptions = new string[] { "ja", "nee" };

            Console.WriteLine("Wil je deze stoel selecteren?");
            Console.WriteLine();
            Console.WriteLine();
            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0, Console.CursorTop - yesNoOptions.Length);
                for (int i = 0; i < yesNoOptions.Length; i++)
                {
                    if (i == currentOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(yesNoOptions[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                        currentOption = Math.Max(0, currentOption - 1);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.RightArrow:
                        currentOption = Math.Min(yesNoOptions.Length - 1, currentOption + 1);
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            if (currentOption == 0)
            {
                chosenSeat.IsReserved = true;
                Console.WriteLine("Stoel succesvol gereserveerd!");
            }
            else
            {
                Console.WriteLine("je hebt je stoel gecanceled.");
            }
        }

        public static void DisplaySeatLayout(int selectedRow = -1, int selectedSeat = -1)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Stoelindeling:");
            Console.WriteLine("Rij   Stoelen");
            Console.WriteLine("      A B C  D E F");

            for (int row = 0; row < 33; row++)
            {
                if (row < 9)
                {
                    Console.Write(" ");
                }

                if (row == 15 || row == 16)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
            

                Console.Write($"{row + 1}    ");

                for (int seat = 0; seat < 6; seat++)
                {
                    if (row == selectedRow && seat == selectedSeat)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    
                    if (row == 15 || row == 16)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    if (seats[row, seat].IsReserved)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("O");
                    }

                    if (seat == 2)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }
    }
}