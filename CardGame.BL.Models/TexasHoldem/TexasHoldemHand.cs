using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.TexasHoldem
{
    internal class TexasHoldemHand : IHand<Card>
    {
        public List<Card> Cards { get; set; }
    }
}
