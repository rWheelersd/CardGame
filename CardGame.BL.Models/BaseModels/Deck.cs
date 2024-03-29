using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Interfaces;
using static CardGame.BL.Models.Constants.BaseConstants;

public class Deck<TCard, THand> where TCard : Card where THand : IHand<TCard>, new()
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
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int i = _rng.Next(n + 1);
                TCard card = Cards[i];
                Cards[i] = Cards[n];
                Cards[n] = pCard;
            }
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
