using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.BaseModels
{
    public class Hand
    {
        public List<Card> Cards { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }

        public Hand(Hand hand, List<Card> communityCards)
        {
            //evaluation hand
            Cards = new List<Card>(hand.Cards);
            Cards.AddRange(communityCards);
        }

    }
}
