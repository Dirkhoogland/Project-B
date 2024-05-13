namespace Project_B.Presentation
{
    public class BoeingSeat
    {
        public string Class { get; set; }
        public decimal Price { get; set; }
        public bool IsReserved { get; set; }
    

    static BoeingSeat [,] boeingseats = new BoeingSeat[38, 9]; 
    public void lay_out()
    {
    for (int i = 0; i < 38; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            if (j == 0 || j == 8) 
                {
                    boeingseats[i, j] = new BoeingSeat { Class = "Economy", Price = 125m, IsReserved = false };
                }
                else
                {
                    boeingseats[i, j] = new BoeingSeat { Class = "Economy", Price = 100m, IsReserved = false };
                }
                if (i < 6) 
                {
                    boeingseats[i, j] = new BoeingSeat { Class = "Business", Price = 200m, IsReserved = false };
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
                DisplaySeatLayoutAirbus();
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
                DisplaySeatLayoutAirbus(row, seat);

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        row = Math.Max(0, row - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        row = Math.Min(38, row + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        seat = Math.Max(0, seat - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        seat = Math.Min(8, seat + 1);
                        break;
                    case ConsoleKey.Enter:
                        ReserveSeat(row, seat);
                        return;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        public static void ReserveSeat(int row, int seat)
        {
            BoeingSeat chosenSeat = boeingseats[row, seat];
            if (chosenSeat.IsReserved)
            {
                Console.WriteLine("This seat has already been reserved. Choose another seat.");
                return;
            }

            Console.WriteLine($"You have chosen this seat: {row + 1}{(char)(seat + 'A')}. Class: {chosenSeat.Class}, Price: {chosenSeat.Price}");

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
                string[] options = { "Extra Notes (Allergies, Wheelchair, etc.)", "Continue" };
                string selectedOption;
                string extraNotes = string.Empty;

                do
                {
                    selectedOption = Seat.AskQuestionWithMenu(options);

                    if (selectedOption == "Extra Notes (Allergies, Wheelchair, etc.)")
                    {
                        if (!string.IsNullOrEmpty(extraNotes))
                        {
                            Console.WriteLine($"Your previous notes were: {extraNotes}");
                        }

                        Console.WriteLine("Please enter your extra notes:");
                        extraNotes = Console.ReadLine();
                        // Add extraNotes to the database...
                    }
                } while (selectedOption != "Continue");

                chosenSeat.IsReserved = true;
                Console.WriteLine("Seat succesfully reserved!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You have cancelled your seat.");
            }
        }

        public static void DisplaySeatLayoutAirbus(int selectedRow = -1, int selectedSeat = -1)
{
    Console.WriteLine("If you select a seat, you have a max bagage limit of 20 kg. If you have more, you have to pay extra.");
    Console.WriteLine("Seating plan:");
    Console.WriteLine("seats   rows");
    Console.WriteLine("   A B C   D E F   G H I");

    for (int row = 0; row < 38; row++)
    {
        if (row < 6) // If the current row is one of the first six rows, change the color to dark yellow
        {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        else if (row >= 15 && row < 22) // If the current row is between 16 and 22, change the color to blue
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        if (row == 3 || row == 26)
        {
            Console.WriteLine("\n");
        }

        Console.Write($"{row + 1,-3}"); // Adjusted to align the row numbers

        for (int seat = 0; seat < 9; seat++)
        {
            if (row < 6) // If the current row is one of the first six rows, change the color to dark yellow
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (row >= 15 && row < 22) // If the current row is between 16 and 22, change the color to blue
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            if (row == 22 && seat >= 3 && seat <= 5) 
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            if (row == 26 && seat >= 1 && seat <= 7) 
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            if (row == 36 || row == 37)
            {
                if (seat < 3 || seat > 5)
                {
                    Console.Write("   "); // Adjusted to align the seats
                    continue;
                }
            }

             if (row < 6 && (seat == 0 || seat == 8 || seat == 4)) // If the current row is one of the first two rows and the seat is the first or last, skip the iteration
                    {
                        Console.Write("  ");
                        continue;
                    }

            if (row >= 6 && row <= 14 || row == 25)
            {
                continue;
            }

            if (row == selectedRow && seat == selectedSeat)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            if (boeingseats[row, seat].IsReserved)
            {
                Console.Write("X ");
            }
            else
            {
                Console.Write("O ");
            }

            if (seat == 2 || seat == 5)
            {
                Console.Write("  ");
            }

            Console.ResetColor();
        }
        Console.ResetColor();
        Console.WriteLine();
    }

        }
    }
}