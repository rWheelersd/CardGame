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
                    BlackjackGame.GameDeck.DealCard(blackjackHands[0]);
                    BlackjackHandManager.GetHandValues(blackjackHands[0]);
                    BlackjackHandManager.EvaluateHand(blackjackHands[0]);

                    if (blackjackHands[0].Action == HandActions.FlipBust || blackjackHands[0].Action == HandActions.FlipBlackjack)
                    {
                        return PlayerStatus.Inactive;
                    }
                    else
                    {
                        blackjackHands[0].Action = HandActions.Thinking;
                        return PlayerStatus.Active;
                    }

                case 2: //Stand
                    return PlayerStatus.Inactive;

                case 3: //Double Down
                    BlackjackGame.GameDeck.DealCard(blackjackHands[0]);
                    BlackjackHandManager.GetHandValues(blackjackHands[0]);
                    BlackjackHandManager.EvaluateHand(blackjackHands[0]);
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
                        blackjackPlayer.WasSplitEvaluated = true;
                    }

                    foreach (BlackjackHand hand in blackjackPlayer.Hands)
                    {
                        while (hand.Action == HandActions.Thinking || hand.Action == HandActions.Hit)
                        {
                            if (hand.Action == HandActions.Hit)
                            {
                                BlackjackGame.GameDeck.DealCard(hand);
                            }

                            if (hand.Action != HandActions.Split && BlackjackHandManager.EvaluateDoubleDown(hand, BlackjackGame.dealerCard))
                            {
                                BlackjackGame.GameDeck.DealCard(hand);
                                hand.Action = HandActions.DoubleDown;
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
