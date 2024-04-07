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
        public List<BlackjackHand> void PlayHands(BlackjackPlayer blackjackPlayer, BlackjackCard dealerCard)
        {
            try
            {
                blackjackPlayer.WasSplitEvaluated = BlackjackHandManager.EvaluateHands(blackjackPlayer.Hands, 
                                                                                       dealerCard, 
                                                                                       blackjackPlayer.WasSplitEvaluated)
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void PlayerBet(BlackjackPlayer currentPlayer, int minBet, int maxBet)
        {
            try
            {
                int maxCapable = maxBet;
                Random rng = new Random();

                if (currentPlayer.Balance < maxBet)
                {
                    maxCapable = currentPlayer.Balance;
                }

                currentPlayer.Bet = rng.Next(minBet, maxCapable + 1);

                currentPlayer.Balance = currentPlayer.Balance - currentPlayer.Bet;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
