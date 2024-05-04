using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackHand : IHand<BlackjackCard>
    {
        public Guid handId { get; private set; }
        public List<BlackjackCard> Cards { get; set; }
        public HandActions Action { get; set; }
        public bool WasSplitEvaluated { get; set; }
        public bool WinningHand { get; set; }
        public bool IsSoft {  get; set; }
        public int SoftValue { get; set; }
        public int HardValue { get; set; }

        public BlackjackHand()
        {
            handId = Guid.NewGuid();
            Cards = new List<BlackjackCard>();
            Action = HandActions.Thinking;
            WasSplitEvaluated = false;
            WinningHand = false;
            IsSoft = false;
            SoftValue = 0;
            HardValue = 0;
        }

        public bool IsHandResolved()
        {
           return Action == HandActions.FlipBlackjack || Action == HandActions.FlipBust || Action == HandActions.Stand;
        }
    }
}
