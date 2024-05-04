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

//Console loops until game and conditions are specified
while (!gameReady)
{
    //Iterator count for listing games
    int i = 1;
    Console.WriteLine("Select game type by its number...\n");
    foreach (Enum gameType in Enum.GetValues(typeof(GameType)))
    {
        //Lists out gametypes from available in enumeration
        Console.WriteLine($"{i}. {gameType.ToString()}");
        i++;
    }
    if (Int32.TryParse(Console.ReadLine(), out selectedGame))
    {
        //If the entry is valid it will assign the chosen game. If there is no valid case then user will be told so and the parent loop will be reiterated
        switch (selectedGame)
        {
            case 1:
                gameChosen = "Blackjack";
                break;
            default:
                Console.WriteLine("Please enter a valid game number. (Texas Holdem is for demo purposes, not yet and option)");
                continue;
        }

        //Required parameters for a generic game to be instantiated, this might be to be more refined to accomdate more specific game types if there are more specific requirements
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
        //Chosen game will be instantiated
        switch (gameChosen)
        {
            case "Blackjack":
                blackjackGameManager = new BlackjackGameManager(gameId, playerCount, humanCount, startingBalance);
                break;
            default:
                throw new Exception("There is no possible way you could've made it here... get out of my program.");
        }
        //Loop will be killed and game will be started
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
            blackjackGameManager.PlayAITurns();
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

//void PlayPlayerTurn(BlackjackPlayer blackjackPlayer)
//{
//    int choice = 0;
//    while (blackjackPlayer.Status == PlayerStatus.Active)
//    {
//        if (!blackjackPlayer.WasSplitEvaluated)
//        {
//            DisplayOptionsDialogue(blackjackPlayer);
//        }
//        else if(blackjackPlayer.WasSplitEvaluated)
//        {
//            int handNumber = 0;
//            foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
//            {
//                while (!HandConditionActive(blackjackHand.Action))
//                {
//                    DisplayOverViewDialogue(blackjackPlayer.Username, blackjackHand, handNumber);
//                    choice = DisplayOptionsDialogue(blackjackPlayer);
//                }
//                handNumber++;
//            }
//        }

//        blackjackPlayer.Status = blackjackGameManager.PlayerTurn(choice, blackjackPlayer.Hands);
//    }
//}

//int DisplayOptionsDialogue(BlackjackPlayer blackjackPlayer)
//{
//    //Used for validation and maxOption is used to restrict user option entry to follow game rules
//    bool isValidInput = false;
//    int selectedOption = 0;
//    int maxOption = 2;

//    //If the player wasnt split evaluated, assume this is the players first action
//    //There for evaluate if it can be split, if it can be show option to split, if not show regular first move options
//    if (!blackjackPlayer.WasSplitEvaluated)
//    {
//        blackjackPlayer.WasSplitEvaluated = true;
//        if (BlackjackHandManager.CheckPair(blackjackPlayer.Hands[0]))
//        {
//            Console.WriteLine("Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN, (4)SPLIT");
//            maxOption = 4;
//        }
//        else
//        {
//            Console.WriteLine("Do you wish to (1)HIT, (2)STAND, (3)DOUBLE DOWN");
//            maxOption = 3;
//        }
//    }
//    else
//    {
//        //If player has played their first turn and have reason to be here, meaning they have split
//        //or hit and have not busted, the only two options that should be available are displayed
//        Console.WriteLine("Do you wish to (1)HIT, (2)STAND");
//        maxOption = 2;
//    }

//    while (!isValidInput)
//    {
//        //Collects and stores user choice input
//        Console.Write("Enter your choice: ");
//        string input = Console.ReadLine();
//        if (int.TryParse(input, out selectedOption))
//        {
//            //If the selected option is between 1 and the allowed options, then option is valid and loop will exit
//            isValidInput = selectedOption >= 1 && selectedOption <= maxOption;
//        }

//        if (!isValidInput)
//        {
//            Console.WriteLine("Invalid input. Please enter a valid option.");
//        }
//    }

//    return selectedOption;
//}

//Display username, hand and dealers shown card
void DisplayOverViewDialogue(string username, BlackjackHand blackjackHand, int handNumber)
{
    Console.WriteLine($"\n{username}");
    Console.WriteLine($"Hand {handNumber + 1}");
    Console.WriteLine($"Your cards are...\n{GetCards(blackjackHand)}");
    Console.WriteLine($"Dealer's shown card is...\n{blackjackGameManager.BlackjackGame.dealerCard.CardName}\n");
}

//void PlayPlayerTurn(BlackjackPlayer blackjackPlayer)
//{
//    int choice = 0;
//    while (blackjackPlayer.Status == PlayerStatus.Active)
//    {
//        if (blackjackPlayer.Hands.Count == 2)
//        {
//            //Players with split hands are dealt with here
//            int handNumber = 0;
//            foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
//            {
//                //Every hand is handled until it conditions are met
//                while (!HandConditionActive(blackjackHand.Action))
//                {
//                    Console.WriteLine($"\n--------");
//                    Console.WriteLine($"Hand {handNumber+1}");
//                    Console.WriteLine($"--------");
//                    DisplayOverViewDialogue(blackjackPlayer.Username, blackjackPlayer.Hands[handNumber]);
//                    choice = DisplayOptionsDialogue(blackjackPlayer);

//                    //a new list is instantiated and the reference to the hand in progress is added so the operation of choice can be processed on it
//                    List<BlackjackHand> blackjackHands = [blackjackHand];

//                    _ = blackjackGameManager.PlayerTurn(choice, blackjackHands);
//                }
//                handNumber++;
//            }
//            //I dont think this is needed, but its here for consistency
//            blackjackPlayer.Status = PlayerStatus.Inactive;
//        }
//        else if(blackjackPlayer.Hands.Count == 1)
//        {
//            DisplayOverViewDialogue(blackjackPlayer.Username, blackjackPlayer.Hands[0]);
//            choice = DisplayOptionsDialogue(blackjackPlayer);
//        }

//        blackjackPlayer.Status = blackjackGameManager.PlayerTurn(choice, blackjackPlayer.Hands);
//    }
//}

bool HandConditionActive(HandActions action)
{
    //I think this looks nicer than having a super loaded condition block, might use this more often
    return (action == HandActions.FlipBlackjack || action == HandActions.FlipBust || action == HandActions.Stand);
}

//Betting dialouge will collect a user bet restricted to min, max, and balance
int DisplayBettingDialouge(string username, int balance)
{
    int playerBet;

    Console.WriteLine($"\n{username}");
    Console.WriteLine($"Your balance is {balance}");
    Console.WriteLine($"Enter Bet (Minimum of {blackjackGameManager.BlackjackGame.minBet} and maximum of {blackjackGameManager.BlackjackGame.maxBet}).");

    while (true)
    {
        //Some input validtion ensuring bet is an int and so fourth
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



//Gets the user input for what they want to do with their hand


string GetCards(BlackjackHand hand)
{
    string cardNames = string.Empty; ;
    foreach(BlackjackCard card in hand.Cards) 
        {
            cardNames += $"{card.CardName}\n";
        }
    return cardNames;
}