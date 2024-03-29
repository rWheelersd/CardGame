using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Constants
{
    public static class BaseConstants
    {
        public enum Rank
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
        }

        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }
        public enum GameType
        {
            Blackjack,
            TexasHoldem
        }
    }
}
