using CardGame.BL.BlackjackManagers;
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGameConsole.UI
{
    internal class PlayBlackjack
    {
        private BlackjackGameManager gameManager;
        public PlayBlackjack(Guid gameId, int playerCount, int humanCount, int startingBalance)
        {
            this.gameManager = new BlackjackGameManager(gameId, playerCount, humanCount, startingBalance);
        }

        internal void PlayGame()
        {
            try
            {
                while (gameManager.BlackjackGame.Players.Any(p => !p.IsDealer))
                {
                    gameManager.BlackjackGame.StartRound();
                    gameManager.ProcessAI();
                    gameManager.ManagePayouts();

                    List<string> results = BlackjackPlayerManager.GetPlayerResults(gameManager.BlackjackGame.Players);

                    foreach (string result in results)
                    {
                        Console.WriteLine(result);
                    }

                    gameManager.ResetGame();
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
        //        else if (blackjackPlayer.WasSplitEvaluated)
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

        //        blackjackPlayer.Status = gameManager.PlayerTurn(choice, blackjackPlayer.Hands);
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
            Console.WriteLine($"Dealer's shown card is...\n{gameManager.BlackjackGame.dealerCard.CardName}\n");
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

        //                    _ = gameManager.PlayerTurn(choice, blackjackHands);
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

        //        blackjackPlayer.Status = gameManager.PlayerTurn(choice, blackjackPlayer.Hands);
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
            Console.WriteLine($"Enter Bet (Minimum of {gameManager.BlackjackGame.minBet} and maximum of {gameManager.BlackjackGame.maxBet}).");

            while (true)
            {
                //Some input validtion ensuring bet is an int and so fourth
                if (!Int32.TryParse(Console.ReadLine(), out playerBet))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                if (playerBet < gameManager.BlackjackGame.minBet)
                {
                    Console.WriteLine("Bet cannot be smaller than minimum allowed bet.");
                }
                else if (playerBet > gameManager.BlackjackGame.maxBet)
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
            foreach (Card card in hand.Cards)
            {
                cardNames += $"{card.CardName}\n";
            }
            return cardNames;
        }
    }
}
