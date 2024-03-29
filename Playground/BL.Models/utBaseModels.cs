using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace UnitTests.BL.Models
{
    [TestClass]
    public class utBaseModels
    {
        [TestMethod]
        public void BuildCardTest()
        { 
            Card card = new Card(Rank.Ace, Suit.Spades);

            Assert.IsNotNull(card);
            Assert.IsTrue(card.CardRank == Rank.Ace);
            Assert.IsTrue(card.CardSuit == Suit.Spades);
            Assert.IsTrue(card.CardName == $"{card.CardRank} of {card.CardSuit}");
        }

        [TestMethod]
        public void BuildHandTest()
        {
            Card card1 = new Card(Rank.Ace, Suit.Spades);
            Card card2 = new Card(Rank.Ace, Suit.Spades);
            Card card3 = new Card(Rank.Ace, Suit.Spades);
            Card card4 = new Card(Rank.Ace, Suit.Spades);
            Card card5 = new Card(Rank.Ace, Suit.Spades);

            Hand playingHand = new Hand();

            playingHand.Cards.Add(card1);
            playingHand.Cards.Add(card2);

            List<Card> communityCards = [card3, card4, card5];

            Hand evaluationHand = new Hand(playingHand, communityCards);

            Assert.IsNotNull(evaluationHand);
            Assert.IsTrue(evaluationHand.Cards.Count == 5);
            Assert.IsNotNull(playingHand);
            Assert.IsTrue(playingHand.Cards.Count == 2);
        }

        [TestMethod]
        public void BuildDeckTest()
        {
            Deck<BlackjackCard, BlackjackHand> blackjackDeck = new Deck<BlackjackCard, BlackjackHand>();

            BlackjackHand blackjackHand = new BlackjackHand();

            blackjackHand = blackjackDeck.DealCards(2);


            Assert.IsNotNull(blackjackDeck);
            Assert.IsTrue(blackjackDeck.Cards is List<BlackjackCard>);
            Assert.IsNotNull(blackjackHand);
            Assert.IsTrue(blackjackHand.Cards.Count == 2);
            Assert.IsTrue(blackjackHand is BlackjackHand);
            Assert.IsTrue(blackjackHand.Cards[0] is BlackjackCard);

        }
    }
}
