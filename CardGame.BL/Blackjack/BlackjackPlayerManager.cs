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
                bool isSoft = player.Hand.Cards.Any(c => c.CardRank == Rank.Ace);
                return EvaluateHand(player, dealerCard, isSoft);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void PlayerBet(BlackjackPlayer currentPlayer, int minBet, int maxBet)
        {
            try
            {
                int maxCapable = maxBet;
                Random rng = new Random();

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
                if (player.Hand.Cards.Count == 2 && player.Status != PlayerStatus.Split)
                {
                    if (player.Hand.Cards[0] == player.Hand.Cards[1])
                    {
                        if (EvaluateSplit(player.Hand.Cards[0], dealerCard) == PlayerActions.Split)
                        {
                            player.Status = PlayerStatus.Split;
                            return PlayerActions.Split;
                        }
                        if (EvaluateSplit(player.Hand.Cards[0], dealerCard) == PlayerActions.DoubleDown)
                        {
                            return PlayerActions.DoubleDown;
                        }
                    }
                }

                //if (player.Status == PlayerStatus.Split)
                //{

                //}

                GetAction(player.Hand, dealerCard);
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

        private PlayerActions GetAction(BlackjackHand playerHand, BlackjackCard dealerCard)
        {
            try
            {
                int hardValue = 0;
                int softValue = 0;

                //Blackjack
                if (hardValue == 21)
                {
                    return PlayerActions.FlipBlackjack;
                }

                //Bust
                if (hardValue > 21)
                {

                    return PlayerActions.FlipBust;
                }

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
                }

                //Returns action for hard hand
                if (!playerHand.IsSoft)
                {
                    if (hardValue == 16 && (dealerCard.CardValue >= 11))
                    {
                        return PlayerActions.Surrender;
                    }

                    if (hardValue <= 11) return PlayerActions.Hit;

                    if ((hardValue >= 12 && hardValue <= 16) && dealerCard.CardRank >= Rank.Seven) return PlayerActions.Hit;

                    else return PlayerActions.Stand;
                }

                if (playerHand.IsSoft && !playerHand.SoftBust)
                {
                    if (hardValue <= 17)
                    {

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
