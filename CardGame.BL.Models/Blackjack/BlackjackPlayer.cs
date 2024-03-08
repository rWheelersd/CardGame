using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Constants;
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
    public class BlackjackPlayer : Player<BlackjackCard>
    {
        public bool IsDealer = false;
        public BlackjackPlayer(int balance, int playerNumber) : base(balance, playerNumber)
        {

        }
    }
}
