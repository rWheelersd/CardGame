using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackJack
{
    public class BlackjackGameManager
    {
        private int turnCounter = 0;
        public BlackjackGame BlackjackGame { get; private set; }
        public BlackjackGameManager(Guid gameId, int playerCount, int startingBalance) 
        {
            BlackjackGame = new BlackjackGame(gameId, playerCount, startingBalance);
        }

        public void PlayAITurn()
        {
            BlackjackCard dealerCard = BlackjackGame.Players.FirstOrDefault(p => p.IsDealer).Hands.Cards;

            foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
            {
                //this might be out of place, go look up blackjack phases/action order
                BlackjackPlayerManager.PlayerBet(blackjackPlayer, BlackjackGame.minBet, BlackjackGame.maxBet)

                if (!blackjackPlayer.IsHuman && !blackjackPlayer.IsDealer)
                {
                    foreach (BlackjackHand hand in blackjackPlayer.Hands)
                    {
                        while (hand.Action == HandActions.Thinking || hand.Action == HandActions.Hit)
                        {
                            BlackjackPlayerManager.PlayHands(blackjackPlayer, dealerCard)
                        }
                        if (hand.Action == HandActions.Surrender)
                        {

                        }
                        if (hand.Action == HandActions.Stand)
                        {
                            blackjackPlayer.Status == PlayerStatus.
                        }
                        if (hand.Action == HandActions.FlipBlackjack)
                        {

                        }
                        if (hand.Action == HandActions.FlipBust)
                        {

                        }
                        if (hand.Action == HandActions.DoubleDown)
                        {

                        }
                    }
                }
            }
        }
    }
}
