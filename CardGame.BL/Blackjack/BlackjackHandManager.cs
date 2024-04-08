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
    public static class BlackjackHandManager
    {

        public static bool EvaluateHands(List<BlackjackHand> hands, BlackjackCard dealerCard, bool splitEvaluated)
        {
            try
            {
                if (!splitEvaluated)
                {
                    HandActions action = EvaluateSplit(hands.FirstOrDefault().Cards.FirstOrDefault(), dealerCard);

                    if (action == HandActions.Split)
                    {
                        SplitHand(hands);
                        GetAction(hands, dealerCard);
                    }
                    if (action == HandActions.DoubleDown)
                    {
                        hands.First().Action = HandActions.DoubleDown;
                    }
                }

                GetAction(hands, dealerCard);
                splitEvaluated = true;
                return splitEvaluated;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private static void SplitHand(List<BlackjackHand> hands)
        {
            BlackjackHand splitHand0 = new BlackjackHand();
            BlackjackHand splitHand1 = new BlackjackHand();

            splitHand0.Cards.Add(hands[0].Cards[0]);
            splitHand1.Cards.Add(hands[0].Cards[1]);

            hands.Clear();

            hands.Add(splitHand0);
            hands.Add(splitHand1);
        }

        private static HandActions EvaluateSplit(BlackjackCard playerCard, BlackjackCard dealerCard)
        {
            try
            {
                if (playerCard.CardRank == Rank.Eight) return HandActions.Split;
                if (playerCard.CardRank == Rank.Eight) return HandActions.Split;
                if (playerCard.CardRank == Rank.Five && dealerCard.CardRank <= Rank.Nine) return HandActions.DoubleDown;
                if (playerCard.CardRank == Rank.Three && (dealerCard.CardRank >= Rank.Four && dealerCard.CardRank >= Rank.Seven)) return HandActions.Split;
                if (playerCard.CardRank == Rank.Two && (dealerCard.CardRank >= Rank.Three && dealerCard.CardRank >= Rank.Seven)) return HandActions.Split;
                else return HandActions.Thinking;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void GetAction(List<BlackjackHand> hands, BlackjackCard dealerCard)
        {
            try
            {
                foreach (BlackjackHand blackjackHand in hands)
                {
                    int hardValue = 0;
                    int softValue = 0;

                    foreach (BlackjackCard blackjackCard in blackjackHand.Cards)
                    {
                        hardValue += blackjackCard.CardValue;
                    }

                    //Checks if hand is soft and sets a soft value
                    if (blackjackHand.Cards.Any(c => c.CardRank == Rank.Ace))
                    {
                        softValue = hardValue - 10;
                        blackjackHand.IsSoft = true;

                        //if soft value is busted then hard value must also be busted
                        if (softValue > 21)
                        {
                            blackjackHand.Action = HandActions.FlipBust; break;
                        }
                    }

                    //Blackjack
                    if (hardValue == 21 || softValue == 21)
                    {
                        blackjackHand.Action = HandActions.FlipBlackjack; break;
                    }

                    //Bust
                    if (hardValue > 21 && !blackjackHand.IsSoft)
                    {
                        blackjackHand.Action = HandActions.FlipBust; break;
                    }

                    //Checks starting hand for surrender conditions
                    if (!blackjackHand.IsSoft && blackjackHand.Cards.Count == 2)
                    {
                        if (hardValue == 16 && dealerCard.CardValue >= 11)
                        {
                            blackjackHand.Action = HandActions.Surrender; break;
                        }

                        if (hardValue == 15 && dealerCard.CardValue == 10)
                        {
                            blackjackHand.Action = HandActions.Surrender; break;
                        }
                    }

                    //Returns action for hard hand
                    if (blackjackHand.IsSoft)
                    {
                        if (hardValue <= 11) blackjackHand.Action = HandActions.Hit; break;

                        if ((hardValue >= 12 && hardValue <= 16) && dealerCard.CardValue >= 7) blackjackHand.Action = HandActions.Hit; break;

                        blackjackHand.Action = HandActions.Stand; break;
                    }

                    //Returns action for soft hand
                    if (blackjackHand.IsSoft)
                    {
                        if (hardValue >= 19 && hardValue <= 21)
                        {
                            blackjackHand.Action = HandActions.Stand; break;
                        }

                        if (hardValue == 18)
                        {
                            if (dealerCard.CardValue == 2) blackjackHand.Action = HandActions.Stand; break;
                            if (dealerCard.CardValue >= 3 && dealerCard.CardValue <= 6) blackjackHand.Action = HandActions.DoubleDown; break;
                            if (dealerCard.CardValue == 7 || dealerCard.CardValue == 8) blackjackHand.Action = HandActions.Stand; break;
                            if (dealerCard.CardValue >= 9) blackjackHand.Action = HandActions.Hit; break;
                        }

                        if (hardValue == 17)
                        {
                            blackjackHand.Action = HandActions.Stand; break;
                        }

                        if (hardValue < 17)
                        {
                            blackjackHand.Action = HandActions.Hit; break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
