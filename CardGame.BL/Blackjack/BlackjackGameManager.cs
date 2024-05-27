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
            //Gets initial hand value, i dont like how this is done. Return to figure out a better way to do this later
            if (blackjackHands[0].HardValue == 0)
            {
                BlackjackHandManager.GetHandValues(blackjackHands[0]);
            }

            switch (option)
            {
                case 1: //HIT
                    BlackjackGame.GameDeck.DealCard(blackjackHands[0]);
                    BlackjackHandManager.GetHandValues(blackjackHands[0]);
                    if (blackjackHands[0].Action != HandActions.FlipBust && blackjackHands[0].Action != HandActions.FlipBlackjack)
                    {
                        return PlayerStatus.Active;
                    }
                    else
                    {
                        return PlayerStatus.Inactive;
                    }

                case 2: //Stand
                    blackjackHands[0].Action = HandActions.Stand;
                    return PlayerStatus.Inactive;

                case 3: //Double Down
                    BlackjackGame.GameDeck.DealCard(blackjackHands[0]);
                    blackjackHands[0].Action = HandActions.Stand;
                    BlackjackHandManager.GetHandValues(blackjackHands[0]);
                    return PlayerStatus.Inactive;
                    break;

                case 4: //Split
                    BlackjackHandManager.SplitHand(blackjackHands, blackjackHands[0]);
                    return PlayerStatus.Active;

                default: throw new ArgumentOutOfRangeException(nameof(option));
            }
        }

        public void PlayAITurns()
        {
            try
            {
                foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
                {
                    if (!blackjackPlayer.IsHuman && !blackjackPlayer.IsDealer)
                    {
                        PlayAIPlayerTurn(blackjackPlayer);
                    }
                    else if (!blackjackPlayer.IsHuman && blackjackPlayer.IsDealer)
                    {
                        PlayDealerTurn(blackjackPlayer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //the bug starts here. logic is flawed. 
        private void PlayAIPlayerTurn(BlackjackPlayer blackjackPlayer)
        {
            bool allHandsSplitEvaluated = false;
            bool allHandsResolved = false;

            do
            {
                foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
                {
                    if (blackjackHand.WasSplitEvaluated && !blackjackHand.IsHandResolved())
                    {
                        PlaySplitEvaluatedHand(blackjackHand);
                    }
                    else if (!blackjackHand.WasSplitEvaluated && !blackjackHand.IsHandResolved())
                    {
                        EvaluateHandForSplit(blackjackPlayer, blackjackHand);
                    }
                }

                allHandsSplitEvaluated = blackjackPlayer.Hands.Any(h => !h.WasSplitEvaluated);
                allHandsResolved = blackjackPlayer.Hands.Any(h => h.IsHandResolved());

            } while (allHandsSplitEvaluated && allHandsResolved);
            
        }

        private void PlaySplitEvaluatedHand(BlackjackHand blackjackHand)
        {
            while (blackjackHand.Action == HandActions.Thinking || blackjackHand.Action == HandActions.Hit)
            {
                //Deals with double down in event BlackjackHandManager logic returns it
                if (BlackjackHandManager.EvaluateDoubleDown(blackjackHand, BlackjackGame.dealerCard))
                {
                    BlackjackGame.GameDeck.DealCard(blackjackHand);
                    break;
                }

                //Gets initial action on thinking status
                if (blackjackHand.Action == HandActions.Thinking)
                {
                    BlackjackHandManager.GetAction(blackjackHand, BlackjackGame.dealerCard);
                }

                ///Deals card in event BlackjackHandManager logic returns hit, then gets the next action
                if (blackjackHand.Action == HandActions.Hit)
                {
                    BlackjackGame.GameDeck.DealCard(blackjackHand);
                    BlackjackHandManager.GetAction(blackjackHand, BlackjackGame.dealerCard);
                }
            }
        }

        private void EvaluateHandForSplit(BlackjackPlayer blackjackPlayer, BlackjackHand blackjackHand)
        {
            if (blackjackHand.Cards.Count == 1)
            {
                BlackjackGame.GameDeck.DealCard(blackjackHand);
            }
            bool wasPair = BlackjackHandManager.CheckPair(blackjackHand);
            if (wasPair)
            {
                BlackjackHandManager.EvaluateSplit(blackjackHand, BlackjackGame.dealerCard);
                if (blackjackHand.Action == HandActions.Split)
                {
                    BlackjackHandManager.SplitHand(blackjackPlayer.Hands, blackjackHand);
                }
            }
        }

        private void PlayDealerTurn(BlackjackPlayer blackjackPlayer)
        {
            while (blackjackPlayer.Hands[0].Action == HandActions.Thinking || blackjackPlayer.Hands[0].Action == HandActions.Hit)
            {
                if (blackjackPlayer.Hands[0].Action == HandActions.Thinking)
                {
                    BlackjackHandManager.HandleDealer(blackjackPlayer.Hands[0]);
                }
                if (blackjackPlayer.Hands[0].Action == HandActions.Hit)
                {
                    BlackjackGame.GameDeck.DealCard(blackjackPlayer.Hands[0]);
                    BlackjackHandManager.HandleDealer(blackjackPlayer.Hands[0]);
                }
            }
        }

        public void ResetGame()
        {
            //Resets each each property that needs to be reset prior to starting a new round
            for (int i = BlackjackGame.Players.Count - 1; i >= 0; i--)
            {
                if (BlackjackGame.Players[i].Balance <= 0 && !BlackjackGame.Players[i].IsDealer)
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
            BlackjackGame.GameDeck.ResetDeck();
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
                        //Looping required in case of players with split hands
                        GetPayoutCondition(blackjackHand, dealerHand);
                    }
                }

                //More handling of the idea that a player may have more than one hand
                //because a player with more than one hand may only win or lose their bet once
                if (blackjackPlayer.Hands.Any(h => h.WinningHand == true))
                {
                    if (blackjackPlayer.Hands.Any(h => h.Action == HandActions.FlipBlackjack))
                    {
                        if (dealerHands.Any(h => h.Action == HandActions.FlipBlackjack))
                        {
                            //If there is a push, no money is won or lost
                        }
                        else
                        {
                            blackjackPlayer.Balance += blackjackPlayer.Bet + (blackjackPlayer.Bet / 2);
                        }
                    }
                    else
                    {
                        blackjackPlayer.Balance += blackjackPlayer.Bet * 2;
                    }
                }
                else
                {
                    blackjackPlayer.Balance -= blackjackPlayer.Bet;
                }
            }
        }

        private void GetPayoutCondition(BlackjackHand playerHand, BlackjackHand dealerHand)
        {
            //All of this determines if a hand is a winning hand or not
            if (playerHand.Action == HandActions.FlipBlackjack)
            {
                if (dealerHand.Action != HandActions.FlipBlackjack)
                {
                    playerHand.WinningHand = true;
                }
            }
            else
            {
                int playerValue = playerHand.HardValue;
                int dealerValue = dealerHand.HardValue;

                if (playerValue <= 21)
                {
                    if ((dealerValue > 21) || (playerValue > dealerValue))
                    {
                        playerHand.WinningHand = true;
                    }
                    else if (playerValue < dealerValue)
                    {
                        playerHand.WinningHand = false;
                    }
                }
                else
                {
                    playerHand.WinningHand = false;
                }
            }
        }
    }
}
