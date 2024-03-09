using CardGame.BL.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.BaseModels
{
    public class Card<TCard>
    {
        public string ImgPath { get; set; }
        public string CardName { get; set; }
        public Rank CardRank { get; set; }
        public Suit CardSuit { get; set; }

        public Card()
        {
        }

        public Card(Rank rank, Suit suit)
        {
            CardRank = rank;
            CardSuit = suit;
        }
    }
}
