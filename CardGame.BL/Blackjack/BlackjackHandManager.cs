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
        public static bool PairCheck(BlackjackHand hand)
        {
            try
            {
                return hand.Cards[0] == hand.Cards[1];
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static HandActions EvaluateSplit(BlackjackCard playerCard, BlackjackCard dealerCard)
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

        public static void SplitHand(List<BlackjackHand> hands)
        {
            BlackjackHand splitHand0 = new BlackjackHand();
            BlackjackHand splitHand1 = new BlackjackHand();

            splitHand0.Cards.Add(hands[0].Cards[0]);
            splitHand1.Cards.Add(hands[0].Cards[1]);

            hands.Clear();

            hands.Add(splitHand0);
            hands.Add(splitHand1);
        }

        

        public static void GetAction(BlackjackHand blackjackHand, BlackjackCard dealerCard)
        {
            try
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
                        blackjackHand.Action = HandActions.FlipBust; 
                    }
                }

                //Blackjack
                if (hardValue == 21 || softValue == 21)
                {
                    blackjackHand.Action = HandActions.FlipBlackjack; 
                }

                //Bust
                if (hardValue > 21 && !blackjackHand.IsSoft)
                {
                    blackjackHand.Action = HandActions.FlipBust; 
                }

                //Returns action for hard hand
                if (blackjackHand.IsSoft)
                {
                    if (hardValue <= 11) blackjackHand.Action = HandActions.Hit; 

                    if ((hardValue >= 12 && hardValue <= 16) && dealerCard.CardValue >= 7) blackjackHand.Action = HandActions.Hit; 

                    blackjackHand.Action = HandActions.Stand; 
                }

                //Returns action for soft hand
                if (blackjackHand.IsSoft)
                {
                    if (hardValue >= 19 && hardValue <= 21)
                    {
                        blackjackHand.Action = HandActions.Stand;
                    }

                    if (hardValue == 18)
                    {
                        if (dealerCard.CardValue == 2) blackjackHand.Action = HandActions.Stand;
                        if (dealerCard.CardValue >= 3 && dealerCard.CardValue <= 6) blackjackHand.Action = HandActions.DoubleDown;
                        if (dealerCard.CardValue == 7 || dealerCard.CardValue == 8) blackjackHand.Action = HandActions.Stand;
                        if (dealerCard.CardValue >= 9) blackjackHand.Action = HandActions.Hit;
                    }

                    if (hardValue == 17)
                    {
                        blackjackHand.Action = HandActions.Stand;
                    }

                    if (hardValue < 17)
                    {
                        blackjackHand.Action = HandActions.Hit;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void EvaluateDoubleDown(List<BlackjackHand> hands)
        {
            throw new NotImplementedException();
        }
    }
}
