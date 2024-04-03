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
    public class BlackjackHandManager
    {

        public void EvaluateHand(List<BlackjackHand> hands, BlackjackCard dealerCard, bool splitEvaluated)
        {
            try
            {
                if (!splitEvaluated)
                {
                    PlayerActions action = EvaluateSplit(hands.FirstOrDefault().Cards.FirstOrDefault(), dealerCard);

                    if (action == PlayerActions.Split)
                    {
                        SplitHand(hands);
                        GetAction(hands);
                    }
                    if (action == PlayerActions.DoubleDown)
                    {

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void SplitHand(List<BlackjackHand> hands)
        {
            BlackjackHand splitHand0 = new BlackjackHand().Cards.Add(hands.Cards[0]);
            BlackjackHand splitHand1 = new BlackjackHand().Cards.Add(hands.Cards[1])

            hands.RemoveAll();

            hands.Add(splitHand0);
            hands.Add(splitHand1);
        }

        private PlayerActions EvaluateSplit(BlackjackCard playerCard, BlackjackCard dealerCard)
        {
            try
            {
                if (playerCard.CardRank == Rank.Eight) return PlayerActions.Split;
                if (playerCard.CardRank == Rank.Eight) return PlayerActions.Split;
                if (playerCard.CardRank == Rank.Five && dealerCard.CardRank <= Rank.Nine) return PlayerActions.DoubleDown;
                if (playerCard.CardRank == Rank.Three && (dealerCard.CardRank >= Rank.Four && dealerCard.CardRank >= Rank.Seven)) return PlayerActions.Split;
                if (playerCard.CardRank == Rank.Two && (dealerCard.CardRank >= Rank.Three && dealerCard.CardRank >= Rank.Seven)) return PlayerActions.Split;
                else return PlayerActions.Thinking;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetAction(List<BlackjackHand> hands, BlackjackCard dealerCard)
        {
            try
            {
                foreach (BlackjackHand blackjackHand in hands)
                {
                    int hardValue = 0;
                    int softValue = 0;

                    foreach (BlackjackCard blackjackCard in blackjackHand)
                    {
                        hardValue += blackjackCard.CardValue;
                    }

                    //Checks if hand is soft and sets a soft value
                    if (blackjackHand.Any(c => c.CardRank == Rank.Ace))
                    {
                        softValue = hardValue - 10;
                        blackjackHand.IsSoft = true;

                        //if soft value is busted then hard value must also be busted
                        if (softValue > 21)
                        {
                            return PlayerActions.FlipBust;
                        }
                    }

                    //Blackjack
                    if (hardValue == 21 || softValue == 21)
                    {
                        return PlayerActions.FlipBlackjack;
                    }

                    //Bust
                    if (hardValue > 21 && !playerHand.IsSoft)
                    {
                        return PlayerActions.FlipBust;
                    }

                    //Checks starting hand for surrender conditions
                    if (!playerHand.IsSoft && playerHand.Cards.Count == 2)
                    {
                        if (hardValue == 16 && dealerCard.CardValue >= 11)
                        {
                            return PlayerActions.Surrender;
                        }

                        if (hardValue == 15 && dealerCard.CardValue == 10)
                        {
                            return PlayerActions.Surrender;
                        }
                    }

                    //Returns action for hard hand
                    if (blackjackHand.IsSoft)
                    {
                        if (hardValue <= 11) return PlayerActions.Hit;

                        if ((hardValue >= 12 && hardValue <= 16) && dealerCard.CardValue >= 7) return PlayerActions.Hit;

                        else return blackjackHand.;
                    }
                }

                

                

                


                

                //Returns action for soft hand
                if (playerHand.IsSoft)
                {
                    if (hardValue >= 19 && hardValue <= 21)
                    {
                        return PlayerActions.Stand;
                    }

                    if (hardValue == 18)
                    {
                        if (dealerCard.CardValue == 2) return PlayerActions.Stand;
                        if (dealerCard.CardValue >= 3 && dealerCard.CardValue <= 6) return PlayerActions.DoubleDown;
                        if (dealerCard.CardValue == 7 || dealerCard.CardValue == 8) return PlayerActions.Stand;
                        if (dealerCard.CardValue >= 9) return PlayerActions.Hit;
                    }

                    if (hardValue == 17)
                    {
                        return PlayerActions.Stand;
                    }

                    if (hardValue < 17)
                    {
                        return PlayerActions.Hit;
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
