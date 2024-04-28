using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System.Numerics;
using static CardGame.BL.Models.Constants.BaseConstants;

//BlackjackGame class inherits from the generic base class with specified constraints
public class BlackjackGame : Game<BlackjackCard, BlackjackHand, BlackjackPlayer>
{
    public readonly int minBet;
    public readonly int maxBet;
    public BlackjackCard dealerCard { get; private set; }

    //Initializes the game with given parameters and passes them to the base class.
    public BlackjackGame(Guid gameId, int numberOfPlayers, int humanPlayers, int startingBalance) : base(gameId, numberOfPlayers, humanPlayers, startingBalance)
    {
        minBet = (startingBalance * 5) / 100;
        maxBet = (startingBalance * 15) / 100;
    }

    //Deals hands for roudn start, which should be done after bet collection in a class that manages the game.
    //Sets a visible card in each hand.
    //Temporarily deals with usernames.
    //assigns the dealers visible card for game management (last player in player pool which is added in base class)
    public void StartRound()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].Hands.Add(GameDeck.DealCards(2));
            Players[i].Hands[0].Cards[0].IsVisible = true;
            //Temporary name handling, change when implementing signalR and DB
            if (Players[i].IsDealer)
            {
                Players[i].Username = $"Dealer";
            }
            else
            {

                Players[i].Username = $"Player {i}";
            }
        }

        dealerCard = Players.FirstOrDefault(p => p.IsDealer)
                            .Hands.First()
                            .Cards.First(c => c.IsVisible == true);
    }
}