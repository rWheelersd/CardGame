﻿using CardGame.BL.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackGame : Game<BlackjackPlayer, BlackjackHand, BlackjackCard>
    {
        private readonly GameType _gameType = GameType.Blackjack;
        public readonly int minBet;
        public readonly int maxBet;

        public BlackjackGame(int playerCount, int startingBalance) : base(playerCount, startingBalance)
        {
            this.minBet = (startingBalance * 5) / 100;
            this.maxBet = (startingBalance * 15) / 100;
            InitializeGame();
        }

        private void InitializeGame()
        {
            try
            {
                Players[0].IsDealer = true;
                Players[0].Balance = Players[0].Balance * Players.Count;
                GameDeck.ShuffleDeck(GameDeck);
            }
            catch (Exception)
            { 

                throw;
            }
        }

        private void DealHands()
        {
            try
            {
                foreach (BlackjackPlayer player in Players)
                {
                    player.Hand = GameDeck.DealCards(_gameType, GameDeck);
                    player.Hand.Cards[1].IsVisible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
