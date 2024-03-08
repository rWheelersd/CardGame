using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Constants
{
    public static class BlackjackConstants
    {
        public enum PlayerStatus
        {
            Active,
            Busted,
            Held
        }
        public enum PlayerActions
        {
            Thinking,
            Hit,
            Stand,
            Split,
            DoubleDown,
            Insurance,
            Surrender
        }
    }
    
}
