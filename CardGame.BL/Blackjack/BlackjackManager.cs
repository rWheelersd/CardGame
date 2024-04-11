using CardGame.BL.BlackJack;
using CardGame.BL.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Blackjack
{
    public class BlackjackManager
    {
        Dictionary<Guid, BlackjackGameManager> blackjackGameManagers;

        public BlackjackManager()
        {
            blackjackGameManagers = new Dictionary<Guid, BlackjackGameManager>();
        }

        public Guid CreateGame(int playerCount, int startingBalance)
        {
            try
            {
                Guid gameId = Guid.NewGuid();
                //blackjackGameManagers.Add(gameId, new BlackjackGameManager(gameId, playerCount, startingBalance));
                return gameId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Guid StartGame(int playerCount, int startingBalance)
        {
            try
            {
                Guid gameId = Guid.NewGuid();
                return gameId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Guid> GetOpenGames()
        {
            try
            {
                return new List<Guid>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int JoinGame(Guid gameManagerId, Guid playerId, string username)
        {
            try
            {
                if (blackjackGameManagers.ContainsKey(gameManagerId) 
                    && blackjackGameManagers[gameManagerId].BlackjackGame.Players.Any(p => p.IsHuman == false))
                {
                    blackjackGameManagers[gameManagerId]
                        .BlackjackGame
                        .Players.First(p  => p.IsHuman == false)
                        .Id = playerId;


                    blackjackGameManagers[gameManagerId].BlackjackGame
                        .Players.First(p => p.IsHuman == false)
                        .Username = username;
                }

                int availableSeats = blackjackGameManagers[gameManagerId]
                         .BlackjackGame
                         .Players
                         .Count(p => p.IsHuman == false);

                return availableSeats;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task PlaceBet(Guid gameId, Guid playerId, int betAmount)
        {
            throw new NotImplementedException();
        }
    }
}
