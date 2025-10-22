using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.TexasHoldemConstants;

namespace CardGame.BL.Models.TexasHoldem
{
    internal class TexasHoldemGame : Game<TexasHoldemHand, TexasHoldemPlayer>
    {
        public TexasHoldemHand CommunityCards { get; set; }
        public GamePhase Phase { get; set; }
        public TurnState TurnState { get; set; }
        public int Pot { get; set; }

        public TexasHoldemGame(Guid gameId, int numberOfPlayers, int humanPlayers, int startingBalance) : base(gameId, numberOfPlayers, humanPlayers, startingBalance)
        {

        }

        public void StartRound()
        {
            throw new NotImplementedException();
        }
    }
}
