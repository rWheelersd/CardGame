using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.BaseModels
{
    public class Player<TCard>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public Hand<TCard> Hand { get; set; }

        public Player(int playerNumber, int balance)
        {
            Id = Guid.NewGuid();
            Name = "Player " + playerNumber;
            Balance = balance;
        }
    }
}
