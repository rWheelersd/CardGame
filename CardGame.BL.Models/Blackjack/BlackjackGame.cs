using CardGame.BL.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackGame : Game<BlackjackPlayer, BlackjackHand, BlackjackCard>
    {
        public BlackjackGame(int playerCount, int startingBalance) : base(playerCount, startingBalance)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            try
            {
                GameDeck.ShuffleDeck(GameDeck);
                DealHands();
            }
            catch (Exception)
            { 

                throw;
            }
        }

        private void DealHands()
        {
            try
            {
                foreach (BlackjackPlayer player in Players)
                {
                    player.Hand = GameDeck.DealCards(Games.Blackjack, GameDeck);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
