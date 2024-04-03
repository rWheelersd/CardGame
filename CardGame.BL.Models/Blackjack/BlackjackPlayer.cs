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
        public string Username { get; set; }
        public bool IsDealer { get; set; }
        public bool IsHuman { get; set; }
        public bool SplitEvaluated { get; set; }
        public PlayerStatus Status { get; set; }
        public List<BlackjackHand> Hands {  get; set; } 

        public BlackjackPlayer() : base(0, 0)
        {

        }

        public BlackjackPlayer(int playerNumber, int balance) : base(playerNumber, balance)
        {
            
        }
    }
}
