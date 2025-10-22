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
    public class utBuildModels
    {
        [TestMethod]
        public void BuildCardTest()
        {
            BlackjackCard blackjackCard = new BlackjackCard(Rank.Ace, Suit.Spades);
            blackjackCard.IsVisible = true;

            Assert.IsNotNull(blackjackCard);
            Assert.IsTrue(blackjackCard.CardRank == Rank.Ace);
            Assert.IsTrue(blackjackCard.CardSuit == Suit.Spades);
            Assert.IsTrue(blackjackCard.CardName == $"{blackjackCard.CardRank} of {blackjackCard.CardSuit}");
            Assert.IsTrue(blackjackCard.IsVisible == true);
            Assert.IsTrue(blackjackCard.CardValue == 11);
        }

        public void BuildPlayerTest()
        {
            Player<BlackjackHand> player = new BlackjackPlayer(1, 5000);

            Assert.IsNotNull(player);
            Assert.IsTrue(player is BlackjackPlayer);
            Assert.IsTrue(player.Hands[0] is BlackjackHand);
            Assert.IsTrue(player.Hands[0].Cards[0] is BlackjackCard);
            Assert.IsTrue(player.Balance == 5000);
            Assert.IsTrue(player.Username == "Player 1");
        }

        [TestMethod]
        public void BuildDeckTest()
        {
            Deck<BlackjackCard, BlackjackHand> blackjackDeck = new Deck<BlackjackCard, BlackjackHand>();

            BlackjackHand blackjackHand = new BlackjackHand();

            Assert.IsTrue(blackjackDeck.Cards.Count == 52);

            blackjackHand = blackjackDeck.DealCards(2);


            Assert.IsNotNull(blackjackDeck);
            Assert.IsTrue(blackjackDeck.Cards is List<BlackjackCard>);
            Assert.IsNotNull(blackjackHand);
            Assert.IsTrue(blackjackHand.Cards.Count == 2);
            Assert.IsTrue(blackjackHand is BlackjackHand);
            Assert.IsTrue(blackjackHand.Cards[0] is BlackjackCard);

        }

        [TestMethod]
        public void BuildGameTest()
        {
            BlackjackGame blackjackGame = new BlackjackGame(Guid.NewGuid(), 5, 1, 5000);
            blackjackGame.StartRound();

            Assert.IsNotNull(blackjackGame);
            Assert.IsTrue(blackjackGame is BlackjackGame);
            Assert.IsTrue(blackjackGame.Players.Count == 6);
            Assert.IsTrue(blackjackGame.Players[0] is BlackjackPlayer);
            Assert.IsTrue(blackjackGame.Players[0].Balance == 5000);
            Assert.IsTrue(blackjackGame.Players[0].Hands[0].Cards.Count == 2);
            Assert.IsTrue(blackjackGame.Players[0].Hands[0].Cards[0] is BlackjackCard);
            Assert.IsTrue(blackjackGame.Players[0].Hands[0].Cards[0].IsVisible == true);
            Assert.IsTrue(!blackjackGame.Players.Last().IsHuman);
            Assert.IsTrue(blackjackGame.Players.Last().Balance == 25000);
            Assert.IsTrue(blackjackGame.GameDeck.Cards[0] is BlackjackCard);
            Assert.IsTrue(blackjackGame.GameDeck.BurntCards.Count == 12);
            Assert.IsTrue(blackjackGame.GameDeck.Cards.Count == 40);
            Assert.IsTrue(blackjackGame.minBet == 250);
            Assert.IsTrue(blackjackGame.maxBet == 750);
        }
    }
}
