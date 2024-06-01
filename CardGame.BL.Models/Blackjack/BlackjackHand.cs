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
    public class BlackjackHand : IHand<Card>
    {
        public Guid HandId { get; private set; }
        public List<Card> Cards { get; set; }
        public HandActions Action { get; private set; }
        public bool WasSplitEvaluated { get; private set; }
        public bool WinningHand { get; private set; }
        public bool IsSoft { get; private set; }
        public int SoftValue { get; private set; }
        public int HardValue { get; private set; }

        public BlackjackHand()
        {
            this.HandId = Guid.NewGuid();
            this.Cards = new List<Card>();
            this.Action = HandActions.Thinking;
            this.WasSplitEvaluated = false;
            this.WinningHand = false;
            this.IsSoft = false;
            this.SoftValue = 0;
            this.HardValue = 0;
        }

        public bool IsHandResolved()
        {
            return Action == HandActions.FlipBlackjack || Action == HandActions.FlipBust || Action == HandActions.Stand || Action == HandActions.DoubleDown;
        }

        public void SetAction(HandActions action)
        {
            this.Action = action;
        }

        public void SetSplitEvaluation(bool evaluation)
        {
            this.WasSplitEvaluated = evaluation;
        }

        public void SetWinStatus(bool winStatus)
        {
            this.WinningHand = winStatus;
        }

        public void SetAsSoft(bool isSoft)
        {
            this.IsSoft = isSoft;
        }

        public void UpdateSoftValue(int softValue)
        {
            this.SoftValue += softValue;
        }

        public void UpdateHardValue(int hardValue)
        {
            this.HardValue += hardValue;
        }
    }
}