using Project_B.DataAcces;
using Project_B.BusinessLogic;

namespace Project_B.Presentation
{
    public class BoeingSeat
    {
        public string Class { get; set; }
        public decimal Price { get; set; }
        public bool IsReserved { get; set; }


        static BoeingSeat[,] boeingseats = new BoeingSeat[38, 9];
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
        public void ToonMenu(CurrentUser current, int flightid)
        {
            int currentOption = 0;
            string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart", "Show Fly points" };

            ConsoleKeyInfo key;
            Console.Clear();

            FlightLogic flightLogic = new FlightLogic();

            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Welcome to the Boeing 787 seat reservation system!");
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
                    DisplaySeatLayoutAirbus(flightid);
                    break;
                case 2:
                    Console.WriteLine("Thank you for using the seat reservation system. Bye!");
                    return;
                case 3:
                    int points = flightLogic.GetFlyPoints(current.Id);
                    Console.WriteLine($"Current Fly points balance: {points}");
                    Console.ReadLine(); // Wait for user input to return to the menu
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            switch (currentOption)
            {
                case 0:
                    ChooseSeatWithArrowKeys(current, flightid);
                    break;
                case 1:
                    DisplaySeatLayoutAirbus(flightid);
                    break;
                case 2:
                    Console.WriteLine("Thank you for using the seat reservation system. Bye!");
                    return;
            }
        }

        public static void ChooseSeatWithArrowKeys(CurrentUser current, int flightid)
        {
            Console.Clear();
            int row = 0;
            int seat = 0;

            ConsoleKeyInfo key;

            do
            {
                Console.SetCursorPosition(0, 0);
                DisplaySeatLayoutAirbus(flightid, row, seat);

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        row = Math.Max(0, row - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        row = Math.Min(boeingseats.GetLength(0) - 1, row + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        seat = Math.Max(0, seat - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        seat = Math.Min(boeingseats.GetLength(1) - 1, seat + 1);
                        break;
                    case ConsoleKey.Enter:
                        ReserveSeat(row, seat, current, flightid);
                        return;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        public static void ReserveSeat(int row, int seat, CurrentUser current, int flightid)
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
            ConsoleKeyInfo key;

            do
            {
                if (Console.CursorTop >= yesNoOptions.Length)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - yesNoOptions.Length);
                }
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

                    Console.WriteLine($"Your total cost before discount is {chosenSeat.Price} euros.");
                }
                else
                {
                    Console.WriteLine($"Your total cost before discount is {chosenSeat.Price} euros.");
                }

                Console.Clear(); // Clear console before asking about fly points
                Console.WriteLine("Do you want to apply your fly points for a discount?");

                currentOption = 0;

                do
                {
                    if (Console.CursorTop >= yesNoOptions.Length)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - yesNoOptions.Length);
                    }
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

                FlightLogic flightLogic = new FlightLogic();
                bool discountApplied = false;

                if (currentOption == 0)
                {
                    if (flightLogic.CanRedeemFlyPoints(current.Id))
                    {
                        if (flightLogic.RedeemFlyPoints(current.Id))
                        {
                            decimal discount = chosenSeat.Price * 0.1m;
                            chosenSeat.Price -= discount;
                            Console.WriteLine($"10% discount applied. Your total cost is now {chosenSeat.Price} euros.");
                            discountApplied = true;
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough fly points to redeem for a discount.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough fly points to redeem for a discount.");
                    }
                }

                if (!discountApplied)
                {
                    Console.WriteLine("No discount applied. Your total cost remains {0} euros.", chosenSeat.Price);
                }

                int remainingPoints = flightLogic.GetFlyPoints(current.Id);
                Console.WriteLine($"Your remaining fly points balance is {remainingPoints}.");

                Console.WriteLine("Do you want to reserve the seat?");

                currentOption = 0;

                do
                {
                    if (Console.CursorTop >= yesNoOptions.Length)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - yesNoOptions.Length);
                    }
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
                    else if (newseat == 7)
                    {
                        seatplace = (row + 1).ToString() + " - " + "G";
                    }
                    else if (newseat == 8)
                    {
                        seatplace = (row + 1).ToString() + " - " + "H";
                    }
                    else if (newseat == 9)
                    {
                        seatplace = (row + 1).ToString() + " - " + "I";
                    }
                    FlightLogic.Reserveseat(flightid, current.Id, seatplace, chosenSeat.Class, false, ""); // Adjusted to match your method signature
                    chosenSeat.IsReserved = true;
                    Console.WriteLine("Seat successfully reserved!");
                }
                else
                {
                    if (discountApplied)
                    {
                        flightLogic.RefundFlyPoints(current.Id);
                        Console.WriteLine("You have cancelled your seat and flight points have been refunded.");
                    }
                    else
                    {
                        Console.WriteLine("You have cancelled your seat.");
                    }
                }

                Console.ReadLine();
            }
        }
        public static void DisplaySeatLayoutAirbus(int FlightID, int selectedRow = -1, int selectedSeat = -1)
        {
            List<Bookinghistory> seatsdatabase = Bookinghistory.GetflightHistorybyflightid(FlightID);
            foreach (Bookinghistory seatsss in seatsdatabase)
            {
                string stoel = seatsss.Seat;
                string[] stoelarray = stoel.Split('-');
                int row = Convert.ToInt32(stoelarray[0]);
                string seat = stoelarray[1].Trim();
                int seatnumber = 0;
                if (seat == "A")
                {
                    seatnumber = 0;
                }
                else if (seat == "B")
                {
                    seatnumber = 1;
                }
                else if (seat == "C")
                {
                    seatnumber = 2;
                }
                else if (seat == "D")
                {
                    seatnumber = 3;
                }
                else if (seat == "E")
                {
                    seatnumber = 4;
                }
                else if (seat == "F")
                {
                    seatnumber = 5;
                }
                else if (seat == "G")
                {
                    seatnumber = 6;
                }
                else if (seat == "H")
                {
                    seatnumber = 7;
                }
                else if (seat == "I")
                {
                    seatnumber = 8;
                }
                if (row - 1 < boeingseats.GetLength(0) && seatnumber < boeingseats.GetLength(1))
                {
                    boeingseats[row - 1, seatnumber].IsReserved = true;
                }
            }

            Console.WriteLine("If you select a seat, you have a max baggage limit of 20 kg. If you have more, you have to pay extra.");
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

            Console.WriteLine("use your arrow keys to select a seat. Press enter to reserve the seat.");
            Console.WriteLine("\nSeat Summary:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("the Yellow seats are business and cost €200.");
            Console.ResetColor(); // Reset the color to the default
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"the Blue seats are Economy class seats with the price €100.");
            Console.ResetColor();
            Console.WriteLine($"the White seats are Economy class seats with the price €100.");
            Console.ResetColor();
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
