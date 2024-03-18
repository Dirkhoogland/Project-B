namespace Project_B.Presentation
{
    public class lay_out
    {
        public void ToonMenu()
        {
            Console.WriteLine("Welk vliegtuig wil je kiezen?");
            Console.WriteLine("1. Boeing 747");
            Console.WriteLine("2. Boeing 737");
            Console.WriteLine("3. Airbus A330-200");

            string keuze = Console.ReadLine();

            switch (keuze)
            {
                case "1":
                    Console.WriteLine("Je hebt gekozen voor de Boeing 747.");
                    break;
                case "2":
                    Console.WriteLine("Je hebt gekozen voor de Boeing 737.");
                    break;
                case "3":
                    Console.WriteLine("Je hebt gekozen voor de Airbus A330-200.");
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
                    break;
            }
        }
    }
}