using Project_B.BusinessLogic;
using Project_B.DataAcces;
using System.Threading;

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
    for (int i = 0; i < 50; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            airbusseats[i, j] = new AirbusSeat { Class = "Business", Price = 200m, IsReserved = false };
            
            if (i < 2) // If the current row is one of the first two rows, set the class to "Club Class"
            {
                airbusseats[i, j] = new AirbusSeat { Class = "Club Class", Price = 200m, IsReserved = false };
            }
            else if (i == 3 || i == 13 || i == 35) // If the current row is the 4th, 14th, or 36th row, set the class to "Seat with Extra Space"
            {
                airbusseats[i, j] = new AirbusSeat { Class = "Seat with Extra Space", Price = 200m, IsReserved = false };
            }
            else if (i >= 4 && i <= 8) // If the current row is one of the rows 5-9, set the class to "Deluxe"
            {
                airbusseats[i, j] = new AirbusSeat { Class = "Deluxe", Price = 200m, IsReserved = false };
            }
            if (i >= 43 && i <= 48 && (j <= 1 || j >= 7)) // If the current row is one of the rows 44-49 and the seat is one of the left 2 or right 2, set the class to "Duo Combo Seat"
            {
                airbusseats[i, j] = new AirbusSeat { Class = "Duo Combo Seat", Price = 200m, IsReserved = false };
            }
            
        }
    }
    }
    public void ToonMenu(CurrentUser current, int flightid)
    {
        int currentOption = 0;
        string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart", "Show Fly Points" };

        ConsoleKeyInfo key;
        Console.Clear();
        
        FlightLogic flightLogic = new FlightLogic();
        
        do
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Welcome to the Airbus seat reservation system!");
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
                    int points = FlightLogic.GetFlyPoints(current.Id);
                    Console.WriteLine($"Current Fly points balance: {points}");
                    Console.ReadLine();
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
                DisplaySeatLayoutAirbus(flightid, row, seat);

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        row = Math.Max(0, row - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        row = Math.Min(50, row + 1);
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

        public static void ReserveSeat(int row, int seat, CurrentUser current, int flightId)
        {
            AirbusSeat chosenSeat = airbusseats[row, seat];
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
                
               Console.WriteLine("If you select a seat, you have a max baggage limit of 20 kg. If you have more, you have to pay extra.");

                string[] baggageOptions = { "yes", "no" };
                int selectedIndex = 0;
                string baggageResponse = string.Empty;

                Console.WriteLine("Do you want more baggage? (20 kg is included in the price)");

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
                int extraCost = 0;
                int extraKg = 0;
                if (baggageResponse.ToLower() == "yes")
                {
                    Console.Write("How many kg do you want extra: ");
                     extraKg = Convert.ToInt32(Console.ReadLine());
                     extraCost = extraKg * 4; // 4 euros per extra kg

                    Console.WriteLine($"The extra cost for baggage is {extraCost} euros."); 

                    chosenSeat.Price += extraCost; // Add extra cost to seat price

                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
                }
                else
                {
                    Console.WriteLine($"Your total cost is {chosenSeat.Price} euros.");
                }
                // counts where the seat is in the plane with numbers that customers understand
                Console.Clear();
                Console.WriteLine("Do you want to apply your fly points for a discount?");

                currentOption = 0;

                // Print the options once
                for (int i = 0; i < yesNoOptions.Length; i++)
                {
                    Console.WriteLine(yesNoOptions[i]);
                }

                do
                {
                    int cursorTop = Console.CursorTop - yesNoOptions.Length; // Set cursorTop to the current cursor position
                    Console.SetCursorPosition(0, cursorTop);
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

                int remainingPoints = FlightLogic.GetFlyPoints(current.Id);
                Console.WriteLine($"Your remaining fly points balance is {remainingPoints}.");

                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();

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
                var test = Console.ReadLine();
                // creates the ticket inside the database
                FlightLogic.Reserveseat(flightId, current.Id, seatplace, chosenSeat.Class, "");
                Console.WriteLine("Do you want to buy one more seat?");
                string[] yesNoOptions2 = new string[] { "yes", "no" };
                int currentOption2 = 0;
                ConsoleKeyInfo key2;
                do
                {
                    Console.Clear();
                    Console.WriteLine("do you want to buy one more seat?");
                    for (int i = 0; i < yesNoOptions2.Length; i++)
                    {
                        if (i == currentOption2)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(yesNoOptions2[i]);

                        Console.ResetColor();
                    }

                    key2 = Console.ReadKey(true);
                    switch (key2.Key)
                    {
                        case ConsoleKey.UpArrow:
                            currentOption2 = Math.Max(0, currentOption2 - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            currentOption2 = Math.Min(yesNoOptions2.Length - 1, currentOption2 + 1);
                            break;
                    }
                } while (key2.Key != ConsoleKey.Enter);

                if (currentOption2 == 0)
                {
                    ChooseSeatWithArrowKeys(current, flightId);
                }
                else
                {
                    Console.WriteLine("Thank you for reserving a seat. Press enter to continue.");
                    return;
                }
                if (discountApplied)
                    {
                        flightLogic.RefundFlyPoints(current.Id);
                        Console.WriteLine("You have cancelled your seat and flight points have been refunded.");
                    }
                    else
                    {
                        Console.WriteLine("You have cancelled your seat.");
                    }
                Console.WriteLine("seat succesfully reserved!");
                DisplaySeatLayoutAirbus(flightId);
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
            foreach (Bookinghistory stoelen in seatsdatabase)
            {
                string airbusstoel = stoelen.Seat;
                string[] airbusstoelarray = airbusstoel.Split('-');
                int row = Convert.ToInt32(airbusstoelarray[0]);
                string seat = airbusstoelarray[1];
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
                
                airbusseats[row - 1, seatnumber].IsReserved = true;	
            }

            Console.WriteLine("If you select a seat, you have a max baggage limit of 20 kg. If you have more, you have to pay extra.");
            Console.WriteLine("Seating plan:");
            Console.WriteLine("Seats   Row");
            Console.WriteLine("      A B C  D E F  G H I");

            for (int row = 0; row < 50; row++)
            {
                if (row < 9)
                {
                    Console.Write(" ");
                }

                if (row < 2) // If the current row is one of the first two rows, change the color to gray
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                if (row == 3 || row == 13 || row == 35) // If the current row is the fourth row, change the color to magenta
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

                if (row >= 4 && row <= 8) // If the current row is one of the rows 5-9, change the color to dark magenta
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                }

                Console.Write($"{row + 1}    ");

                if (row == 2 || row == 10 || row == 11 || row == 12 || row == 33 || row == 34) // If the current row is the third row, skip the iteration
                {
                    Console.WriteLine();
                    continue;
                }

                for (int seat = 0; seat < 9; seat++) // Adjusted to 9 seats
                {
                    if (row >= 43 && row <= 48 && (seat == 2 || seat == 6)) // If the current row is one of the rows 44-48 and the seat is the 3rd or 7th, skip the iteration
                    {
                        Console.Write("  ");
                        continue;
                    }
                    if (row < 2 && (seat == 2 || seat == 7 || seat == 4)) // If the current row is one of the first two rows and the seat is the first or last, skip the iteration
                    {
                        Console.Write("  ");
                        continue;
                    }
                    if (row == 9) // If the current row is the 10th row
                    {
                        if (seat < 3 || seat > 5) // If the seat is not one of the 4th, 5th, or 6th, skip the iteration
                        {
                            Console.Write("  ");
                            continue;
                        }
                    }

                    if (row >= 43 && row <= 48) // If the current row is one of the rows 44-49
                    {
                        if (seat <= 1 || seat >= 7) // If the seat is one of the left 2 or right 2, change the color to blue
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                    }

                    if (row == 3) // If the current row is the fourth row, change the color to magenta
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    if (row >= 4 && row <= 8) // If the current row is between 5 and 9, change the color to dark magenta
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }

                    if (row == 13) // If the current row is the fourth row, change the color to magenta
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    if (row == 35) // If the current row is the fourth row, change the color to magenta
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    if (row < 2) // If the current row is either 1 or 2, change the color to gray
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    if (row == 49 && (seat < 3 || seat > 5)) // If the current row is the 50th row and the seat is one of the first 3 or last 3, skip the iteration
                    {
                        Console.Write("  ");
                        continue;
                    }

                    if (row == selectedRow && seat == selectedSeat)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    if (airbusseats[row, seat].IsReserved)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("O");
                    }

                    if (seat == 2 || seat == 5) // Add a space after the 3rd and 6th seats
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

            Console.WriteLine("use your arrow keys to select a seat. Press enter to reserve the seat.");
            Console.WriteLine("\nSeat Summary:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("the gray seats are Club Class and cost €200.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"the Pink seats are Seats with extra leg space class seats with the price €200.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"the Cyan seats are Duo combo seats with the price €200.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"the Purple seats are Deluxe seats with the price €200.");
            Console.ResetColor();
            Console.WriteLine($"the White seats are Economy seats with the price €200.");
            Console.ResetColor();
        }

        public static string AskQuestionWithMenu(string[] options)
        {
            int currentOption = 0;
            ConsoleKeyInfo key;
            int cursorTop = Console.CursorTop - options.Length;
            if (cursorTop < 0)
            {
                cursorTop = 0;
            }
            do
            {
                Console.SetCursorPosition(0, cursorTop);
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