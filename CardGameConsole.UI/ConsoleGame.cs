using CardGame.BL.BlackjackManagers;
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGameConsole.UI;
using System.Numerics;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

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
                PlayBlackjack playBlackjack = new PlayBlackjack(gameId, playerCount, humanCount, startingBalance);
                playBlackjack.PlayGame();
                gameReady = true;
                break;
            default:
                throw new Exception("There is no possible way you could've made it here... get out of my program.");
        }
    }
    else
    {
        Console.WriteLine("Please enter a valid game number.");
    }
}