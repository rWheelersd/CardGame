using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Constants;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackCard : Card
    {
        public bool IsVisible { get; set; }
        public int CardValue { get; private set; }
        public BlackjackCard(Rank rank, Suit suit) : base(rank, suit)
        {
            SetValue(this.CardRank);
        }

        public void SetValue(Rank rank)
        {
            switch (CardRank)
            {
                case Rank.Two:
                    CardValue = (int)CardValues.Two;
                    break;

                case Rank.Three:
                    CardValue = (int)CardValues.Three;
                    break;

                case Rank.Four:
                    CardValue = (int)CardValues.Four;
                    break;

                case Rank.Five:
                    CardValue = (int)CardValues.Five;
                    break;

                case Rank.Six:
                    CardValue = (int)CardValues.Six;
                    break;

                case Rank.Seven:
                    CardValue = (int)CardValues.Seven;
                    break;

                case Rank.Eight:
                    CardValue = (int)CardValues.Eight;
                    break;

                case Rank.Nine:
                    CardValue = (int)CardValues.Nine;
                    break;

                case Rank.Ten:
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    CardValue = (int)CardValues.Ten;
                    break;

                case Rank.Ace:
                    CardValue = (int)CardValues.Ace;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
