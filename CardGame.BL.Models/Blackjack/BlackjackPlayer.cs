using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Constants;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackPlayer : Player<BlackjackHand>
    {
        public PlayerStatus Status { get; set; }

        public BlackjackPlayer() : base(0, 0)
        {

        }

        public BlackjackPlayer(int playerNumber, int balance) : base(playerNumber, balance)
        {

        }

        public void SetPlayerStatus(PlayerStatus status)
        {
            this.Status = status;
        }
    }
}