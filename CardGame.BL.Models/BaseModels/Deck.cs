using static CardGame.BL.Models.Constants.BaseConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CardGame.BL.Models.Blackjack;
using System.Reflection;

namespace CardGame.BL.Models.BaseModels
{
    public class Deck<TCard> where TCard : Card<TCard>, new()
    {
        public List<TCard> Cards { get; set; }
        public List<TCard> BurntCards { get; set; }


        public Deck()
        {
            Cards = new List<TCard>();
            BurntCards = new List<TCard>();
            BuildDeck();
        }

        private void BuildDeck()
        {
            try
            {
                foreach (Enum suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Enum rank in Enum.GetValues(typeof(Rank)))
                    {
                        Cards.Add(new TCard { CardRank = (Rank)rank, CardSuit = (Suit)suit, CardName = $"{rank}_of_{suit}" });
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TCard> RebuildDeck(List<TCard> gameDeck)
        {
            return Cards;
        }

        public void ShuffleDeck(Deck<TCard> gameDeck)
        {
            try
            {
                Random rng = new Random();
                int i = gameDeck.Cards.Count;

                while (i > 1)
                {
                    i--;
                    int n = rng.Next(i + 1);
                    TCard card = gameDeck.Cards[n];
                    gameDeck.Cards[n] = gameDeck.Cards[i];
                    gameDeck.Cards[i] = card;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public Card DealCard(Deck<TCard> gameDeck)
        //{
        //    return Cards;
        //}

        public Hand<TCard> DealCards(GameType gameType, Deck<TCard> gameDeck)
        {
            try
            {
                int handSize = new int();
                Random rng = new Random();
                Hand<TCard> hand = new Hand<TCard>();

                if (gameType == GameType.Blackjack)
                {
                    handSize = 2;
                }

                while (handSize > 0)
                {
                    int card = rng.Next(gameDeck.Cards.Count);
                    hand.Cards.Add(gameDeck.Cards[card]);
                    //RemoveCard(card1, gameDeck);
                    gameDeck.BurntCards.Add(gameDeck.Cards[card]);
                    gameDeck.Cards.RemoveAt(card);
                    handSize--;
                }



                return hand;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public void RemoveCard(int index, Deck<TCard> gameDeck)
        //{
        //    try
        //    {
        //        gameDeck.BurntCards.Add(gameDeck.Cards[index]);
        //        gameDeck.Cards[index] = gameDeck.Cards[gameDeck.Cards.Count - 1];
        //        gameDeck.Cards.Remove(gameDeck.Cards[gameDeck.Cards.Count - 1]);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
