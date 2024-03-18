using CardGame.BL.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackHand : Hand<BlackjackCard>
    {
        public bool IsSoft = false;
        public bool SoftBust = false;

        public BlackjackHand(int handSize) : base(handSize)
        {

        }
    }
}
