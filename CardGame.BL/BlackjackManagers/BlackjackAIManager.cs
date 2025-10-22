using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackjackManagers
{
    public class BlackjackAIManager 
    {
        private Card dealerCard;
        private Deck<BlackjackHand> deck;

        public BlackjackAIManager(Card dealerCard, Deck<BlackjackHand> deck) 
        { 
            this.dealerCard = dealerCard; 
            this.deck = deck;
        }

        public void PlayAITurns(List<BlackjackPlayer> blackjackPlayers)
        {
            try
            {
                foreach (BlackjackPlayer blackjackPlayer in blackjackPlayers)
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

        private void PlayAIPlayerTurn(BlackjackPlayer blackjackPlayer)
        {
            bool allHandsSplitEvaluated = false;
            bool allHandsResolved = false;

            do
            {
                for (int i = 0; i < blackjackPlayer.Hands.Count; i++)
                {
                    BlackjackHand blackjackHand = blackjackPlayer.Hands[i];

                    if (blackjackHand.WasSplitEvaluated && !blackjackHand.IsHandResolved())
                    {
                        PlaySplitEvaluatedHand(blackjackHand);
                    }
                    else if (!blackjackHand.WasSplitEvaluated && !blackjackHand.IsHandResolved())
                    {
                        EvaluateHandForSplit(blackjackPlayer, blackjackHand);
                    }
                }

                allHandsSplitEvaluated = blackjackPlayer.Hands.All(h => h.WasSplitEvaluated);
                allHandsResolved = blackjackPlayer.Hands.All(h => h.IsHandResolved());

            } while (!allHandsSplitEvaluated || !allHandsResolved);
        }

        private void PlaySplitEvaluatedHand(BlackjackHand blackjackHand)
        {
            while (blackjackHand.Action == HandActions.Thinking || blackjackHand.Action == HandActions.Hit)
            {
                //Deals with double down in event BlackjackHandManager logic returns it
                if (BlackjackHandManager.EvaluateDoubleDown(blackjackHand, dealerCard))
                {
                    deck.DealCards(blackjackHand);
                    break;
                }

                //Gets initial action on thinking status
                if (blackjackHand.Action == HandActions.Thinking)
                {
                    BlackjackHandManager.GetAction(blackjackHand, dealerCard);
                }

                //Deals card in event BlackjackHandManager logic returns hit, then gets the next action
                if (blackjackHand.Action == HandActions.Hit)
                {
                    deck.DealCards(blackjackHand);
                    BlackjackHandManager.GetAction(blackjackHand, dealerCard);
                }
            }
        }

        private void EvaluateHandForSplit(BlackjackPlayer blackjackPlayer, BlackjackHand blackjackHand)
        {
            if (blackjackHand.Cards.Count == 1)
            {
                deck.DealCards(blackjackHand);
            }
            bool isPair = BlackjackHandManager.CheckPair(blackjackHand);
            if (isPair)
            {
                BlackjackHandManager.EvaluateSplit(blackjackHand, dealerCard);
                if (blackjackHand.Action == HandActions.Split)
                {
                    BlackjackHandManager.SplitHand(blackjackPlayer.Hands, blackjackHand);
                    return;
                }
            }
            if (blackjackPlayer.Hands.Count >2)
            {
                int x = 0;
            }
            blackjackHand.SetSplitEvaluation(true);
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
                    deck.DealCards(blackjackPlayer.Hands[0]);
                    BlackjackHandManager.HandleDealer(blackjackPlayer.Hands[0]);
                }
            }
        }

        internal void CollectBets(List<BlackjackPlayer> blackjackPlayers, int minBet, int maxBet)
        {
            try
            {
                foreach (BlackjackPlayer blackjackPlayer in blackjackPlayers)
                {
                    if (blackjackPlayer.IsHuman || blackjackPlayer.IsDealer)
                    {
                        continue;
                    }

                    //Gets a random bet for an AI player with given parameters
                    int maxCapable = maxBet;
                    Random rng = new Random();

                    if (blackjackPlayer.Balance < maxBet)
                    {
                        maxCapable = blackjackPlayer.Balance;
                    }

                    blackjackPlayer.UpdateBet(rng.Next(minBet, maxCapable + 1));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
