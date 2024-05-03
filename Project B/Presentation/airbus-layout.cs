namespace Project_B.Presentation
{
    public class AirbusSeat
    {
        public string Class { get; set; }
        public decimal Price { get; set; }
        public bool IsReserved { get; set; }
    

    static AirbusSeat [,] airbusseats = new AirbusSeat[50, 9]; 
    public void lay_out()
    {
    for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (j == 0 || j == 5) 
                {
                    airbusseats[i, j] = new AirbusSeat { Class = "Economy", Price = 125m, IsReserved = false };
                }
                else
                {
                    airbusseats[i, j] = new AirbusSeat { Class = "Economy", Price = 100m, IsReserved = false };
                }
                 if (i == 15 || i == 16) 
                {
                    airbusseats[i, j] = new AirbusSeat { Class = "Business", Price = 200m, IsReserved = false };
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
            Console.WriteLine("Welkom bij het stoelreserveringssysteem voor de airbus!");
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
                DisplaySeatLayoutAirbus();
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
                DisplaySeatLayoutAirbus(row, seat);

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
            AirbusSeat chosenSeat = airbusseats[row, seat];
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

        public static void DisplaySeatLayoutAirbus(int selectedRow = -1, int selectedSeat = -1)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Stoelindeling:");
            Console.WriteLine("Rij   Stoelen");
            Console.WriteLine("      A B C  D E F");

            for (int row = 0; row < 9; row++)
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

                for (int seat = 0; seat < 50; seat++)
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

                    if (airbusseats[row, seat].IsReserved)
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