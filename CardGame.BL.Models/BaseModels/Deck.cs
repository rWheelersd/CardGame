using static CardGame.BL.Models.Constants.BaseConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CardGame.BL.Models.Blackjack;
using System.Reflection;
using CardGame.BL.Models.Interfaces;

namespace CardGame.BL.Models.BaseModels
{
    public class Deck<T> : IDeck<T> where T : ICard
    {
        private readonly Random _rng;
        public List<T> Cards { get; set; }
        public List<T> BurntCards { get; set; }


        public Deck()
        {
            _rng = new Random();
            Cards = new List<T>();
            BurntCards = new List<T>();
            BuildDeck();
        }

        public void BuildDeck()
        {
            try
            {
                foreach (Enum suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Enum rank in Enum.GetValues(typeof(Rank)))
                    {
                        Cards.Add((T)Activator.CreateInstance(typeof(T), rank, suit));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<TCard> RebuildDeck(List<TCard> gameDeck)
        //{
        //    return Cards;
        //}

        public void ShuffleDeck()
        {
            try
            {
                int n = Cards.Count;
                while (n > 1)
                {
                    n--;
                    int k = _rng.Next(n + 1);
                    T value = Cards[k];
                    Cards[k] = Cards[n];
                    Cards[n] = value;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IHand<T> DealCards(int handSize)
        {
            try
            {
                var hand = new Hand<T>();
                for (int i = 0; i < handSize; i++)
                {
                    if (Cards.Any())
                    {
                        var card = Cards.First();
                        hand.Cards.Add(card);
                        BurntCards.Add(card);
                        Cards.Remove(card);
                    }
                    else
                    {
                        // Handle empty deck scenario
                        break;
                    }
                }
                return hand;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
