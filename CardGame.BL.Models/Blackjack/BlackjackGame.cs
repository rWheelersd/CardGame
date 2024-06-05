using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.BL.Models.Interfaces;
using System.Numerics;
using static CardGame.BL.Models.Constants.BaseConstants;

//BlackjackGame class inherits from the generic base class with specified constraints
public class BlackjackGame : Game<BlackjackHand, BlackjackPlayer>
{
    public readonly int minBet;
    public readonly int maxBet;
    public Card dealerCard { get; private set; }

    public BlackjackGame()
    {
    }

    //Initializes the game with given parameters and passes them to the base class.
    public BlackjackGame(Guid gameId, int numberOfPlayers, int humanPlayers, int startingBalance) : base(gameId, numberOfPlayers, humanPlayers, startingBalance)
    {
        this.minBet = (startingBalance * 5) / 100;
        this.maxBet = (startingBalance * 15) / 100;

        AssignCardValues(this.GameDeck);
    }

    private void AssignCardValues(Deck<BlackjackHand> gameDeck)
    {
        foreach (var card in gameDeck.Cards)
        {
            card.SetBlackjackValue();
        }
    }

    public void SetDealerCard(Card dealerCard)
    {
        this.dealerCard = dealerCard;
    }

    //Deals hands for round start, which should be done after bet collection in a class that manages the game.
    //Sets a visible card in each hand.
    //Temporarily deals with usernames.
    //assigns the dealers visible card for game management (last player in player pool which is added in base class)

    //public override void StartRound()
    //{
    //    for (int i = 0; i < this.Players.Count; i++)
    //    {
    //        this.Players[i].Hands.Add(this.GameDeck.DealHand(2));
    //        this.Players[i].Hands[0].Cards[0].RevealCard();
    //        //Temporary name handling, change when implementing signalR and DB
    //        if (this.Players[i].IsDealer)
    //        {
    //            this.Players[i].SetUserName($"Dealer");
    //        }
    //        else
    //        {
    //            this.Players[i].SetUserName($"Player {i}");
    //        }
    //    }

    //    Card dealerCard = this.Players.FirstOrDefault(p => p.IsDealer)
    //                        .Hands.First()
    //                        .Cards.First(c => c.IsVisible == true);

    //    this.SetDealerCard(dealerCard);
    //}

}