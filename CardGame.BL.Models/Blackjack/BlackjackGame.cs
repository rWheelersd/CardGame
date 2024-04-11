﻿
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System.Numerics;
using static CardGame.BL.Models.Constants.BaseConstants;

public class BlackjackGame : Game<BlackjackCard, BlackjackHand, BlackjackPlayer>
{
    public readonly int minBet;
    public readonly int maxBet;

    public BlackjackGame(Guid gameId, int numberOfPlayers, int humanPlayers, int startingBalance) : base(gameId, numberOfPlayers, startingBalance)
    {
        minBet = (startingBalance * 5) / 100;
        maxBet = (startingBalance * 15) / 100;
        SetHumans(humanPlayers);
        AddDealer();
    }

    private void SetHumans(int humanPlayers)
    {
        for (int i = 0; i < humanPlayers; i++)
        {
            Players[i].IsHuman = true;
        }
    }

    private void AddDealer()
    {
        BlackjackPlayer dealer = new BlackjackPlayer();
        dealer.IsDealer = true;
        dealer.Balance = Players[0].Balance * Players.Count;
        Players.Add(dealer);
    }

    public void StartRound()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].Hands.Add(GameDeck.DealCards(2));
            Players[i].Hands[0].Cards[0].IsVisible = true;
            //Temporary name handling, change when implementing signalR and DB
            Players[i].Username += i;
        }
    }
}