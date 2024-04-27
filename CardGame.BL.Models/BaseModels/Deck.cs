using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Interfaces;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.BaseModels
{
    /*
     * Deck Class
     * Deck requires generic parameter constraints as every gametype can have a card/hand unique to itself,
     * So in order to beable to deal cards/hands to a specific gametype/hand it must remain entirely generic.
     */

    public class Deck<TCard, THand>
    where TCard : Card
    where THand : IHand<TCard>, new()
    {
        private readonly Random _rng;
        public List<TCard> Cards { get; set; }
        public List<TCard> BurntCards { get; set; }

        public Deck()
        {
            _rng = new Random();
            Cards = new List<TCard>();
            BurntCards = new List<TCard>();
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
                        //Simply cycles through all enums of suit and rank, instantiates a card given
                        //the current indexes in the loops and adds to the decks list of cards
                        Cards.Add((TCard)Activator.CreateInstance(typeof(TCard), rank, suit));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ShuffleDeck()
        {
            try
            {
                //Winds down from the last card in the deck.
                //Grabs the random card and stores its index based on remaining cards to shuffle
                //Instantiate a generic card and assign it the card of the random index
                //Swaps that card with card[n] (which will be the index of the card we are currently on in the loop
                //meaning the last card in the deck is assigned to the card at the randomly generated index
                //and the last card in the deck becomes the card randomly generated
                int n = Cards.Count;
                while (n > 1)
                {
                    n--;
                    int i = _rng.Next(n + 1);
                    TCard card = Cards[i];
                    Cards[i] = Cards[n];
                    Cards[n] = card;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DealCard(THand hand)
        {
            try
            {
                //Adds the first card of the deck to the hand passed in
                //Adds it to the burnt pile
                //Removes that card from the deck
                var card = Cards.First();
                hand.Cards.Add(card);
                BurntCards.Add(card);
                Cards.RemoveAt(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public THand DealCards(int handSize)
        {
            try
            {
                //Deals a hand given the hand size, doing exactly what DealCard does in interation
                var hand = new THand();
                for (int i = 0; i < handSize; i++)
                {
                    if (Cards.Any())
                    {
                        var card = Cards.First();
                        hand.Cards.Add(card);
                        BurntCards.Add(card);
                        Cards.RemoveAt(0);
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
    }
}

