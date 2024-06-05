using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.BlackjackManagers
{
    internal static class BlackjackHumanManager
    {
        //Betting dialouge will collect a user bet restricted to min, max, and balance
        internal static void CollectBet(BlackjackPlayer blackjackPlayer, int minBet, int maxBet)
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

        internal static void DisplayOverViewDialogue(BlackjackPlayer blackjackPlayer, Card dealerCard)
        {
            Console.WriteLine($"\n{blackjackPlayer.Username}");
            Console.WriteLine($"Hand {handNumber + 1}");
            Console.WriteLine($"Your cards are...\n{GetCards(blackjackPlayer.Hands[handNumber])}");
            Console.WriteLine($"Dealer's shown card is...\n{dealerCard.CardName}\n");
        }

        private static string GetCards(BlackjackHand hand)
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
