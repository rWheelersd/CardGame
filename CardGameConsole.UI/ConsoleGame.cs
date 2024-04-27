using CardGame.BL.BlackJack;
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System.Numerics;
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
    //112
    if (Int32.TryParse(Console.ReadLine(), out selectedGame))
    {
        switch (selectedGame)
        {
            case 1:
                gameChosen = "Blackjack";
                break;
            default:
                Console.WriteLine("Please enter a valid game number. (Texas Holdem is for demo purposes, not yet and option)");
                continue; // Ask for game selection again
        }

        Console.WriteLine("\nEnter desired amount of opponents (Up to 9).");
        while (!Int32.TryParse(Console.ReadLine(), out playerCount) || playerCount > 9)
        {
            Console.WriteLine("Please enter a valid number for player count up to 9 players.");
        }

        Console.WriteLine("\nEnter desired amount of human opponents.");
        while (!Int32.TryParse(Console.ReadLine(), out humanCount) || humanCount > playerCount)
        {
            Console.WriteLine($"Please enter a valid number for human count (must be less than or equal to {playerCount}).");
        }

        Console.WriteLine("\nEnter desired starting balance no more than 10000.");
        while (!Int32.TryParse(Console.ReadLine(), out startingBalance) || startingBalance > 10000)
        {
            Console.WriteLine("Please enter a valid starting balance (no more than 10000).");
        }

        Guid gameId = Guid.NewGuid();

        switch (gameChosen)
        {
            case "Blackjack":
                blackjackGameManager = new BlackjackGameManager(gameId, playerCount, humanCount, startingBalance);
                break;
            default:
                throw new Exception("There is no possible way you could've made it here... get out of my program.");
        }

        gameReady = true;
        PlayGame();
    }
    else
    {
        Console.WriteLine("Please enter a valid game number.");
    }
}

void PlayGame()
{
    while (blackjackGameManager.BlackjackGame.Players.Any(p => !p.IsDealer))
    {
        blackjackGameManager.BlackjackGame.StartRound();
        foreach (BlackjackPlayer blackjackPlayer in blackjackGameManager.BlackjackGame.Players)
        {
            if (blackjackPlayer.IsHuman)
            {
                if (blackjackPlayer.Bet == 0)
                {
                    int playerBet = 0;
                    Console.WriteLine($"\nEnter Bet (Minimum of {blackjackGameManager.BlackjackGame.minBet} and maximum of {blackjackGameManager.BlackjackGame.maxBet}.");
                    while (!Int32.TryParse(Console.ReadLine(), out playerBet) 
                           || playerBet < blackjackGameManager.BlackjackGame.minBet
                           || playerBet > blackjackGameManager.BlackjackGame.maxBet
                           || playerBet > blackjackPlayer.Balance)
                    {
                        if (playerBet < blackjackGameManager.BlackjackGame.minBet)
                        {
                            Console.WriteLine("Bet cannot be smaller than minimum allowed bet, try again...");
                        }
                        if (playerBet > blackjackGameManager.BlackjackGame.maxBet)
                        {
                            Console.WriteLine("Bet cannot be larger than maximum allowed bet, try again...");
                        }
                        if (playerBet > blackjackPlayer.Balance)
                        {
                            Console.WriteLine("Bet cannot exceed your balance, try again...");
                        }
                        Console.WriteLine($"\nEnter Bet (Minimum of {blackjackGameManager.BlackjackGame.minBet} and maximum of {blackjackGameManager.BlackjackGame.maxBet}.");
                    }
                    blackjackPlayer.Bet = playerBet;
                }
                PlayPlayerTurn(blackjackPlayer);
            }
        }

        blackjackGameManager.PlayAITurn();
        blackjackGameManager.ManagePayouts();
        blackjackGameManager.ResetPlayers();
    }
}
void PlayPlayerTurn(BlackjackPlayer blackjackPlayer)
{
    while (blackjackPlayer.Status == PlayerStatus.Active)
    {
        if (blackjackPlayer.Hands.Count == 2)
        {
            foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
            {
                while (blackjackHand.Action == HandActions.Thinking)
                {
                    DisplayDialogue(blackjackPlayer, blackjackHand);
                }
            }
        }
        else
        {
            DisplayDialogue(blackjackPlayer, blackjackPlayer.Hands[0]);
        }

        int choice;
        Int32.TryParse(Console.ReadLine(), out choice);
        blackjackPlayer.Status = blackjackGameManager.PlayerTurn(choice, blackjackPlayer.Hands);
    }
}

void DisplayDialogue(BlackjackPlayer blackjackPlayer, BlackjackHand blackjackHand)
{
    Console.WriteLine($"\n{blackjackPlayer.Username}");
    Console.WriteLine($"Your balance is {blackjackPlayer.Balance}");
    Console.WriteLine($"Your cards are...\n{GetCards(blackjackPlayer.Hands)}");
    Console.WriteLine($"Dealer's shown card is...\n{blackjackGameManager.BlackjackGame.dealerCard.CardName}");

    if (!blackjackPlayer.WasSplitEvaluated)
    {
        blackjackPlayer.WasSplitEvaluated = true;
        Console.WriteLine(BlackjackHandManager.CheckPair(blackjackPlayer.Hands[0]) ?
            $"Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN, (4)SPLIT"
            :
            $"Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN");
    }
    else
    {
        Console.WriteLine($"Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN");
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

void GetPlayerResults(BlackjackPlayer player)
{
    
}