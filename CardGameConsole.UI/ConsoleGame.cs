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
    if (Int32.TryParse(Console.ReadLine(), out selectedGame))
    {
        switch (selectedGame)
        {
            case 1:
                gameChosen = "Blackjack";
                break;
            default:
                Console.WriteLine("Please enter a valid game number. (Texas Holdem is for demo purposes, not yet and option)");
                continue;
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
    try
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
                        blackjackPlayer.Bet = DisplayBettingDialouge(blackjackPlayer.Username, blackjackPlayer.Balance);
                    }
                    PlayPlayerTurn(blackjackPlayer);
                }
            }
            blackjackGameManager.PlayAITurn();
            blackjackGameManager.ManagePayouts();

            List<string> results = BlackjackPlayerManager.GetPlayerResults(blackjackGameManager.BlackjackGame.Players);

            foreach (string result in results)
            {
                Console.WriteLine(result);
            }

            blackjackGameManager.ResetGame();
        }
    }
    catch (Exception ex)
    {
            throw ex;
    }
}
void PlayPlayerTurn(BlackjackPlayer blackjackPlayer)
{
    int choice = 0;
    while (blackjackPlayer.Status == PlayerStatus.Active)
    {
        DisplayOverViewDialogue(blackjackPlayer);
        if (blackjackPlayer.Hands.Count == 2)
        {
            foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
            {
                while (HandConditionActive(blackjackHand.Action))
                {
                    choice = DisplayOptionsDialogue(blackjackPlayer);
                }
            }
        }
        else if(blackjackPlayer.Hands.Count == 1)
        {

            choice = DisplayOptionsDialogue(blackjackPlayer);
        }

        blackjackPlayer.Status = blackjackGameManager.PlayerTurn(choice, blackjackPlayer.Hands);
    }
}

bool HandConditionActive(HandActions action)
{
    return action == HandActions.FlipBlackjack || action == HandActions.FlipBust || action == HandActions.Stand;
}

int DisplayBettingDialouge(string username, int balance)
{
    int playerBet;

    Console.WriteLine($"\n{username}");
    Console.WriteLine($"Your balance is {balance}");
    Console.WriteLine($"Enter Bet (Minimum of {blackjackGameManager.BlackjackGame.minBet} and maximum of {blackjackGameManager.BlackjackGame.maxBet}).");

    while (true)
    {
        if (!Int32.TryParse(Console.ReadLine(), out playerBet))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            continue;
        }

        if (playerBet < blackjackGameManager.BlackjackGame.minBet)
        {
            Console.WriteLine("Bet cannot be smaller than minimum allowed bet.");
        }
        else if (playerBet > blackjackGameManager.BlackjackGame.maxBet)
        {
            Console.WriteLine("Bet cannot be larger than maximum allowed bet.");
        }
        else if (playerBet > balance)
        {
            Console.WriteLine("Bet cannot exceed your balance.");
        }
        else
        {
            break;
        }
    }

    return playerBet;
}

void DisplayOverViewDialogue(BlackjackPlayer blackjackPlayer)
{
    Console.WriteLine($"\n{blackjackPlayer.Username}");
    Console.WriteLine($"Your cards are...\n{GetCards(blackjackPlayer.Hands)}");
    Console.WriteLine($"Dealer's shown card is...\n{blackjackGameManager.BlackjackGame.dealerCard.CardName}\n");
}

int DisplayOptionsDialogue(BlackjackPlayer blackjackPlayer)
{
    bool isValidInput = false;
    int selectedOption = 0;
    int maxOption = 2;

    if (!blackjackPlayer.WasSplitEvaluated)
    {
        blackjackPlayer.WasSplitEvaluated = true;
        if (BlackjackHandManager.CheckPair(blackjackPlayer.Hands[0]))
        {
            Console.WriteLine("Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN, (4)SPLIT");
            maxOption = 4;
        }
        else
        {
            Console.WriteLine("Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN");
            maxOption = 3;
        }
    }
    else
    {
        if (blackjackPlayer.Hands.Count == 1 && blackjackPlayer.Hands[0].Cards.Count > 2)
        {
            Console.WriteLine("Do you wish to (1)HIT, (2)STAND");
            maxOption = 2;
        }
    }

    while (!isValidInput)
    {
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out selectedOption))
        {
            isValidInput = selectedOption >= 1 && selectedOption <= maxOption;
        }

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid option.");
        }
    }

    return selectedOption;
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