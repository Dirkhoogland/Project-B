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
public static void DisplaySeatLayout(int currentRow, int currentSeat)
{
    for (int i = 0; i < 33; i++)
    {
        // Add an extra space at the start of the first 9 rows
        Console.Write(i < 9 ? $" {i + 1}   " : $"{i + 1}   ");

        for (int seat = 0; seat < 6; seat++)
        {
            if (i == currentRow && seat == currentSeat)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            if (seats[i, seat].IsReserved)
            {
                Console.Write("X");
            }
            else
            {
                Console.Write("O");
            }

            Console.ResetColor();

            if (seat == 2)
            {
                Console.Write("  ");
            }
            else
            {
                Console.Write(" ");
            }
        }

        Console.WriteLine();
    }

    Console.ResetColor();
}
public void ToonMenu()
{
    string[] menuOptions = { "Reserveer een stoel", "Bekijk de stoelindeling", "Verlaat het programma" };
    int currentOption = 0;

    while (true)
    {
        Console.Clear();
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

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (currentOption > 0) currentOption--;
                break;
            case ConsoleKey.DownArrow:
                if (currentOption < menuOptions.Length - 1) currentOption++;
                break;
            case ConsoleKey.Enter:
                switch (currentOption)
                {
                    case 0:
                        ReserveSeat();
                        break;
                    case 1:
                        DisplaySeatLayout(0,0);
                        break;
                    case 2:
                        Console.WriteLine("Bedankt voor het gebruik van het stoelreserveringssysteem. Tot ziens!");
                        return;
                }
                break;
        }

        Console.WriteLine();
    }
}

public static void ReserveSeat()
{
    int currentRow = 0;
    int currentSeat = 0;

    while (true)
    {
        Console.SetCursorPosition(0, 0);

        // Clear the lines that display the seat layout
        for (int i = 0; i < 36; i++)
        {
            Console.Write(new string(' ', Console.WindowWidth));
        }

        Console.SetCursorPosition(0, 0);

        // Print the first few lines
        Console.WriteLine("Seat layout:");
        Console.WriteLine("Row  Seats");
        Console.WriteLine("     A B C  D E F");

        DisplaySeatLayout(currentRow, currentSeat);

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);



        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (currentRow > 0) currentRow--;
                break;
            case ConsoleKey.DownArrow:
                if (currentRow < 32) currentRow++;
                break;
            case ConsoleKey.LeftArrow:
                if (currentSeat > 0) currentSeat--;
                break;
            case ConsoleKey.RightArrow:
                if (currentSeat < 5) currentSeat++;
                break;
            case ConsoleKey.Enter:
                Seat chosenSeat = seats[currentRow, currentSeat];
                if (chosenSeat.IsReserved)
                {
                    Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine($"Je hebt deze stoel gekozen: {currentRow + 1}{(char)('A' + currentSeat)}. Klasse: {chosenSeat.Class}, Prijs: {chosenSeat.Price}");

                string[] confirmationOptions = { "Ja", "Nee" };
                int currentOption = 0;

                while (true)
                {
                    Console.WriteLine("Wil je deze stoel selecteren?");
                    for (int i = 0; i < confirmationOptions.Length; i++)
                    {
                        if (i == currentOption)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(confirmationOptions[i]);

                        Console.ResetColor();
                    }

                    keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (currentOption > 0) currentOption--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (currentOption < confirmationOptions.Length - 1) currentOption++;
                            break;
                        case ConsoleKey.Enter:
                            if (confirmationOptions[currentOption] == "Ja")
                            {
                                chosenSeat.IsReserved = true;
                                Console.WriteLine("Stoel succesvol gereserveerd!");
                                Console.ReadLine();
                            }
                            return;
                    }

                    Console.WriteLine();
                }
        }
    }
}
    }
}