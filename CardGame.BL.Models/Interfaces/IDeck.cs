using CardGame.BL.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.BL.Models.Interfaces
{
    public interface IDeck<T> where T : ICard
    {
        public List<T> Cards { get; set; }
        public List<T> BurntCards { get; set; }


        void BuildDeck();
        //List<ICard> RebuildDeck(List<T> gameDeck, List<T> burntCards);
        void ShuffleDeck();
        public IHand<T> DealCards(int handSize);
    }
}
