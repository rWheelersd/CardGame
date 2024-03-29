using CardGame.BL.Blackjack;

namespace CardGame.API.Hubs
{
    public class BlackjackHub
    {
        private readonly BlackjackManager _gamesManager;

        public BlackjackHub(BlackjackManager gamesManager)
        {
            _gamesManager = gamesManager;
        }
        public async Task CreateGame(Guid gameId, Guid playerId, int betAmount)
        {
            await _gamesManager.CreateGame(gameId, playerId, betAmount);
        }

        public async Task PlaceBet(Guid gameId, Guid playerId, int betAmount)
        {
            await _gamesManager.PlaceBet(gameId, playerId, betAmount);
        }
    }
}
