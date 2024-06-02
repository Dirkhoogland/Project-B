using Project_B.BusinessLogic;
using Project_B.DataAcces;

namespace Project_B.Presentation
{
    public class AirbusSeat
    {
        public string Class { get; set; }
        public decimal Price { get; set; }
        public bool IsReserved { get; set; }

        static AirbusSeat[,] seats = new AirbusSeat[45, 6];

        public void lay_out()
        {
            for (int i = 0; i < 45; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (j == 0 || j == 5)
                    {
                        seats[i, j] = new AirbusSeat { Class = "Economy", Price = 150m, IsReserved = false };
                    }
                    else
                    {
                        seats[i, j] = new AirbusSeat { Class = "Economy", Price = 120m, IsReserved = false };
                    }
                    if (i == 20 || i == 21)
                    {
                        seats[i, j] = new AirbusSeat { Class = "Business", Price = 250m, IsReserved = false };
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
                    DisplaySeatLayoutAirbus330();
                    break;
                case 2:
                    Console.WriteLine("Thank you for using the seat reservation system. Bye!");
                    return;
                case 3:
                    int points = FlightLogic.GetFlyPoints(current.Id);
                    Console.WriteLine($"Current Fly points balance: {points}");
                    break;
                case 4:
                    if (FlightLogic.RedeemFlyPoints(current.Id))
                    {
                        Console.WriteLine("20 Fly points redeemed for a 10% discount on your next booking.");
                    }
                    else
                    {
                        Console.WriteLine("Not enough Fly points to redeem. Minimum 20 points required.");
                    }
                    int newPoints = FlightLogic.GetFlyPoints(current.Id);
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
                Console.SetCursorPosition(0, 0);
                DisplaySeatLayoutAirbus330(row, seat);

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        row = Math.Max(0, row - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        row = Math.Min(44, row + 1);
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
            AirbusSeat chosenSeat = seats[row, seat];
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
                    selectedOption = AskQuestionWithMenu(options);

                    if (selectedOption == "Extra Notes (Allergies, Wheelchair, etc.)")
                    {
                        if (!string.IsNullOrEmpty(extraNotes))
                        {
                            Console.WriteLine($"Your previous notes were: {extraNotes}");
                        }

                        Console.WriteLine("Please enter your extra notes:");
                        extraNotes = Console.ReadLine();
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
<<<<<<< Updated upstream
                int extraKg = 0;
                int extraCost = 0;
=======

>>>>>>> Stashed changes
                if (baggageResponse.ToLower() == "yes")
                {
                    while (true)
                    {
                        Console.Write("How many kg do you want extra: ");
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out extraKg) && extraKg >= 0)
                        {
                            break; // Exit the loop if the input is a valid number and not negative
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                    }

<<<<<<< Updated upstream
                    extraCost = extraKg * 4; // 4 euros per extra kg

                    // Confirmation step
                    string[] confirmationOptions = { "Yes", "No" };
                    int confirmationIndex = 0;
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"The extra cost for baggage is {extraCost} euros. Total cost: {chosenSeat.Price + extraCost} euros."); 
                        Console.WriteLine("Do you really want to purchase with the extra baggage cost?");

                        for (int i = 0; i < confirmationOptions.Length; i++)
                        {
                            if (i == confirmationIndex)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine(confirmationOptions[i]);

                            Console.ResetColor();
                        }

                        ConsoleKeyInfo confirmationKeyInfo = Console.ReadKey();

                        switch (confirmationKeyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (confirmationIndex > 0)
                                {
                                    confirmationIndex--;
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                if (confirmationIndex < confirmationOptions.Length - 1)
                                {
                                    confirmationIndex++;
                                }
                                break;
                            case ConsoleKey.Enter:
                                if (confirmationOptions[confirmationIndex] == "Yes")
                                {
                                    chosenSeat.Price += extraCost; // Add extra cost to seat price only if 'Yes' is selected
                                    goto EndConfirmation;
                                }

                                else if (confirmationOptions[confirmationIndex] == "No")
                                {
                                    Console.WriteLine("You have cancelled your seat.");
                                    return;
                                }
                                break;
                        }

                        Console.Clear();
                    }

                EndConfirmation:

                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
=======
                    Console.WriteLine($"The extra cost for baggage is {extraCost} euros.");

                    chosenSeat.Price += extraCost; // Add extra cost to seat price

                    Console.WriteLine($"Your total cost before discount is {chosenSeat.Price} euros.");

                    Console.WriteLine("Do you want to apply your discount now? (yes/no)");
                    string applyDiscountResponse = Console.ReadLine();

                    if (applyDiscountResponse.ToLower() == "yes")
                    {
                        FlightLogic flightLogic = new FlightLogic();
                        if (flightLogic.CanRedeemFlyPoints(current.Id))
                        {
                            flightLogic.RedeemFlyPoints(current.Id);
                            decimal discountAmount = chosenSeat.Price * 0.1m; // 10% discount
                            chosenSeat.Price -= discountAmount;
                            Console.WriteLine($"Discount applied! You saved {discountAmount} euros.");
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough Fly points to apply the discount.");
                        }
                    }

                    Console.WriteLine($"Your total cost after discount (if any) is {chosenSeat.Price} euros.");
>>>>>>> Stashed changes
                }
                else
                {
                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
                }
<<<<<<< Updated upstream
                // counts where the seat is in the plane with numbers that customers understand
                string seatplace = "";
                int newseat = seat + 1;
                if (newseat == 1)
=======

                Console.WriteLine("Do you want to reserve this seat? (yes/no)");
                string reserveSeatResponse = Console.ReadLine();

                if (reserveSeatResponse.ToLower() == "yes")
>>>>>>> Stashed changes
                {
                    string seatplace = $"{row + 1}{(char)(seat + 'A')}";
                    FlightLogic.Reserveseat(flightid, current.Id, seatplace, chosenSeat.Class, extranotes);
                    chosenSeat.IsReserved = true;
                    Console.WriteLine("Seat successfully reserved!");
                }
                else
                {
                    Console.WriteLine("Seat reservation cancelled.");
                }
<<<<<<< Updated upstream
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
                //checks if there are notes to be added to the ticket
                string notes = "";
                if (extraCost > 0 || extraNotes != null)
                {
                    notes = extraNotes + " And extra baggage of:" + extraCost + " Euro With a weight of" + extraKg;
                }
                // creates the ticket inside the database
                FlightLogic.Reserveseat(flightId, current.Id, seatplace, chosenSeat.Class, notes);
                chosenSeat.IsReserved = true;
                Console.WriteLine("Seat succesfully reserved!");
=======

>>>>>>> Stashed changes
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You have cancelled your seat selection.");
            }
        }

        public static void DisplaySeatLayoutAirbus330(int selectedRow = -1, int selectedSeat = -1)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("If you select a seat, you have a max baggage limit of 20 kg. If you have more, you have to pay extra.");
            Console.WriteLine("Seat plan:");
            Console.WriteLine("Seat   row");
            Console.WriteLine("      A B C  D E F");

            for (int row = 0; row < 45; row++)
            {
                if (row < 9)
                {
                    Console.Write(" ");
                }

                if (row == 20 || row == 21)
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

                    if (row == 20 || row == 21)
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

            while (true)
            {
                Console.Clear();

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

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (currentOption > 0)
                    {
                        currentOption--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (currentOption < options.Length - 1)
                    {
                        currentOption++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return options[currentOption];
                }
            }
        }
    }
}
