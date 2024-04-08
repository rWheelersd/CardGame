using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;
using static System.Collections.Specialized.BitVector32;

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
            BlackjackCard dealerCard = BlackjackGame.Players.FirstOrDefault(p => p.IsDealer)
                                                            .Hands.First()
                                                            .Cards.First(c => c.IsVisible == true);

            foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
            {
                //this might be out of place, go look up blackjack phases/action order
                BlackjackPlayerManager.PlayerBet(blackjackPlayer, BlackjackGame.minBet, BlackjackGame.maxBet);

                if (!blackjackPlayer.IsHuman && !blackjackPlayer.IsDealer)
                {
                    //Check for split here
                    if (BlackjackHandManager.PairCheck(blackjackPlayer.Hands[0]))
                    {
                        HandActions action = BlackjackHandManager.EvaluateSplit(blackjackPlayer.Hand.Cards[0], dealerCard);
                        if (action == HandActions.Split)
                        {
                            BlackjackHandManager.SplitHand(blackjackPlayer.Hands);
                        }
                        //there needs to be more double down logic outside of splits, and this will need to move after that is done
                        if (action == HandActions.DoubleDown)
                        {
                            BlackjackGame.GameDeck.DealCard(blackjackPlayer.Hands[0]);
                        }
                    }


                    foreach (BlackjackHand hand in blackjackPlayer.Hands)
                    {
                        while (hand.Action == HandActions.Thinking || hand.Action == HandActions.Hit)
                        {
                            if (hand.Action == HandActions.Hit)
                            {
                                BlackjackGame.GameDeck.DealCard(hand);
                            }
                        }
                        if (hand.Action == HandActions.Surrender)
                        {

                        }
                        if (hand.Action == HandActions.Stand)
                        {
                            
                        }
                        if (hand.Action == HandActions.FlipBlackjack)
                        {

                        }
                        if (hand.Action == HandActions.FlipBust)
                        {

                        }
                    }
                }
            }
        }
    }
}
