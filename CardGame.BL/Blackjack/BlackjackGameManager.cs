using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.BlackJack
{
    public class BlackjackGameManager
    {
        public BlackjackGame Game { get; set; }

        public BlackjackGameManager(int players, int balance) 
        {
            Game = new BlackjackGame(players, balance);
            StartGame();
        }

        private void StartGame()
        {
            int playerTurn = 0;
            int playerCount = 0;
            while (Game.Players.Count > 0)
            {
                if (playerTurn >= Game.Players.Count)
                {
                    playerTurn = 0;

                    PlayerMove();

                    if (Game.Players[playerTurn].Balance == 0)
                    {
                        Game.Players.RemoveAt(playerTurn);
                    }
                }

                playerTurn++;
            }
        }

        private void PlayerMove()
        {
            throw new NotImplementedException();
        }
    }
}
