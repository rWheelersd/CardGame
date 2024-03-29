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
    public class utBlackjackModels
    {
        [TestMethod]
        public void BuildBlackjackCardTest()
        {
            BlackjackCard blackjackCard = new BlackjackCard(Rank.Ace, Suit.Spades);
            blackjackCard.IsVisible = true;

            Assert.IsNotNull(blackjackCard);
            Assert.IsTrue(blackjackCard.CardRank == Rank.Ace);
            Assert.IsTrue(blackjackCard.CardSuit == Suit.Spades);
            Assert.IsTrue(blackjackCard.CardName == $"{blackjackCard.CardRank} of {blackjackCard.CardSuit}");
            Assert.IsTrue(blackjackCard.IsVisible == true);
        }

        [TestMethod]
        public void BuildBlackjackHandTest()
        {
            BlackjackHand blackjackHand = new BlackjackHand();
            Assert.IsNotNull (blackjackHand.Cards);
        }
    }
}
