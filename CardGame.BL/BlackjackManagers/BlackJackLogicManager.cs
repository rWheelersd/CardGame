using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackjackManagers
{
    internal static class BlackjackHandManager
    {
        public static bool CheckPair(BlackjackHand blackjackHand)
        {
            blackjackHand.SetSplitEvaluation(true);
            return blackjackHand.Cards[0].CardRank == blackjackHand.Cards[1].CardRank;
        }
        internal static void EvaluateSplit(BlackjackHand blackjackHand, Card dealerCard)
        {
            try
            {
                //Checks if cards are a pair, then evaluates if hand should be split or double down
                if (blackjackHand.Cards[0].CardRank == blackjackHand.Cards[1].CardRank)
                {
                    if (blackjackHand.Cards[0].CardRank == Rank.Eight) blackjackHand.SetAction(HandActions.Split);
                    else if (blackjackHand.Cards[0].CardRank == Rank.Ace) blackjackHand.SetAction(HandActions.Split);
                    else if (blackjackHand.Cards[0].CardRank == Rank.Three && (dealerCard.CardRank >= Rank.Four && dealerCard.CardRank >= Rank.Seven)) blackjackHand.SetAction(HandActions.Split);
                    else if (blackjackHand.Cards[0].CardRank == Rank.Two && (dealerCard.CardRank >= Rank.Three && dealerCard.CardRank >= Rank.Seven)) blackjackHand.SetAction(HandActions.Split);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SplitHand(List<BlackjackHand> blackJackHands, BlackjackHand blackjackHand)
        {
            try
            {
                //Creates two hands, splits the cards in the initial hands list into the new hands, clears current list and adds the new hands
                BlackjackHand splitHand0 = new BlackjackHand();
                BlackjackHand splitHand1 = new BlackjackHand();

                splitHand0.Cards.Add(blackjackHand.Cards[0]);
                splitHand1.Cards.Add(blackjackHand.Cards[1]);

                blackJackHands.RemoveAll(h => h.HandId == blackjackHand.HandId);

                blackJackHands.Add(splitHand0);
                blackJackHands.Add(splitHand1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void GetAction(BlackjackHand blackjackHand, Card dealerCard)
        {
            try
            {
                GetHandValues(blackjackHand);

                if (blackjackHand.Action == HandActions.FlipBust || blackjackHand.Action == HandActions.FlipBlackjack)
                {
                    //exits method if busted or blackjack is scored
                    return;
                }
                else
                {
                    //Returns action for hard hand
                    if (!blackjackHand.IsSoft)
                    {
                        if (blackjackHand.HardValue <= 11) blackjackHand.SetAction(HandActions.Hit);
                        else if ((blackjackHand.HardValue >= 12 && blackjackHand.HardValue <= 16) && dealerCard.BlackjackValue >= 7) blackjackHand.SetAction(HandActions.Hit);
                        else blackjackHand.SetAction(HandActions.Stand);
                    }

                    //Returns action for soft hand
                    if (blackjackHand.IsSoft)
                    {
                        if (blackjackHand.HardValue >= 19 && blackjackHand.HardValue <= 21)
                        {
                            blackjackHand.SetAction(HandActions.Stand);
                        }
                        else if (blackjackHand.HardValue == 18)
                        {
                            if (dealerCard.BlackjackValue == 2) blackjackHand.SetAction(HandActions.Stand);
                            else if (dealerCard.BlackjackValue == 7 || dealerCard.BlackjackValue == 8) blackjackHand.SetAction(HandActions.Stand);
                            else blackjackHand.SetAction(HandActions.Hit);
                        }
                        else if (blackjackHand.HardValue == 17)
                        {
                            blackjackHand.SetAction(HandActions.Stand);
                        }
                        else if (blackjackHand.HardValue < 17)
                        {
                            blackjackHand.SetAction(HandActions.Hit);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void HandleDealer(BlackjackHand blackjackHand)
        {
            GetHandValues(blackjackHand);
            //Dealer handling, essentially if dealers doesnt have a score of 17+ they must hit
            if (blackjackHand.Action == HandActions.FlipBust || blackjackHand.Action == HandActions.FlipBlackjack)
            {
                return;
            }
            else
            {
                if (blackjackHand.IsSoft)
                {
                    if (blackjackHand.SoftValue >= 17 && blackjackHand.SoftValue <= 21)
                    {
                        blackjackHand.SetAction(HandActions.Stand);
                    }
                    else
                    {
                        blackjackHand.SetAction(HandActions.Hit);
                    }
                }
                else
                {
                    if (blackjackHand.HardValue >= 17)
                    {
                        blackjackHand.SetAction(HandActions.Stand);
                    }
                    else
                    {
                        blackjackHand.SetAction(HandActions.Hit);
                    }
                }
            }
        }



        internal static bool EvaluateDoubleDown(BlackjackHand blackjackHand, Card dealerCard)
        {
            //Double down conditions
            if ((blackjackHand.HardValue == 9 && dealerCard.BlackjackValue >= 3 && dealerCard.BlackjackValue <= 6)
                    || (blackjackHand.HardValue == 10 && dealerCard.BlackjackValue >= 2 && dealerCard.BlackjackValue <= 9)
                    || (blackjackHand.HardValue == 11 && dealerCard.BlackjackValue >= 2 && dealerCard.BlackjackValue <= 10))
            {
                blackjackHand.SetAction(HandActions.DoubleDown);
                return true;
            }

            return false;
        }

        internal static void GetHandValues(BlackjackHand blackjackHand)
        {
            try
            {
                if (blackjackHand.HardValue == 0)
                {
                    //Calcualte the initial hand value, checks if the hand is soft by looking for a an ace, sets the soft value
                    //if it is and set the hand to soft
                    foreach (Card blackjackCard in blackjackHand.Cards)
                    {
                        blackjackHand.UpdateHardValue(blackjackCard.BlackjackValue);
                    }

                    if (blackjackHand.Cards.Any(c => c.CardRank == Rank.Ace))
                    {
                        blackjackHand.UpdateSoftValue(blackjackHand.HardValue - 10);
                        blackjackHand.SetAsSoft(true);
                    }
                }
                else
                {
                    //Dealt card management
                    if (blackjackHand.Cards.Last().CardRank == Rank.Ace)
                    {
                        //if the card was an ace and the hard value is over 21 then add the aces soft value to the hard value
                        if (blackjackHand.HardValue + blackjackHand.Cards.Last().BlackjackValue > 21)
                        {
                            blackjackHand.UpdateHardValue(1);
                        }
                        else
                        {
                            //if it isnt over 21 then update both soft and hard values accordingly and ensure the handi is flagged as soft
                            blackjackHand.UpdateHardValue(11);
                            blackjackHand.UpdateSoftValue(-10);
                            blackjackHand.SetAsSoft(true);
                        }
                    }
                    else
                    {
                        //If it isnt an ace just calculate both values
                        blackjackHand.UpdateHardValue(blackjackHand.Cards.Last().BlackjackValue);
                        if (blackjackHand.IsSoft)
                        {
                            blackjackHand.UpdateSoftValue(blackjackHand.Cards.Last().BlackjackValue);
                        }
                    }
                }

                EvaluateHand(blackjackHand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static void EvaluateHand(BlackjackHand blackjackHand)
        {
            //Blackjack
            if (blackjackHand.HardValue == 21 || blackjackHand.SoftValue == 21)
            {
                blackjackHand.SetAction(HandActions.FlipBlackjack);
            }

            //Bust
            if (blackjackHand.HardValue > 21 && !blackjackHand.IsSoft)
            {
                blackjackHand.SetAction(HandActions.FlipBust);
            }

            //Bust soft value
            if (blackjackHand.SoftValue > 21 && blackjackHand.IsSoft)
            {
                blackjackHand.SetAction(HandActions.FlipBust);
            }
        }
    }
}
