using CardGame.BL.BlackJack;
using CardGame.BL.Models.Blackjack;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

BlackjackGameManager blackjackGameManager;

int playerCount;
int humanCount;
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
                Console.WriteLine("Enter desired amount of human oponnents");
                if (Int32.TryParse(Console.ReadLine(), out humanCount) && humanCount <= playerCount)
                {
                    if (Int32.TryParse(Console.ReadLine(), out startingBalance))
                    {
                        Guid gameId = Guid.NewGuid();

                        switch (gameChosen)
                        {
                            case "Blackjack":
                                blackjackGameManager = new BlackjackGameManager(gameId, playerCount, humanCount, startingBalance);
                                break;
                            default: throw new Exception("There is no possible way you couldve made it here...get out of my program.");
                        }

                        gameReady = true;
                        PlayGame();
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid starting balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter valid human count.");
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

void PlayGame()
{
    while (blackjackGameManager.BlackjackGame.Players.Any(p => p.Status == PlayerStatus.Active))
    {
        foreach (BlackjackPlayer blackjackPlayer in blackjackGameManager.BlackjackGame.Players)
        {
            if (blackjackPlayer.IsHuman)
            {
                //This needs better managment
                while (true)
                {
                    Console.WriteLine($"Hello {blackjackPlayer.Username}");
                    Console.WriteLine($"Your cards are {GetCards(blackjackPlayer.Hands)}");
                    //Will want to change this to hand split dialouge if split is a possible option
                    Console.WriteLine($"Do you wish to HIT, STAND, DOUBLE DOWN");
                    Console.WriteLine($"");
                    Console.WriteLine($"");
                    Console.WriteLine($"");
                }
            }
        }
    }
}

string GetCards(List<BlackjackHand> hands)
{
    string cardNames = string.Empty; ;
    if (hands.Count == 1)
    {
        foreach(BlackjackCard card in hands[0].Cards) 
        {
            cardNames += $"{card.CardName} : ";
        }
    }
    else
    {
    }
    return cardNames;
}