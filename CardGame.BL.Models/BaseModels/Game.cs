using System;
using CardGame.BL.Models.Interfaces;
using System.Collections.Generic;
using static CardGame.BL.Models.Constants.BaseConstants;
using CardGame.BL.Models.Blackjack;

namespace CardGame.BL.Models.BaseModels
{
    /*
     * Game class
     * As a specific game can have properties and methods unique to it, this game class must remain generic
     * therfore it has genric parameter constraint which also have their own generic constraints outside of card.
     * A game of any type must have a deck and players. And players of those games must have hands that have cards.
     */
    public abstract class Game<THand, TPlayer>
        where TPlayer : Player<THand>, new()
        where THand : IHand<Card>, new()
    {
        public Guid GameId { get; set; }
        public Deck<THand> GameDeck { get; set; }
        public List<TPlayer> Players { get; set; }

        public Game()
        {
        }

        //On instatiation of game the core requirements of any standard 52 card game will be assigned and prepared using the required parameters
        public Game(Guid gameId, int numberOfPlayers, int humanPlayers, int startingBalance)
        {
            this.GameId = gameId;
            this.GameDeck = new Deck<THand>();

            this.Players = new List<TPlayer>();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                this.Players.Add((TPlayer)Activator.CreateInstance(typeof(TPlayer), i, startingBalance));
            }

            SetHumans(humanPlayers);
            AddDealer();
        }

        //public abstract void StartRound();

        public void SetHumans(int humanPlayers)
        {
            for (int i = 0; i < humanPlayers; i++)
            {
                this.Players[i].SetHuman(true);
            }
        }

        public void AddDealer()
        {
            TPlayer player = new TPlayer();
            player.SetDealer(true);
            this.Players.Add(player);
        }
    }
}
