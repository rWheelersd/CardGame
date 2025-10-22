using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.BaseModels
{
    /*
     * Card class
     * Each card is assigned its rank, suit, and name on construction.
     * Card class is intended to be inhereted as specific games can have
     * card actions or properties unique to a game type.
     */
    public class Card
    {
        public string CardName { get; private set; }
        public bool IsVisible { get; private set; }
        public string ImageName { get; private set; }
        public Rank CardRank { get; private set; }
        public Suit CardSuit { get; private set; }
        public int BlackjackValue { get; private set; }

        public Card(Rank rank, Suit suit)
        {
            this.CardRank = rank;
            this.CardSuit = suit;
            this.CardName = $"{rank} of {suit}";
            this.ImageName = $"{rank}_of_{suit}.png".ToLower();
        }

        public void RevealCard()
        {
            this.IsVisible = true;
        }

        public void SetBlackjackValue()
        {
            switch (this.CardRank)
            {
                case Rank.Two:
                    this.BlackjackValue = (int)CardValues.Two;
                    break;

                case Rank.Three:
                    this.BlackjackValue = (int)CardValues.Three;
                    break;

                case Rank.Four:
                    this.BlackjackValue = (int)CardValues.Four;
                    break;

                case Rank.Five:
                    this.BlackjackValue = (int)CardValues.Five;
                    break;

                case Rank.Six:
                    this.BlackjackValue = (int)CardValues.Six;
                    break;

                case Rank.Seven:
                    this.BlackjackValue = (int)CardValues.Seven;
                    break;

                case Rank.Eight:
                    this.BlackjackValue = (int)CardValues.Eight;
                    break;

                case Rank.Nine:
                    this.BlackjackValue = (int)CardValues.Nine;
                    break;

                case Rank.Ten:
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    this.BlackjackValue = (int)CardValues.Ten;
                    break;

                case Rank.Ace:
                    this.BlackjackValue = (int)CardValues.Ace;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}