using CardGame.BL.BlackJack;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace UnitTests.BL.Managers.Blackjack
{
    [TestClass]
    public class utBlackjackHandManager
    {
        public TestContext TestContext { get; set; }
        Deck<BlackjackCard, BlackjackHand> blackjackDeck;
        List<BlackjackHand> testHands;

        [TestMethod]
        public void HandEvaluationTest()
        {
            blackjackDeck = new Deck<BlackjackCard, BlackjackHand>();
            testHands = new List<BlackjackHand>();

            blackjackDeck.ShuffleDeck();

            for (int i = 0; i < 26; i++)
            {
                testHands.Add(blackjackDeck.DealCards(2));
            }

            BlackjackHand dealerHand = testHands.Last();
            BlackjackCard dealerCard = dealerHand.Cards.First();
            testHands.RemoveAt(testHands.Count - 1);

            foreach (BlackjackHand hand in testHands)
            {
                TestContext.WriteLine($"**Pair Check**");
                TestContext.WriteLine($"Hand has {hand.Cards[0].CardName} : {hand.Cards[1].CardName}");
                TestContext.WriteLine($"Hand pair : {BlackjackHandManager.PairCheck(hand)}\n");

                TestContext.WriteLine($"**Action Check**");
                TestContext.WriteLine($"Hand has {hand.Cards[0].CardName} : {hand.Cards[1].CardName}");
                TestContext.WriteLine($"Dealer card is {dealerCard.CardName}");
                BlackjackHandManager.GetAction(hand, dealerCard);
                TestContext.WriteLine($"Hand action : {hand.Action.ToString()}\n");
                TestContext.WriteLine($"--------------------------------------------------------\n");
            }
        }
    }
}
