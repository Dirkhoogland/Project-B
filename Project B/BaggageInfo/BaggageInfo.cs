class BaggageInfo
{
    static void BaggageUI(){
        Console.WriteLine("The weight limit for baggage is 20kg. (Exceeding this limit results in an additional fee.)");
        Console.WriteLine("Enter the weight of your baggage:");
        int baggageWeight = int.Parse(Console.ReadLine());
        bool limitChecker = CheckWeight(baggageWeight);

        if(limitChecker)
        {
            Console.WriteLine("No additional fees will be charged.");
        }

        else
        {
            //AddFee()
            Console.WriteLine("Baggage exceeded limit, additional fees have been added to your ticket.");
        }

        Console.WriteLine("Would you like to inform us of medical information regarding your baggage?\n1. Yes\n 2. No");
        string userChoice = Console.ReadLine();
        if(userChoice == "2" || userChoice.ToLower() == "no")
        {
            Console.WriteLine("Your baggage has been registered.");
        }

        else
        {
            Console.WriteLine("Medical information: ");
            string medicalInfo = Console.ReadLine();
        }
    }

    static bool CheckWeight(int baggageWeight){
        if(baggageWeight < 20)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}