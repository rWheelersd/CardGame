using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.BL.Models.Interfaces;
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
        public static bool CheckPair(BlackjackHand hand)
        {
            return hand.Cards[0].CardRank == hand.Cards[1].CardRank;
        }
        public static void EvaluateSplit(BlackjackHand hand, BlackjackCard dealerCard)
        {
            try
            {
                //Checks if cards are a pair, then evaluates if hand should be split or double down
                if (hand.Cards[0].CardRank == hand.Cards[1].CardRank)
                {
                    if (hand.Cards[0].CardRank == Rank.Eight) hand.Action = HandActions.Split;
                    else if (hand.Cards[0].CardRank == Rank.Three && (dealerCard.CardRank >= Rank.Four && dealerCard.CardRank >= Rank.Seven)) hand.Action = HandActions.Split;
                    else if (hand.Cards[0].CardRank == Rank.Two && (dealerCard.CardRank >= Rank.Three && dealerCard.CardRank >= Rank.Seven)) hand.Action = HandActions.Split;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Creates two hands, splits the cards in the initial hands list into the new hands, clears current list and adds the new hands
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
                GetHandValues(blackjackHand);
                EvaluateHand(blackjackHand);

                if (blackjackHand.Action == HandActions.FlipBust || blackjackHand.Action == HandActions.FlipBlackjack)
                {
                    return;
                }
                else
                {
                    //Returns action for hard hand
                    if (!blackjackHand.IsSoft)
                    {
                        if (blackjackHand.HardValue <= 11) blackjackHand.Action = HandActions.Hit;
                        else if ((blackjackHand.HardValue >= 12 && blackjackHand.HardValue <= 16) && dealerCard.CardValue >= 7) blackjackHand.Action = HandActions.Hit;
                        else blackjackHand.Action = HandActions.Stand;
                    }

                    //Returns action for soft hand
                    if (blackjackHand.IsSoft)
                    {
                        if (blackjackHand.HardValue >= 19 && blackjackHand.HardValue <= 21)
                        {
                            blackjackHand.Action = HandActions.Stand;
                        }
                        else if (blackjackHand.HardValue == 18)
                        {
                            if (dealerCard.CardValue == 2) blackjackHand.Action = HandActions.Stand;
                            else if (dealerCard.CardValue >= 3 && dealerCard.CardValue <= 6) blackjackHand.Action = HandActions.DoubleDown;
                            else if (dealerCard.CardValue == 7 || dealerCard.CardValue == 8) blackjackHand.Action = HandActions.Stand;
                            else blackjackHand.Action = HandActions.Hit;
                        }
                        else if (blackjackHand.HardValue == 17)
                        {
                            blackjackHand.Action = HandActions.Stand;
                        }
                        else if (blackjackHand.HardValue < 17)
                        {
                            blackjackHand.Action = HandActions.Hit;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal static void GetHandValues(BlackjackHand blackjackHand)
        {
            if (blackjackHand.HardValue == 0)
            {
                //Calcualte the initial hand value, checks if the hand is soft by looking for a an ace, sets the soft value
                //if it is and set the hand to soft
                blackjackHand.HardValue += blackjackHand.Cards[0].CardValue + blackjackHand.Cards[1].CardValue;

                if (blackjackHand.Cards.Any(c => c.CardRank == Rank.Ace))
                {
                    blackjackHand.SoftValue = blackjackHand.HardValue - 10;
                    blackjackHand.IsSoft = true;
                }
            }
            else
            {
                //Dealt card management
                if (blackjackHand.Cards.Last().CardRank == Rank.Ace)
                {
                    //if the card was an ace and the hard value is over 21 then add the aces soft value to the hard value
                    if (blackjackHand.HardValue + blackjackHand.Cards.Last().CardValue > 21)
                    {
                        blackjackHand.HardValue += 1;
                    }
                    else
                    {
                        //if it isnt over 21 then update both soft and hard values accordingly and ensure the handi is flagged as soft
                        blackjackHand.HardValue += 11;
                        blackjackHand.SoftValue = blackjackHand.HardValue - 10;
                        blackjackHand.IsSoft = true;
                    }
                }
                else
                {
                    //If it isnt an ace just calculate the hard value
                    blackjackHand.HardValue += blackjackHand.Cards.Last().CardValue;
                }
            }
        }


        internal static void EvaluateHand(BlackjackHand blackjackHand)
        {
            //Blackjack
            if (blackjackHand.HardValue == 21 || blackjackHand.SoftValue == 21)
            {
                blackjackHand.Action = HandActions.FlipBlackjack;
            }

            //Bust
            if (blackjackHand.HardValue > 21 && !blackjackHand.IsSoft)
            {
                blackjackHand.Action = HandActions.FlipBust;
            }

            //Bust soft value
            if (blackjackHand.SoftValue > 21 && blackjackHand.IsSoft)
            {
                blackjackHand.Action = HandActions.FlipBust;
            }
        }

        internal static bool EvaluateDoubleDown(BlackjackHand blackjackHand, BlackjackCard dealerCard)
        {
            if ((blackjackHand.HardValue == 9 && dealerCard.CardValue >= 3 && dealerCard.CardValue <= 6) ||
                (blackjackHand.HardValue == 10 && dealerCard.CardValue >= 2 && dealerCard.CardValue <= 9) ||
                (blackjackHand.HardValue == 11 && dealerCard.CardValue >= 2 && dealerCard.CardValue <= 10))
            {
                return true;
            }
            return false;
        }
    }
}
