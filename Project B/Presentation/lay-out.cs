namespace Project_B.Presentation
{
    public class lay_out
    {
        static bool[,] seats = new bool[33, 6];
        public void ToonMenu()
        {
            Console.WriteLine("Welkom bij het stoelreserveringssysteem voor het vliegtuig!");
            Console.WriteLine("Beschikbare opties:");
            Console.WriteLine("1. Reserveer een stoel");
            Console.WriteLine("2. Bekijk de stoelindeling");
            Console.WriteLine("3. Verlaat het programma");

            Console.Write("Voer uw keuze in (1-3): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ReserveSeat();
                    break;
                case 2:
                    DisplaySeatLayout();
                    break;
                case 3:
                    Console.WriteLine("Bedankt voor het gebruik van het stoelreserveringssysteem. Tot ziens!");
                    return;
                default:
                    Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
                    break;
            }

            Console.WriteLine();
        }
    
        public static void ReserveSeat()
        {
            Console.WriteLine("Beschikbare stoelen:");
            DisplaySeatLayout();

            Console.Write("Voer de rijnummer in (1-33): ");
            int row = Convert.ToInt32(Console.ReadLine());

            Console.Write("Voer de stoelnummer in (A-F): ");
            char seat = Convert.ToChar(Console.ReadLine().ToUpper());

            int rowIndex = row - 1;
            int seatIndex = seat - 'A';

            if (rowIndex < 0 || rowIndex >= 33 || seatIndex < 0 || seatIndex >= 6)
            {
                Console.WriteLine("Ongeldige stoel. Probeer het opnieuw.");
                return;
            }

            if (seats[rowIndex, seatIndex])
            {
                Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
                return;
            }

            seats[rowIndex, seatIndex] = true;
            Console.WriteLine("Stoel succesvol gereserveerd!");
        }

        public static void DisplaySeatLayout()
        {
            Console.WriteLine("Stoelindeling:");
            Console.WriteLine("Rij  Stoelen");
            Console.WriteLine("     A B C D E F");

            for (int row = 0; row < 33; row++)
            {
                Console.Write($"{row + 1}    ");

                for (int seat = 0; seat < 6; seat++)
                {
                    if (seats[row, seat])
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write("O ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}