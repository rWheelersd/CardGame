using CardGame.BL.BlackJack;
using static CardGame.BL.Models.Constants.BaseConstants;

BlackjackGameManager blackjackGameManager;

int playerCount;
int startingBalance;
int selectedGame;
string gameChosen = string.Empty;
bool gameReady = false;


while (!gameReady)
{
    int i = 1;
    Console.WriteLine("Select game type by its number...");
    foreach (Enum gameType in Enum.GetValues(typeof(GameType)))
    {
        Console.WriteLine($"{i}. {gameType.ToString()}");
        i++;
    }

    if (Int32.TryParse(Console.ReadLine(), out selectedGame))
    {
        switch (selectedGame)
        {
            case 1:
                gameChosen = "Blackjack";
                break;
            default: 
                break;
        }

        if (!string.IsNullOrEmpty(gameChosen))
        {
            Console.WriteLine("Enter desired amount of opponents.");

            if (Int32.TryParse(Console.ReadLine(), out playerCount))
            {
                Console.WriteLine("Enter desired starting balance.");

                if (Int32.TryParse(Console.ReadLine(), out startingBalance))
                {
                    Guid gameId = Guid.NewGuid();

                    switch (gameChosen)
                    {
                        case "Blackjack":
                            blackjackGameManager = new BlackjackGameManager(gameId, playerCount, startingBalance);
                            break;
                        default: throw new Exception("There is no possible way you couldve made it here...get out of my program.");
                    }

                    gameReady = true;
                    PlayGame();
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid game number");
    }

    
}

static void PlayGame()
{
    Console.WriteLine("Made it here.");
}