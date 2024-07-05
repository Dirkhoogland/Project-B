using Project_B.BusinessLogic;
using Project_B.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_B.Presentation
{
    internal class Boeing737 : Seat
    {

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
            string[] menuOptions = new string[] { "Reserve a seat", "View the seating chart", "Leave the seating chart", "Show Fly Points" };

            ConsoleKeyInfo key;
            Console.Clear();

            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Welcome to Boeing the seat reservation system!");
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
                    DisplaySeatLayoutBoeing737(flightid);
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
                Console.SetCursorPosition(0, 0);
                DisplaySeatLayoutBoeing737(flightid, row, seat);

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
                        ReserveSeat(row, seat, current, flightid, seats[row , seat].Class, seats[row, seat].Price );
                        return;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        public static void DisplaySeatLayoutBoeing737(int FlightID, int selectedRow = -1, int selectedSeat = -1)
        {
            List<Bookinghistory> seatsdatabase = Bookinghistory.GetflightHistorybyflightid(FlightID);
            foreach (Bookinghistory seat in seatsdatabase)
            {
                string test = seat.Seat;
                string[] parts = test.Split('-');
                int row = Convert.ToInt32(parts[0]);
                string seatss = parts[1];
                int seatnumber = 0;
                if (seatss == " A")
                {
                    seatnumber = 0;
                }
                else if (seatss == " B")
                {
                    seatnumber = 1;
                }
                else if (seatss == " C")
                {
                    seatnumber = 2;
                }
                else if (seatss == " D")
                {
                    seatnumber = 3;
                }
                else if (seatss == " E")
                {
                    seatnumber = 4;
                }
                else if (seatss == " F")
                {
                    seatnumber = 5;
                }
                seats[row - 1, seatnumber].IsReserved = true;

            }
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

            Console.WriteLine("use your arrow keys to select a seat. Press enter to reserve the seat.");
            Console.WriteLine("\nSeat Summary:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Yellow seats are Business class seats with the price €200.");
            Console.ResetColor(); // Reset the color to the default
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Cyan seats are Economy class seats with the price €100.");
            Console.ResetColor();
        }
    }
}
