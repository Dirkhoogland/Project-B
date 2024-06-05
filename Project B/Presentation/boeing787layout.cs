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
            string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart", "Show Fly points", "Redeem Fly points" };

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
                            row = Math.Min(38, row + 1);
                            break;
                        case ConsoleKey.LeftArrow:
                            seat = Math.Max(0, seat - 1);
                            break;
                        case ConsoleKey.RightArrow:
                            seat = Math.Min(8, seat + 1);
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
                    bool retourstatus = false;
                    string[] options = { "Book a retour flight", "Extra Notes (Allergies, Wheelchair, etc.)", "Continue" };
                    string selectedOption;
                    string extraNotes = string.Empty;

                    do
                    {
                        selectedOption = BoeingSeat.AskQuestionWithMenu(options);

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
                        else if (selectedOption == "Book a retour flight")
                        {
                            string[] retourOptions = { "yes", "no" };
                            Console.Clear();
                            Console.WriteLine("Would you like to book a retour flight?");

                            string retourResponse = BoeingSeat.AskQuestionWithMenu(retourOptions);

                            if (retourResponse == "yes")
                            {
                                retourstatus = true;
                                // Double the price if the user chooses a retour flight
                                chosenSeat.Price *= 2;
                                // Change user options to be able to cancel retour status
                                options = new string[] { "Cancel retour status", "Extra Notes (Allergies, Wheelchair, etc.)", "Continue" };
                                Console.WriteLine("Your ticket has been marked as a retour flight.");
                                Thread.Sleep(1000);
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                            }
                            else if (retourResponse == "no")
                            {
                                Console.WriteLine("You have chosen not to book a retour flight.");
                                Thread.Sleep(1000);
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                            }
                        }
                        else if (selectedOption == "Cancel retour status")
                        {
                            Console.Clear();
                            retourstatus = false;
                            options = new string[] { "Book a retour flight", "Extra Notes (Allergies, Wheelchair, etc.)", "Continue" };
                            Console.WriteLine("Your flight has been marked as a one-way ticket.");
                            Thread.Sleep(1000);
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
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
                    int extraKg = 0;
                    int extraCost = 0;
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

                        if (retourstatus is false)
                        {
                            extraCost = extraKg * 4; // 4 euros per extra kg
                        }
                        else
                        {
                            extraCost = extraKg * 4 * 2; // doubles price of extra baggage if retour flight
                        }

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
                    }
                    else
                    {
                        Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
                    }
                    // counts where the seat is in the plane with numbers that customers understand
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
                    //checks if there are notes to be added to the ticket
                    string notes = "";
                    if (extraCost > 0 || extraNotes != null) {
                        notes = extraNotes + " And extra baggage of:" + extraCost + " Euro With a weight of" + extraKg;
                    }
                    // creates the ticket inside the database

                    FlightLogic.Reserveseat(flightid, current.Id, seatplace, chosenSeat.Class, retourstatus, notes);
                    chosenSeat.IsReserved = true;
                    Console.WriteLine("seat succesfully reserved!");
                    DisplaySeatLayoutAirbus(flightid);
                    Console.ReadLine();
                    // DataAccess.SaveSeatSelection(row, seat, flightId, userId);  
                }
                else
                {
                    Console.WriteLine("You have cancelled your seat.");
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
                    string seat = stoelarray[1];
                    int seatnumber = 0;
                    if (seat == " A")
                    {
                        seatnumber = 0;
                    }
                    else if (seat == " B")
                    {
                        seatnumber = 1;
                    }
                    else if (seat == " C")
                    {
                        seatnumber = 2;
                    }
                    else if (seat == " D")
                    {
                        seatnumber = 3;
                    }
                    else if (seat == " E")
                    {
                        seatnumber = 4;
                    }
                    else if (seat == " F")
                    {
                        seatnumber = 5;
                    }
                    else if (seat == " G")
                    {
                        seatnumber = 6;
                    }
                    else if (seat == " H")
                    {
                        seatnumber = 7;
                    }
                    else if (seat == " I")
                    {
                        seatnumber = 8;
                    }
                    boeingseats[row - 1, seatnumber].IsReserved = true;
                }


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



