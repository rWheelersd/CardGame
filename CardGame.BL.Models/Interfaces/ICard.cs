using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.Interfaces
{
    public interface ICard
    {
        string ImgPath { get; set; }
        string CardName { get; set; }
        //Rank CardRank { get; set; }
        //Suit CardSuit { get; set; }
    }
}
