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
    /*
     * Card class
     * Each card is assigned its rank, suit, and name on construction.
     * Card class is intended to be inhereted as specific games can have
     * card actions or properties unique to a game type.
     */
    public class Card
    {
        public string CardName { get; set; }
        public string ImageName { get; private set; }
        public Rank CardRank { get; set; }
        public Suit CardSuit { get; set; }

        public Card(Rank rank, Suit suit)
        {
            CardRank = rank;
            CardSuit = suit;
            CardName = $"{rank} of {suit}";
            ImageName = $"{rank}_of_{suit}.png".ToLower();
        }
    }
}


