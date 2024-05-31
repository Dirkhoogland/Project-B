class BaggageInfo
{
    static void BaggageUI(){
        Console.WriteLine("The weight limit for baggage is 20kg. (Exceeding this limit results in an additional fee.)");
        Console.WriteLine("Enter the weight of your baggage:");
        int baggageWeight = int.Parse(Console.ReadLine());
    }

    
}