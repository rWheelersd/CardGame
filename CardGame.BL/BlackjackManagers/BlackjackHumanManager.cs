using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackjackManagers
{
    internal class BlackjackHumanManager
    {
        Deck<BlackjackHand> Deck;
        Card dealerCard;
        public BlackjackHumanManager(Card dealerCard, Deck<BlackjackHand> Deck) { this.dealerCard = dealerCard; this.Deck = Deck; }

        //Betting dialouge will collect a user bet restricted to min, max, and balance
        internal void CollectBet(BlackjackPlayer blackjackPlayer, int minBet, int maxBet)
        {
            int playerBet;

            Console.WriteLine($"\n{blackjackPlayer.Username}");
            Console.WriteLine($"Your balance is {blackjackPlayer.Balance}");
            Console.WriteLine($"Enter Bet (Minimum of {minBet} and maximum of {maxBet}).");

            while (true)
            {
                //Some input validtion ensuring bet is an int and so fourth
                if (!Int32.TryParse(Console.ReadLine(), out playerBet))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                if (playerBet < minBet)
                {
                    Console.WriteLine("Bet cannot be smaller than minimum allowed bet.");
                }
                else if (playerBet > maxBet)
                {
                    Console.WriteLine("Bet cannot be larger than maximum allowed bet.");
                }
                else if (playerBet > blackjackPlayer.Balance)
                {
                    Console.WriteLine("Bet cannot exceed your balance.");
                }
                else
                {
                    break;
                }
            }

            blackjackPlayer.UpdateBet(playerBet);
        }

        internal HandActions PlayChoice(int option, List<BlackjackHand> blackjackHands, int handIndex)
        {
            //Gets initial hand value, i dont like how this is done. Return to figure out a better way to do this later
            if (blackjackHands[0].HardValue == 0)
            {
                BlackjackHandManager.GetHandValues(blackjackHands[handIndex]);
            }

            switch (option)
            {
                case 1: //HIT
                    Deck.DealCards(blackjackHands[handIndex]);
                    BlackjackHandManager.GetHandValues(blackjackHands[handIndex]);
                    return HandActions.Thinking;

                case 2: //Stand
                    blackjackHands[handIndex].SetAction(HandActions.Stand);
                    return HandActions.Stand;

                case 3: //Double Down
                    Deck.DealCards(blackjackHands[handIndex]);
                    blackjackHands[handIndex].SetAction(HandActions.Stand);
                    BlackjackHandManager.GetHandValues(blackjackHands[handIndex]);
                    return HandActions.Stand;

                case 4: //Split
                    BlackjackHandManager.SplitHand(blackjackHands, blackjackHands[handIndex]);
                    return HandActions.Split;

                default: throw new ArgumentOutOfRangeException(nameof(option));
            }
        }

        internal void PlayTurn(BlackjackPlayer blackjackPlayer)
        {
            int choice = 0;

            bool allHandsSplitEvaluated = false;
            bool allHandsResolved = false;

            List<BlackjackHand> blackjackHands = blackjackPlayer.Hands;

            while (!allHandsSplitEvaluated || !allHandsResolved)
            {
                for (int i = 0; i < blackjackPlayer.Hands.Count; i++)
                {
                    if (blackjackPlayer.Hands[i].Cards.Count == 1)
                    {
                        Deck.DealCards(blackjackHands[i]);
                    }

                    while (!blackjackHands[i].IsHandResolved())
                    {
                        DisplayOverViewDialogue(blackjackPlayer, dealerCard, i);
                        choice = DisplayOptionsDialogue(blackjackHands[i]);

                        if (PlayChoice(choice, blackjackHands, i) == HandActions.Split)
                        {
                            break;
                        }
                    }
                }

                allHandsSplitEvaluated = blackjackPlayer.Hands.All(h => h.WasSplitEvaluated);
                allHandsResolved = blackjackPlayer.Hands.All(h => h.IsHandResolved());
            }

            blackjackPlayer.SetPlayerStatus(PlayerStatus.Inactive);



            //foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
            //{
            //    while (!blackjackHand.IsHandResolved())
            //    {
            //        DisplayOptionsDialogue(blackjackHand);
            //    }
            //}

            //if (blackjackPlayer.WasSplitEvaluated)
            //{
            //    int handNumber = 0;
            //    foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
            //    {
            //        while (!HandConditionActive(blackjackHand.Action))
            //        {
            //            DisplayOverViewDialogue(blackjackPlayer.Username, blackjackHand, handNumber);
            //            choice = DisplayOptionsDialogue(blackjackPlayer);
            //        }
            //        handNumber++;
            //    }
            //}

            //blackjackPlayer.Status = blackjackGameManager.PlayerTurn(choice, blackjackPlayer.Hands);
        }

        internal int DisplayOptionsDialogue(BlackjackHand blackjackHand)
        {
            //Used for validation and maxOption is used to restrict user option entry to follow game rules
            bool isValidInput = false;
            int selectedOption = 0;
            int maxOption = 2;

            //If the player wasnt split evaluated, assume this is the players first action
            //There for evaluate if it can be split, if it can be show option to split, if not show regular first move options
            if (!blackjackHand.WasSplitEvaluated)
            {
                blackjackHand.SetSplitEvaluation(true);
                if (BlackjackHandManager.CheckPair(blackjackHand))
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
                //If player has played their first turn and have reason to be here, meaning they have split
                //or hit and have not busted, the only two options that should be available are displayed
                Console.WriteLine("Do you wish to (1)HIT, (2)STAND");
                maxOption = 2;
            }

            while (!isValidInput)
            {
                //Collects and stores user choice input
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out selectedOption))
                {
                    //If the selected option is between 1 and the allowed options, then option is valid and loop will exit
                    isValidInput = selectedOption >= 1 && selectedOption <= maxOption;
                }

                if (!isValidInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }

            return selectedOption;
        }

        internal static void DisplayOverViewDialogue(BlackjackPlayer blackjackPlayer, Card dealerCard, int handIndex)
        {
            Console.WriteLine($"\n{blackjackPlayer.Username}");
            Console.WriteLine($"Hand {handIndex + 1}");
            Console.WriteLine($"Your cards are...\n{GetCards(blackjackPlayer.Hands[handIndex])}");
            Console.WriteLine($"Dealer's shown card is...\n{dealerCard.CardName}\n");
        }

        internal static string GetCards(BlackjackHand hand)
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
