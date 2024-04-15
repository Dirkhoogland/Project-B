// namespace Project_B.Presentation
// {
//     public class lay_out
//     {
//         static bool[,] seats = new bool[33, 6];
//         public void ToonMenu()
//         {
//             Console.WriteLine("Welkom bij het stoelreserveringssysteem voor het vliegtuig!");
//             Console.WriteLine("Beschikbare opties:");
//             Console.WriteLine("1. Reserveer een stoel");
//             Console.WriteLine("2. Bekijk de stoelindeling");
//             Console.WriteLine("3. Verlaat het programma");

//             Console.Write("Voer uw keuze in (1-3): ");
//             int choice = Convert.ToInt32(Console.ReadLine());

//             switch (choice)
//             {
//                 case 1:
//                     ReserveSeat();
//                     break;
//                 case 2:
//                     DisplaySeatLayout();
//                     break;
//                 case 3:
//                     Console.WriteLine("Bedankt voor het gebruik van het stoelreserveringssysteem. Tot ziens!");
//                     return;
//                 default:
//                     Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
//                     break;
//             }

//             Console.WriteLine();
//         }
    
//         public static void ReserveSeat()
//         {
//             Console.WriteLine("Beschikbare stoelen:");
//             DisplaySeatLayout();

//             Console.Write("Voer de rijnummer in (1-33): ");
//             int row = Convert.ToInt32(Console.ReadLine());

//             Console.Write("Voer de stoelnummer in (A-F): ");
//             char seat = Convert.ToChar(Console.ReadLine().ToUpper());

//             int rowIndex = row - 1;
//             int seatIndex = seat - 'A';

//             if (rowIndex < 0 || rowIndex >= 33 || seatIndex < 0 || seatIndex >= 6)
//             {
//                 Console.WriteLine("Ongeldige stoel. Probeer het opnieuw.");
//                 return;
//             }

//             if (seats[rowIndex, seatIndex])
//             {
//                 Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
//                 return;
//             }

//             seats[rowIndex, seatIndex] = true;
//             Console.WriteLine("Stoel succesvol gereserveerd!");
//         }

//         public static void DisplaySeatLayout()
//         {
//             Console.WriteLine("Stoelindeling:");
//             Console.WriteLine("Rij  Stoelen");
//             Console.WriteLine("     A B C  D E F");

//             for (int row = 0; row < 33; row++)
//             {
//                 Console.Write($"{row + 1}    ");

//                 for (int seat = 0; seat < 6; seat++)
//                 {
//                     if (seats[row, seat])
//                     {
//                         Console.Write("X");
//                     }
//                     else
//                     {
//                         Console.Write("O");
//                     }

//                     if (seat == 2) 
//                     {
//                         Console.Write("  "); 
//                     }
//                     else
//                     {
//                         Console.Write(" ");
//                     }
//                 }

//                 Console.WriteLine();
//             }
//         }
//     }
// }

namespace Project_B.Presentation
{
    class Seat
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
                seats[i, j] = new Seat { Class = "Economy", Price = 100m, IsReserved = false };
            }
        }
    }

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

        Seat chosenSeat = seats[rowIndex, seatIndex];
        if (chosenSeat.IsReserved)
        {
            Console.WriteLine("Deze stoel is al gereserveerd. Kies een andere stoel.");
            return;
        }

        Console.WriteLine($"Je hebt deze stoel gekozen: {row}{seat}. Klasse: {chosenSeat.Class}, Prijs: {chosenSeat.Price}");
        Console.Write("Wil je deze stoel selecteren? (Ja/Nee): ");
        string confirmation = Console.ReadLine().ToLower();

        if (confirmation == "ja")
        {
            chosenSeat.IsReserved = true;
            Console.WriteLine("Stoel succesvol gereserveerd!");
        }
        else
        {
            Console.WriteLine("je hebt je stoel gecanceled.");
        }
    }

    public static void DisplaySeatLayout()
    {
    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Stoelindeling:");
    Console.WriteLine("Rij  Stoelen");
    Console.WriteLine("     A B C  D E F");

    for (int row = 0; row < 33; row++)
    {
        Console.Write($"{row + 1}    ");

        for (int seat = 0; seat < 6; seat++)
        {
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
        }

        Console.WriteLine();
    }

    Console.ResetColor(); 
}
}
}

