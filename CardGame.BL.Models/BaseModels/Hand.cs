using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.BaseModels
{
    public class Hand<T> : IHand<T> where T : ICard
    {
        public List<T> Cards { get; set; }

        public Hand()
        {
            Cards = new List<T>();
        }

        public Hand(int handSize)
        {
            Cards = new List<T>(handSize);
        }
    }
}
