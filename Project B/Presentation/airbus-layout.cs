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
        string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart" };

        ConsoleKeyInfo key;
        Console.Clear();

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
                DisplaySeatLayoutAirbus();
                break;
            case 2:
                Console.WriteLine("Thank you for using the seat reservation system. Bye!");
                return;
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
                DisplaySeatLayoutAirbus(row, seat);

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
                Console.WriteLine("This seat has already been reserved. Choose another seat..");
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
                    else if (selectedOption == "Book a retour flight")
                    {
                        string[] retourOptions = { "yes", "no" };
                        Console.Clear();
                        Console.WriteLine("Would you like to book a retour flight?");
                        
                        string retourResponse = Seat.AskQuestionWithMenu(retourOptions);

                        if (retourResponse == "yes")
                        {
                            retourstatus = true;
                            // Double the price if the user chooses a retour flight
                            chosenSeat.Price *= 2;
                            // Change user options to be able to cancel retour status
                            options = new string[] {"Cancel retour status", "Extra Notes (Allergies, Wheelchair, etc.)", "Continue" };
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
                    // If the user wants to cancel retour status after selecting it
                    else if (selectedOption == "Cancel retour status")
                    {
                        Console.Clear();
                        retourstatus = false;
                        // Change price back to original value
                        chosenSeat.Price /= 2;
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
                if (extraCost > 0 || extraNotes != null)
                {
                    notes = extraNotes + " And extra baggage of:" + extraCost + " Euro With a weight of" + extraKg;
                }
                // creates the ticket inside the database
                FlightLogic.Reserveseat(flightId, current.Id, seatplace, chosenSeat.Class, retourstatus, notes);
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
                    if ((row >= 43 && row <= 48) && (seat == 2 || seat == 6)) // If the current row is one of the rows 44-48 and the seat is the 3rd or 7th, skip the iteration
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
        }
    }
}