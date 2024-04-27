using System;
using System.Collections.Generic;

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
        public string Username { get; set; }
        public bool IsDealer { get; set; }
        public bool IsHuman { get; set; }
        public int Balance { get; set; }
        public int Bet { get; set; }
        public List<THand> Hands { get; set; }

        public Player(int playerNumber, int balance)
        {
            Id = Guid.NewGuid();
            Username = "Player " + playerNumber;
            Balance = balance;
            Hands = new List<THand>();
        }
    }
}
