using CardGame.BL.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Blackjack
{
    public class BlackjackManager
    {
        Dictionary<Guid, BlackjackGameManager> Games;

        public BlackjackManager()
        {
            Games = new Dictionary<Guid, BlackjackGameManager>();
        }

        public Guid CreateGame()
        {
            try
            {
                Guid gameId = Guid.NewGuid();
                Games.Add(gameId, new BlackjackGameManager());
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

        public bool JoinGame(Guid gameId, Guid playerId)
        {
            try
            {
                if (Games.ContainsKey(gameId))
                {
                    
                }
                return false;
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
