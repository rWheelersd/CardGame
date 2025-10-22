using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.TexasHoldem
{
    internal class TexasHoldemPlayer : Player<TexasHoldemHand>
    {
        public PlayerStatus Status { get; private set; }
        public int Score { get; private set; }

        public TexasHoldemPlayer() : base(0, 0)
        {

        }

        public TexasHoldemPlayer(int playerNumber, int balance) : base(playerNumber, balance)
        {

        }

        public void SetPlayerStatus(PlayerStatus status)
        {
            this.Status = status;
        }

        public void SetScore(int score)
        {
            this.Score = score;
        }
    }
}
