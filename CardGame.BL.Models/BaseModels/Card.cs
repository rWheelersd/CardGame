using CardGame.BL.Models.Constants;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.BaseModels
{
    public class Card
    {
        public string CardName { get; set; }
        public Rank CardRank { get; set; }
        public Suit CardSuit { get; set; }

        public Card(Rank rank, Suit suit)
        {
            CardRank = rank;
            CardSuit = suit;
            CardName = $"{CardRank} of {CardSuit}";
        }
    }
}
