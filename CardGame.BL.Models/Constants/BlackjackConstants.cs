using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Constants
{
    public static class BlackjackConstants
    {
        public enum CardValues
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 10,
            Queen = 10,
            King = 10,
            Ace = 11
        }
        public enum PlayerStatus
        {
            Split,
            Busted,
            Held,
            Blackjack
        }
        public enum PlayerActions
        {
            Thinking,
            Hit,
            Stand,
            Split,
            DoubleDown,
            Insurance,
            Surrender,
            FlipBust,
            FlipBlackjack
        }
    }
    
}
