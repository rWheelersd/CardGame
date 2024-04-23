using System;
using CardGame.BL.Models.Interfaces;
using System.Collections.Generic;
using static CardGame.BL.Models.Constants.BaseConstants;
using CardGame.BL.Models.Blackjack;

namespace CardGame.BL.Models.BaseModels
{
    public class Game<TCard, THand, TPlayer>
        where TCard : Card
        where TPlayer : Player<THand>, new()
        where THand : IHand<TCard>, new()
    {
        public Guid GameId { get; set; }
        public Deck<TCard, THand> GameDeck { get; set; }
        public List<TPlayer> Players { get; set; }

        public Game(Guid gameId, int numberOfPlayers, int startingBalance)
        {
            GameId = gameId;
            GameDeck = new Deck<TCard, THand>();
            GameDeck.ShuffleDeck();

            Players = new List<TPlayer>();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Players.Add(new TPlayer() { Balance = startingBalance });
            }
        }

        public void SetHumans(int humanPlayers)
        {
            for (int i = 0; i < humanPlayers; i++)
            {
                Players[i].IsHuman = true;
            }
        }

        public void AddDealer()
        {
            TPlayer dealer = new TPlayer();
            dealer.IsDealer = true;
            dealer.Balance = Players[0].Balance * Players.Count;
            Players.Add(dealer);
        }
    }
}