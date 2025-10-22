using CardGame.BL.BlackjackManagers;
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGameConsole.UI
{
    internal class PlayBlackjack
    {
        private BlackjackGameManager gameManager;
        public PlayBlackjack(Guid gameId, int playerCount, int humanCount, int startingBalance)
        {
            this.gameManager = new BlackjackGameManager(gameId, playerCount, humanCount, startingBalance);
        }

        internal void PlayGame()
        {
            try
            {
                while (gameManager.BlackjackGame.Players.Any(p => !p.IsDealer))
                {
                    gameManager.StartRound();
                    PlayHumans();
                    gameManager.ProcessAI();
                    gameManager.ManagePayouts();

                    List<string> results = BlackjackPlayerManager.GetPlayerResults(gameManager.BlackjackGame.Players);

                    foreach (string result in results)
                    {
                        Console.WriteLine(result);
                    }

                    gameManager.ResetGame();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PlayHumans()
        {
            foreach (BlackjackPlayer blackjackPlayer in gameManager.BlackjackGame.Players)
            {
                if (!blackjackPlayer.IsDealer || blackjackPlayer.IsHuman)
                {
                    gameManager.ProcessHuman(blackjackPlayer);
                }
            }
        }
    }
}
