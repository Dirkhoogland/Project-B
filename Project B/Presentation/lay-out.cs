using Project_B.BusinessLogic;
using Project_B.DataAcces;


namespace Project_B.Presentation
{
    public class Seat
    {
        public string Class { get; set; }
        public decimal Price { get; set; }
        public bool IsReserved { get; set; }

        static Seat[,] seats = new Seat[100, 10];
        public static void ReserveSeat(int row, int seat, CurrentUser current, int flightid, string seatClass, decimal Price)
        {


            Console.WriteLine($"You have chosen this seat: {row + 1}{(char)(seat + 'A')}. Class: {seatClass}, Price: {Price}");

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

                    Price += extraCost; // Add extra cost to seat price

                    Console.WriteLine($"Your total cost is {Price} euros.");
                }
                else
                {
                    Console.WriteLine($"Your total cost is {Price} euros.");
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
                            decimal discount = Price * 0.1m;
                            Price -= discount;
                            Console.WriteLine($"10% discount applied. Your total cost is now {Price} euros.");
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
                    Console.WriteLine("No discount applied. Your total cost remains {0} euros.", Price);
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
                //checks if there are notes to be added to the ticket
                string notes = "";
                if (extraCost > 0 || extraNotes != null) {
                    notes = extraNotes + " And extra baggage of:" + extraCost + " Euro With a weight of" + extraKg;
                }
                var test = Console.ReadLine();
                // creates the ticket inside the database
                FlightLogic.Reserveseat(flightid, current.Id, seatplace, seatClass, "");
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
                    // ChooseSeatWithArrowKeys(current, flightid);
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
                //DisplaySeatLayoutBoeing737(flightid);
                Console.ReadLine();
                // DataAccess.SaveSeatSelection(row, seat, flightId, userId);  
            }
            else
            {
                Console.WriteLine("You have cancelled your seat.");
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
