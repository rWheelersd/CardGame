int playerCount;

while (true)
{
    Console.WriteLine("Enter desired amount of opponents");

    if (Int32.TryParse(Console.ReadLine(), out playerCount))
    {

    }
    else
    {
        Console.WriteLine("Please enter a valid number");
    }
}