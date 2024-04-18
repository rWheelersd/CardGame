using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackJack
{
    public class BlackjackGameManager
    {
        private int turnCounter = 0;
        public BlackjackGame BlackjackGame { get; private set; }
        public BlackjackGameManager(Guid gameId, int playerCount, int humanPlayers, int startingBalance) 
        {
            BlackjackGame = new BlackjackGame(gameId, playerCount, humanPlayers, startingBalance);
        }

        public PlayerStatus PlayerTurn(int option, List<BlackjackHand> blackjackHands)
        {
            switch (option)
            {
                case 1: //HIT
                    BlackjackGame.GameDeck.DealCard(blackjackHands[0]);
                    blackjackHands[0].Action = HandActions.Thinking;
                    return PlayerStatus.Active;
                case 2: //Stand
                    return PlayerStatus.Inactive;
                case 3: //Double Down
                    BlackjackGame.GameDeck.DealCard(blackjackHands[0]);
                    return PlayerStatus.Inactive;
                case 4: //Split
                    BlackjackHandManager.SplitHand(blackjackHands);
                    blackjackHands[0].Action = HandActions.Split;
                    return PlayerStatus.Active;
                default: throw new ArgumentOutOfRangeException(nameof(option));
            }
        }

        public void PlayAITurn()
        {

            foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
            {
                turnCounter++;

                //this might be out of place, go look up blackjack phases/action order
                BlackjackPlayerManager.PlayerBet(blackjackPlayer, BlackjackGame.minBet, BlackjackGame.maxBet);

                if (!blackjackPlayer.IsHuman && !blackjackPlayer.IsDealer)
                {
                    if (!blackjackPlayer.WasSplitEvaluated)
                    {
                        BlackjackHandManager.EvaluateSplit(blackjackPlayer.Hands[0], BlackjackGame.dealerCard);
                        if (blackjackPlayer.Hands[0].Action == HandActions.Split)
                        {
                            BlackjackHandManager.SplitHand(blackjackPlayer.Hands);
                        }
                        //there needs to be more double down logic outside of splits, and this will need to move after that is done
                        if (blackjackPlayer.Hands[0].Action == HandActions.DoubleDown)
                        {
                            BlackjackGame.GameDeck.DealCard(blackjackPlayer.Hands[0]);
                        }
                        blackjackPlayer.WasSplitEvaluated = true;
                    }


                    foreach (BlackjackHand hand in blackjackPlayer.Hands)
                    {

                        //More robust double down logic needed
                        //BlackjackHandManager.EvaluateDoubleDown(blackjackPlayer.Hands);
                        while (hand.Action == HandActions.Thinking || hand.Action == HandActions.Hit)
                        {
                            if (hand.Action == HandActions.Hit)
                            {
                                BlackjackGame.GameDeck.DealCard(hand);
                            }
                            BlackjackHandManager.GetAction(hand, BlackjackGame.dealerCard);
                        }
                    }
                }
            }
        }
    }
}
