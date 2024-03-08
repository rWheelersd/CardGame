using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.BaseModels
{
    public class Hand<TCard>
    {
        public List<TCard> Cards { get; set; }
        public Hand()
        {
            Cards = new List<TCard>();
        }
        public Hand(int handSize)
        {
            Cards = new List<TCard>(handSize);
        }
    }
}
