using System;
using System.Collections.Generic;

namespace CardGame.BL.Models.BaseModels
{
    public class Player<THand>
    {
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
