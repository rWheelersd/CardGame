using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackHand : IHand<BlackjackCard>
    {
        public List<BlackjackCard> Cards { get; set; }
        public HandActions Action { get; set; }

        public BlackjackHand()
        {
            Cards = new List<BlackjackCard>();
        }
    }
}
