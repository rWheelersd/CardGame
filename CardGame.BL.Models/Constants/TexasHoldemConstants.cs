using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Constants
{
    public static class TexasHoldemConstants
    {
        public enum Score
        {
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeOfKind = 4,
            Straight = 5,
            Flush = 6,
            FullHouse = 7,
            FourOfKind = 8,
            StraightFlush = 9,
            RoyalFlush = 10
        }
        public enum GamePhase
        {
            PreFlop,
            Flop,
            Turn,
            River,
            EndRound
        }

        public enum TurnState
        {
            Check,
            Bet,
            Call,
            Start
        }

        public enum PlayerStatus
        {
            Active,
            Inactive
        }
    }
}
