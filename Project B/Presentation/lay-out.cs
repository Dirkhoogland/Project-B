using Project_B.BusinessLogic;
using Project_B.DataAcces;

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
    public void ToonMenu(CurrentUser current, int flightid)
        {
            int currentOption = 0;
            string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart", "Show Fly points", "Redeem Fly points" };

            ConsoleKeyInfo key;
            Console.Clear();

            FlightLogic flightLogic = new FlightLogic();

            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Welcome to the seat reservation system!");
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
                    ChooseSeatWithArrowKeys(current, flightid);
                    break;
                case 1:
                    DisplaySeatLayoutBoeing737();
                    break;
                case 2:
                    Console.WriteLine("Thank you for using the seat reservation system. Bye!");
                    return;
                case 3:
                    int points = flightLogic.GetFlyPoints(current.Id);
                    Console.WriteLine($"Current Fly points balance: {points}");
                    break;
                case 4:
                    if (flightLogic.RedeemFlyPoints(current.Id))
                    {
                        Console.WriteLine("20 Fly points redeemed for a 10% discount on your next booking.");
                    }
                    else
                    {
                        Console.WriteLine("Not enough Fly points to redeem. Minimum 20 points required.");
                    }
                    int newPoints = flightLogic.GetFlyPoints(current.Id);
                    Console.WriteLine($"New Fly points balance: {newPoints}");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine();
        }

        public static void ChooseSeatWithArrowKeys(CurrentUser current, int flightid)
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
                        ReserveSeat(row, seat, current, flightid);
                        return;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        public static void ReserveSeat(int row, int seat, CurrentUser current, int flightid)
        {
            Console.WriteLine("Welcome to the seat reservation system");

            Seat chosenSeat = seats[row, seat];
            if (chosenSeat.IsReserved)
            {
                Console.WriteLine("This seat has already been reserved. Choose another seat.");
                return;
            }

            Console.WriteLine($"You have chosen this seat: {row + 1}{(char)(seat + 'A')}. Class: {chosenSeat.Class}, Price: {chosenSeat.Price}");

            int currentOption = 0;
            string[] yesNoOptions = new string[] { "yes", "no" };

            Console.WriteLine("Do you want to select this seat?");
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
                    selectedOption = AskQuestionWithMenu(options);

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

                Console.WriteLine("If you select a seat, you have a max baggage limit of 20 kg. If you have more, you have to pay extra.");

                string[] baggageOptions = { "yes", "no" };
                int selectedIndex = 0;
                string baggageResponse = string.Empty;

                Console.WriteLine("Do you want more baggage?");

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Do you want more baggage?");

                    for (int i = 0; i < baggageOptions.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(baggageOptions[i]);

                        Console.ResetColor();
                    }

                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + baggageOptions.Length) % baggageOptions.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % baggageOptions.Length;
                            break;
                        case ConsoleKey.Enter:
                            baggageResponse = baggageOptions[selectedIndex];
                            goto EndLoop;
                    }
                }

                EndLoop:

                if (baggageResponse.ToLower() == "yes")
                {
                    Console.Write("How many kg do you want extra: ");
                    int extraKg = Convert.ToInt32(Console.ReadLine());
                    int extraCost = extraKg * 4; // 4 euros per extra kg

                    Console.WriteLine($"The extra cost for baggage is {extraCost} euros."); 

                    chosenSeat.Price += extraCost; // Add extra cost to seat price

                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
                }
                else
                {
                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
                }

                // Flight points usage step
                int flightPoints = new FlightLogic().GetFlyPoints(current.Id);
                Console.WriteLine($"Current Flight Points: {flightPoints}");
                Console.WriteLine("Would you like to use flight points for this reservation? (yes/no)");
                string useFlightPoints = Console.ReadLine();
                if (useFlightPoints.ToLower() == "yes")
                {
                    if (flightPoints >= 20) // Check if user has enough points (assuming 20 points for a discount)
                    {
                        new FlightLogic().RedeemFlyPoints(current.Id);
                        chosenSeat.Price -= (chosenSeat.Price * 0.10m); // Apply 10% discount
                        Console.WriteLine("Flight points applied to your reservation.");
                        Console.WriteLine($"New total cost is {chosenSeat.Price} euros.");
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough flight points.");
                        Console.WriteLine($"Your total cost is {chosenSeat.Price} euros. Do you want to proceed? (yes/no)");
                        string proceed = Console.ReadLine();
                        if (proceed.ToLower() != "yes")
                        {
                            Console.WriteLine("Reservation cancelled.");
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros. Do you want to proceed? (yes/no)");
                    string proceed = Console.ReadLine();
                    if (proceed.ToLower() != "yes")
                    {
                        Console.WriteLine("Reservation cancelled.");
                        return;
                    }
                }

                string seatplace = "";
                int newseat = seat + 1;
                if (newseat == 1)
                {
                    seatplace = (row + 1).ToString() + " - " + "A";
                }
                else if (newseat == 2)
                {
                    seatplace = (row + 1).ToString() + " - " + "B";
                }
                else if (newseat == 3)
                {
                    seatplace = (row + 1).ToString() + " - " + "C";
                }
                else if (newseat == 4)
                {
                    seatplace = (row + 1).ToString() + " - " + "D";
                }
                else if (newseat == 5)
                {
                    seatplace = (row + 1).ToString() + " - " + "E";
                }
                else if (newseat == 6)
                {
                    seatplace = (row + 1).ToString() + " - " + "F";
                }
                FlightLogic.Reserveseat(flightid, current.Id, seatplace, chosenSeat.Class);
                chosenSeat.IsReserved = true;
                Console.WriteLine("Seat successfully reserved!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You have cancelled your seat.");
            }
        }
        public static void DisplaySeatLayoutBoeing737(int selectedRow = -1, int selectedSeat = -1)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            Console.WriteLine("If you select a seat, you have a max bagage limit of 20 kg. If you have more, you have to pay extra.");
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
        public static string AskQuestionWithMenu(string[] options)
        {
            int currentOption = 0;
            ConsoleKeyInfo key;
            do
            {
                Console.SetCursorPosition(0, Console.CursorTop - options.Length);
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(options[i]);

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
                        currentOption = Math.Min(options.Length - 1, currentOption + 1);
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return options[currentOption];
        }
    }
}