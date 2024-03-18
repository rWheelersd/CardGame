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
    public class BlackjackPlayerManager
    {
        internal PlayerActions PlayTurn(BlackjackPlayer player, BlackjackCard dealerCard)
        {
            try
            {
                //Checks if hand has ace to determine if hand is soft
                bool isSoft = player.Hand.Cards.Any(c => c.CardRank == Rank.Ace);

                return EvaluateHand(player, dealerCard, isSoft);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void PlayerBet(BlackjackPlayer currentPlayer, int minBet, int maxBet, Random rng)
        {
            try
            {
                int maxCapable = maxBet;

                if (currentPlayer.Balance < maxBet)
                {
                    maxCapable = currentPlayer.Balance;
                }

                currentPlayer.Bet = rng.Next(minBet, maxCapable + 1);

                currentPlayer.Balance = currentPlayer.Balance - currentPlayer.Bet;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private PlayerActions EvaluateHand(BlackjackPlayer player, BlackjackCard dealerCard, bool isSoft)
        {
            try
            {
                //Checks if is starting hand by card cound and player status
                if (player.Hand.Cards.Count == 2 && !player.WasSplit)
                {
                    //Checks if hand is pair
                    if (player.Hand.Cards[0] == player.Hand.Cards[1])
                    {
                        //Condition check for if computer should split or double down
                        if (EvaluateSplit(player.Hand.Cards[0], dealerCard) == PlayerActions.Split)
                        {
                            player.WasSplit = true;
                            player.Status = PlayerStatus.Split;
                            return PlayerActions.Split;
                        }
                        if (EvaluateSplit(player.Hand.Cards[0], dealerCard) == PlayerActions.DoubleDown)
                        {
                            player.WasSplit = true;
                            player.Status = PlayerStatus.Doubled;
                            return PlayerActions.DoubleDown;
                        }
                        if (EvaluateSplit(player.Hand.Cards[0], dealerCard) == PlayerActions.Thinking)
                        {
                            return GetAction(player.Hand, dealerCard);
                        }
                    }
                }

                //Plays a split computer
                if (player.WasSplit)
                {
                    if (!player.Hand.splitBust)
                    {
                        return GetAction(player.Hand, dealerCard, PlayerStatus.Split);
                    }
                    if (!player.SplitHand.splitBust)
                    {
                        return GetAction(player.SplitHand, dealerCard);
                    }
                }

                //Plays computer
                return GetAction(player.Hand, dealerCard);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private PlayerActions EvaluateSplit(BlackjackCard playerCard, BlackjackCard dealerCard)
        {
            try
            {
                //Determines if player splits or doubles down
                if (playerCard.CardValue == 11) return PlayerActions.Split;
                if (playerCard.CardValue == 8) return PlayerActions.Split;
                if (playerCard.CardValue == 5 && dealerCard.CardValue <= 9) return PlayerActions.DoubleDown;
                if (playerCard.CardValue == 3 && (dealerCard.CardValue >= 4 && dealerCard.CardValue <= 7)) return PlayerActions.Split;
                if (playerCard.CardValue == 2 && (dealerCard.CardValue >= 3 && dealerCard.CardValue <= 7)) return PlayerActions.Split;
                else return PlayerActions.Thinking;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private PlayerActions GetAction(BlackjackHand playerHand, BlackjackCard dealerCard, PlayerStatus status = PlayerStatus.Active)
        {
            try
            {
                int hardValue = 0;
                int softValue = 0;


                //Calculate hands hard value
                foreach (BlackjackCard card in playerHand.Cards)
                {
                    hardValue += card.CardValue;
                }

                //Checks if hand is soft and sets a soft value
                if (playerHand.Cards.Any(c => c.CardRank == Rank.Ace))
                {
                    softValue = hardValue - 10;
                    playerHand.IsSoft = true;

                    //if soft value is busted then hard value must also be busted
                    if (softValue > 21)
                    {
                        return PlayerActions.FlipBust;
                    }
                }

                //Blackjack if either value is 21
                if (hardValue == 21 || softValue == 21)
                {
                    return PlayerActions.FlipBlackjack;
                }

                //Bust conditions for a hard hand
                if (hardValue > 21 && !playerHand.IsSoft)
                {
                    //If player is split on first hand the first hand is busted
                    if (status == PlayerStatus.Split)
                    {
                        //Might need to change this based how i decide to handle the return
                        playerHand.splitBust = true;
                        return PlayerActions.FlipBust;
                    }

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

                //Returns action for hard hand
                if (!playerHand.IsSoft)
                {
                    if (hardValue <= 11) return PlayerActions.Hit;

                    if ((hardValue >= 12 && hardValue <= 16) && dealerCard.CardValue >= 7) return PlayerActions.Hit;
                }

                return PlayerActions.Stand;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
