using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CardGame.BL.Models.BaseModels
{
    public class Player<THand>
    {
        /*
         * Player class
         * Intended to be generic and inhereted as specific games can have players, hands, and cards
         * with unique properties and methods to a specific gametype. Player will also have a generic hand 
         * as hand actions can be unique to a game this is handled thorugh using and interface to ensure every
         * player has a list of hands.
         */

        public Guid Id { get; set; }
        public string Username { get; private set; }
        public bool IsDealer { get; private set; }
        public bool IsHuman { get; private set; }
        public int Balance { get; private set; }
        public int Bet { get; private set; }
        public List<THand> Hands { get; set; }

        public Player(int playerNumber, int balance)
        {
            this.Id = Guid.NewGuid();
            this.Username = "Player " + playerNumber;
            this.Balance = balance;
            this.Hands = new List<THand>();
        }

        public void SetUserName(string userName)
        {
            this.Username = userName.Trim();
        }
        public void SetDealer(bool isDealer)
        {
            this.IsDealer = isDealer;
        }

        public void SetHuman(bool isHuman)
        {
            this.IsHuman = isHuman;
        }

        public void UpdateBalance(int balanceModifier)
        {
            this.Balance += balanceModifier;
        }

        public void UpdateBet(int betModifier, bool reset = false)
        {
            if (reset)
            {
                this.Bet = 0;
            }
            else
            {
                this.Bet += betModifier;
            }
        }
    }
}