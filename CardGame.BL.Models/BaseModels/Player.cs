using System;
using System.Collections.Generic;

namespace CardGame.BL.Models.BaseModels
{
    public class Player<THand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public int Bet { get; set; }
        public THand Hand { get; set; }

        public Player(int playerNumber, int balance)
        {
            Id = Guid.NewGuid();
            Name = "Player " + playerNumber;
            Balance = balance;
        }
    }
}
