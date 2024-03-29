using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Constants;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.Models.Blackjack
{
    public class BlackjackCard : Card
    {
        public bool IsVisible = false;

        public BlackjackCard(Rank rank, Suit suit) : base(rank, suit)
        {

        }
    }
}
