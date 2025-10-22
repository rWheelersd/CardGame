using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Interfaces;
using System.Collections.Generic;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.BaseModels
{
    /*
     * Deck Class
     * Deck requires generic parameter constraints as every gametype can have a card/hand unique to itself,
     * So in order to beable to deal cards/hands to a specific gametype/hand it must remain entirely generic.
     */

    public class Deck<THand>
    where THand : IHand<Card>, new()
    {
        private readonly Random _rng;
        public List<Card> Cards { get; private set; }
        public List<Card> BurntCards { get; private set; }

        public Deck()
        {
            this._rng = new Random();
            this.Cards = new List<Card>();
            this.BurntCards = new List<Card>();
            BuildDeck();
            ShuffleCards(Cards);
        }

        private void BuildDeck()
        {
            try
            {
                foreach (Enum suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Enum rank in Enum.GetValues(typeof(Rank)))
                    {
                        //Simply cycles through all enums of suit and rank, instantiates a card given
                        //the current indexes in the loops and adds to the decks list of cards
                        this.Cards.Add((Card)Activator.CreateInstance(typeof(Card), rank, suit));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ShuffleCards(List<Card> cards)
        {
            try
            {
                //Winds down from the last card in the deck.
                //Grabs the random card and stores its index based on remaining cards to shuffle
                //Instantiate a generic card and assign it the card of the random index
                //Swaps that card with card[n] (which will be the index of the card we are currently on in the loop
                //meaning the last card in the deck is assigned to the card at the randomly generated index
                //and the last card in the deck becomes the card randomly generated
                int n = this.Cards.Count;
                while (n > 1)
                {
                    n--;
                    int i = this._rng.Next(n + 1);
                    Card card = this.Cards[i];
                    this.Cards[i] = this.Cards[n];
                    this.Cards[n] = card;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DealCards(THand hand)
        {
            try
            {
                //Adds the first card of the deck to the hand passed in
                //Adds it to the burnt pile
                //Removes that card from the deck
                if (this.Cards.Count <= 0)
                {
                    ResetDeck();
                }
                var card = this.Cards.First();
                hand.Cards.Add(card);
                this.BurntCards.Add(card);
                this.Cards.RemoveAt(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public THand DealHand(int handSize)
        {
            try
            {
                if (Cards.Count <= 0)
                {
                    ResetDeck();
                }

                //Deals a hand given the hand size, doing exactly what DealCard does in interation
                var hand = new THand();
                for (int i = 0; i < handSize; i++)
                {
                    if (Cards.Any())
                    {
                        var card = this.Cards.First();
                        hand.Cards.Add(card);
                        this.BurntCards.Add(card);
                        this.Cards.RemoveAt(0);
                    }
                    else
                    {
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

        public void AddBurned()
        {
            try
            {
                ShuffleCards(this.BurntCards);
                this.Cards.AddRange(this.BurntCards);
                this.BurntCards.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ResetDeck()
        {
            try
            {
                this.Cards.AddRange(this.BurntCards);
                ShuffleCards(this.Cards);
                this.BurntCards.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}