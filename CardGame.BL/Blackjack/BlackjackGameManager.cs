using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

                case 2: //Stand

                case 3: //Double Down

                case 4: //Split

                default: throw new ArgumentOutOfRangeException(nameof(option));
            }
        }

        public void PlayAITurn()
        {
            //Cycle through all players in game
            foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
            {
                //Operates only on players not human or dealer
                if (!blackjackPlayer.IsHuman && !blackjackPlayer.IsDealer)
                {
                    //collects there bets and incremets counter for event driven UI
                    turnCounter++;
                    BlackjackPlayerManager.PlayerBet(blackjackPlayer, BlackjackGame.minBet, BlackjackGame.maxBet);

                    //Evlautaes hand for split based on if already evalutaed and hand is a pair. Splits or doesnt based on BlackjackHandManager logic.
                    if (!blackjackPlayer.WasSplitEvaluated && BlackjackHandManager.CheckPair(blackjackPlayer.Hands[0]))
                    {
                        BlackjackHandManager.EvaluateSplit(blackjackPlayer.Hands[0], BlackjackGame.dealerCard);
                        if (blackjackPlayer.Hands[0].Action == HandActions.Split)
                        {
                            //This will split hands into the list, it will default reset hand action to thinking as its done in blackjackhand constructor
                            BlackjackHandManager.SplitHand(blackjackPlayer.Hands);
                        }
                        blackjackPlayer.WasSplitEvaluated = true;
                    }

                    //Cycles through each hand in the players list of hands in case of split hand
                    foreach (BlackjackHand hand in blackjackPlayer.Hands)
                    {
                        while (hand.Action == HandActions.Thinking || hand.Action == HandActions.Hit)
                        {
                            //Gets initial action on thinking status
                            if (hand.Action == HandActions.Thinking)
                            {
                                BlackjackHandManager.GetAction(hand, BlackjackGame.dealerCard);
                            }

                            //deals card in event BlackjackHandManager logic returns hit
                            if (hand.Action == HandActions.Hit)
                            {
                                BlackjackGame.GameDeck.DealCard(hand);
                            }

                            //deals with double down in event BlackjackHandManager logic returns so
                            if (BlackjackHandManager.EvaluateDoubleDown(hand, BlackjackGame.dealerCard))
                            {
                                BlackjackGame.GameDeck.DealCard(hand);
                                hand.Action = HandActions.DoubleDown;
                            }

                            else
                            {
                                //if BlackjackHandManager logic returns flipbust, flipblackjack or stand, exit the while loop to play the next player
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void ResetPlayers()
        {
            for (int i = BlackjackGame.Players.Count - 1; i >= 0; i--)
            {
                if (BlackjackGame.Players[i].Balance <= 0)
                {
                    BlackjackGame.Players.Remove(BlackjackGame.Players[i]);
                }
                else
                {
                    BlackjackGame.Players[i].Status = PlayerStatus.Active;
                    BlackjackGame.Players[i].Hands.Clear();
                    BlackjackGame.Players[i].Bet = 0;
                }
            }
        }

        public void ManagePayouts()
        {
            List<BlackjackHand> dealerHands = BlackjackGame.Players.LastOrDefault(p => p.IsDealer).Hands;

            foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
            {
                foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
                {
                    foreach (BlackjackHand dealerHand in dealerHands)
                    {
                        CompareHands(blackjackPlayer, blackjackHand, dealerHand);
                    }
                }
            }
        }

        private void CompareHands(BlackjackPlayer blackjackPlayer, BlackjackHand playerHand, BlackjackHand dealerHand)
        {
            if (playerHand.Action == HandActions.FlipBlackjack)
            {
                if (dealerHand.Action != HandActions.FlipBlackjack)
                {
                    blackjackPlayer.Balance += blackjackPlayer.Bet + (blackjackPlayer.Bet / 2);
                }
            }
            else if (dealerHand.Action == HandActions.FlipBlackjack)
            {
                
            }
            else
            {
                int playerValue = playerHand.HardValue;
                int dealerValue = dealerHand.HardValue;

                if (playerValue <= 21)
                {
                    if ((dealerValue > 21) || (playerValue > dealerValue))
                    {
                        blackjackPlayer.Balance += blackjackPlayer.Bet * 2;
                    }
                    else if (playerValue < dealerValue)
                    {
                        blackjackPlayer.Balance -= blackjackPlayer.Bet;
                    }
                }
                else
                {
                    blackjackPlayer.Balance -= blackjackPlayer.Bet;
                }
            }
        }
    }
}
