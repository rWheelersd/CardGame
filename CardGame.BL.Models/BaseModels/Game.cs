using System;
using CardGame.BL.Models.Interfaces;
using System.Collections.Generic;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.BaseModels
{
    public class Game<TCard, THand, TPlayer>
        where TCard : Card
        where TPlayer : Player<THand>, new()
        where THand : IHand<TCard>, new()
    {
        public Deck<TCard, THand> GameDeck { get; set; }
        public List<TPlayer> Players { get; set; }

        public Game(int numberOfPlayers, int startingBalance)
        {
            GameDeck = new Deck<TCard, THand>();
            GameDeck.ShuffleDeck();

            Players = new List<TPlayer>();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Players.Add(new TPlayer() { Balance = startingBalance });
            }
        }
    }
}