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
    Console.WriteLine("Select game type by its number...\n");
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
            Console.WriteLine("\nEnter desired amount of opponents.");

            if (Int32.TryParse(Console.ReadLine(), out playerCount))
            {
                Console.WriteLine("\nEnter desired amount of human oponnents");
                if (Int32.TryParse(Console.ReadLine(), out humanCount) && humanCount <= playerCount)
                {
                    Console.WriteLine("\nEnter desired starting balance");
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
    blackjackGameManager.BlackjackGame.StartRound();
    while (blackjackGameManager.BlackjackGame.Players.Any(p => p.Status == PlayerStatus.Active))
    {

        foreach (BlackjackPlayer blackjackPlayer in blackjackGameManager.BlackjackGame.Players)
        {
            if (blackjackPlayer.IsHuman)
            {
                //This needs better managment
                while (blackjackPlayer.Status == PlayerStatus.Active)
                {
                    Console.WriteLine($"\nHello {blackjackPlayer.Username}");
                    Console.WriteLine($"Your cards are...\n{GetCards(blackjackPlayer.Hands)}");
                    Console.WriteLine($"Dealers shown card is...\n{blackjackGameManager.BlackjackGame.dealerCard.CardName}");

                    if (!blackjackPlayer.WasSplitEvaluated)
                    {
                        bool canBeSplit = BlackjackHandManager.CheckPair(blackjackPlayer.Hands[0]);
                        blackjackPlayer.WasSplitEvaluated = true;
                        if (canBeSplit)
                        {
                            Console.WriteLine($"Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN, (4)SPLIT");
                        }
                    }
                    if (blackjackPlayer.Hands.Count == 2)
                    {
                        for (int i = 0; i < blackjackPlayer.Hands.Count; i++)
                        {
                            while (blackjackPlayer.Hands[i].Action == HandActions.Thinking)
                            {
                                Console.WriteLine($"\nHello {blackjackPlayer.Username}");
                                Console.WriteLine($"Your cards in hand {i} are...\n{GetCards(blackjackPlayer.Hands)}");
                                Console.WriteLine($"Dealers shown card is...\n{blackjackGameManager.BlackjackGame.dealerCard.CardName}");
                                Console.WriteLine($"Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN on this hand");
                            }
                        }
                    }

                    Console.WriteLine($"Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN");

                    int choice;
                    Int32.TryParse(Console.ReadLine(), out choice);
                    blackjackPlayer.Status = blackjackGameManager.PlayerTurn(choice, blackjackPlayer.Hands);
                    //Console.WriteLine($"");
                    //Console.WriteLine($"");
                    //Console.WriteLine($"");
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
            cardNames += $"{card.CardName}\n";
        }
    }
    else
    {
    }
    return cardNames;
}