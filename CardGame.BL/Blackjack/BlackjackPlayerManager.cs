using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackJack
{
    public static class BlackjackPlayerManager
    {
        internal static void PlayerBet(BlackjackPlayer currentPlayer, int minBet, int maxBet)
        {
            try
            {
                //Gets a random bet for an AI player with given parameters
                int maxCapable = maxBet;
                Random rng = new Random();

                if (currentPlayer.Balance < maxBet)
                {
                    maxCapable = currentPlayer.Balance;
                }

                currentPlayer.Bet = rng.Next(minBet, maxCapable + 1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void GetPlayerResults(List<BlackjackPlayer> blackjackPlayers)
        {
            List<string> results = new List<string>();

            foreach (BlackjackPlayer blackjackPlayer in blackjackPlayers)
            {
                int handCount = 1;

                foreach (BlackjackHand hand in blackjackPlayer.Hands)
                {
                    //deals with dialouge in the event a hand was split
                    string handNumber = handCount > 1 ? $"hand {handCount}" : "hand";
                    if (hand.Action == HandActions.FlipBust)
                    {
                        results.Add(new string($"{blackjackPlayer.Username} {handNumber} busted."));
                    }

                    if (hand.Action == HandActions.FlipBlackjack)
                    {
                        results.Add(new string($"{blackjackPlayer.Username} {handNumber} got blackjack"));
                    }

                    if (hand.Action == HandActions.Stand)
                    {
                        // Deals with the idea that a hand could have an ace and the hardvalue was a bust
                        int score = hand.HardValue <= 21 ? hand.HardValue : hand.SoftValue;
                        results.Add(new string($"{blackjackPlayer.Username} {handNumber} had a score of {score}"));
                    }

                    handCount++;
                }
            }
        }
    }
}
