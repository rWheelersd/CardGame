using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace UnitTests.BL.Models
{
    [TestClass]
    public class utBlackJackModels
    {
        [TestMethod]
        public void BuildBlackJackCardTest()
        { 
            BlackjackCard blackJackCard = new BlackjackCard(Rank.Ace, Suit.Spades);
            blackJackCard.IsVisible = true;

            Assert.IsNotNull(blackJackCard);
            Assert.IsTrue(blackJackCard.CardRank == Rank.Ace);
            Assert.IsTrue(blackJackCard.CardSuit == Suit.Spades);
            Assert.IsTrue(blackJackCard.CardName == $"{blackJackCard.CardRank} of {blackJackCard.CardSuit}");
            Assert.IsTrue(blackJackCard.IsVisible == true);
        }
    }
}
