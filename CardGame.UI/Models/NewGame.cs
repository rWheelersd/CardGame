using CardGame.BL.Models.BaseModels;

namespace CardGame.UI.Models
{
    public class NewGame
    {
        public string SelectedGame { get; set; }
        public int StartingBalance { get; set; }
        public int PlayerCount { get; set; }

        public NewGame()
        {
        }

        public NewGame(string gameType, int startingBalance, int playerCount)
        {
            SelectedGame = gameType;
            StartingBalance = startingBalance;
            PlayerCount = playerCount;
        }
    }
}
