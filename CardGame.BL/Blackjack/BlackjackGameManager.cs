using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.BlackJack
{
    public class BlackjackGameManager
    {
        private BlackjackGame game;
        public BlackjackGameManager()
        {
        }

        public BlackjackGameManager(int players, int balance) 
        {
            game = new BlackjackGame(players, balance);

            StartGame();
        }

        private void StartGame()
        {
            int playerTurn = 0;
            int playerCount = 0;
            int roundCount = 0;
            bool betsRetrieved = false;
            bool roundActive = false;
            game.DealHands();

            //while (game.Players.Count > 0)
            //{
                
            //    if (roundActive)
            //    {
            //        if (playerTurn >= game.Players.Count)
            //        {
            //            playerTurn = 0;


            //            if (game.Players[playerTurn].Balance == 0)
            //            {
            //                game.Players.RemoveAt(playerTurn);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        foreach (BlackjackPlayer player in game.Players)
            //        {
            //            if (player.IsHuman)
            //            {

            //            }
            //            else if (!player.IsDealer) 
            //            {
                            
            //            }
            //            game.DealHands();
            //        }
            //    }

                

            //    playerTurn++;
            //}
        }
    }
}
