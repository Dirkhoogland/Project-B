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
        string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart" };

        ConsoleKeyInfo key;
        Console.Clear();

        do
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Welcome to the Boeing seat reservation system!");
            Console.WriteLine("Available options:");

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
                DisplaySeatLayoutBoeing737();
                break;
            case 2:
                Console.WriteLine("Thank you for using the seat reservation system. Bye!");
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
                DisplaySeatLayoutBoeing737(row, seat);

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
                Console.WriteLine("This seat has already been reserved. Choose another seat.");
                return;
            }

            Console.WriteLine($"You hace chosen this seat: {row + 1}{(char)(seat + 'A')}. Class: {chosenSeat.Class}, Price: {chosenSeat.Price}");

            int currentOption = 0;
            string[] yesNoOptions = new string[] { "yes", "no" };

            Console.WriteLine("Do you want to select this seat?");
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
                Console.WriteLine("Seat succesfully reserved!");
                Console.WriteLine("Do you have allergies? (yes/no)");
                string? hasAllergies = Console.ReadLine();

                if (hasAllergies?.ToLower() == "yes")
                {
                    Console.WriteLine("Do you need medication for your allergies? (yes/no)");
                }
            }
            else
            {
                Console.WriteLine("You have cancelled your seat.");
            }
        }

        public static void DisplaySeatLayoutBoeing737(int selectedRow = -1, int selectedSeat = -1)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Seat plan:");
            Console.WriteLine("Seat   row");
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