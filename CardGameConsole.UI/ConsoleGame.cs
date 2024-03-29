using CardGame.BL.BlackJack;

BlackjackGameManager blackjackGameManager;
int playerCount;
int startingBalance;
bool gameReady = false;


while (!gameReady)
{
    Console.WriteLine("Enter desired amount of opponents");

    if (Int32.TryParse(Console.ReadLine(), out playerCount))
    {
        Console.WriteLine("Enter desired starting balance.");

        if (Int32.TryParse(Console.ReadLine(), out startingBalance))
        {
            Guid gameId = Guid.NewGuid();
            blackjackGameManager = new BlackjackGameManager(gameId, playerCount, startingBalance);
            gameReady = true;
            PlayGame();
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid number");
    }
}

static void PlayGame()
{
    Console.WriteLine("Made it here.");
}