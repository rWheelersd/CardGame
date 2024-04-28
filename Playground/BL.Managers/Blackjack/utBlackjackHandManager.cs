using CardGame.BL.BlackJack;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace UnitTests.BL.Managers.Blackjack
{
    [TestClass]
    public class utBlackjackHandManager
    {
        //public TestContext TestContext { get; set; }
        //Deck<BlackjackCard, BlackjackHand> blackjackDeck;
        //List<BlackjackHand> testHands;

        //[TestMethod]
        //public void HandEvaluationTest()
        //{
        //    blackjackDeck = new Deck<BlackjackCard, BlackjackHand>();
        //    testHands = new List<BlackjackHand>();

        //    blackjackDeck.ShuffleDeck();

        //    for (int i = 0; i < 26; i++)
        //    {
        //        testHands.Add(blackjackDeck.DealCards(2));
        //    }

        //    BlackjackHand dealerHand = testHands.Last();
        //    BlackjackCard dealerCard = dealerHand.Cards.First();
        //    testHands.RemoveAt(testHands.Count - 1);

        //    foreach (BlackjackHand hand in testHands)
        //    {

        //        TestContext.WriteLine($"**Split Check**");
        //        TestContext.WriteLine($"Hand has {hand.Cards[0].CardName} : {hand.Cards[1].CardName}");
        //        TestContext.WriteLine($"Dealer card is {dealerCard.CardName}");

        //        BlackjackHandManager.EvaluateSplit(hand, dealerCard);

        //        TestContext.WriteLine($"Hand action : {hand.Action.ToString()}\n");

        //        TestContext.WriteLine($"**Split Test**");

        //        if (hand.Action == HandActions.Split)
        //        {
        //            TestContext.WriteLine($"Hand has {hand.Cards[0].CardName} : {hand.Cards[1].CardName}");
        //            List<BlackjackHand> listOfHands = new List<BlackjackHand>();
        //            BlackjackHand newBlackjackHand = new BlackjackHand();
        //            newBlackjackHand = hand;
        //            listOfHands.Add(newBlackjackHand);
        //            BlackjackHandManager.SplitHand(listOfHands);
        //            TestContext.WriteLine($"listOfHands has {listOfHands.Count} hands");

        //            foreach (BlackjackHand splitHand in listOfHands)
        //            {
        //                TestContext.WriteLine($"This hand has {splitHand.Cards.Count} Card");
        //                foreach (BlackjackCard card in splitHand.Cards) 
        //                {
        //                    TestContext.WriteLine($"Card is {card.CardName}");
        //                }
        //            }
        //            TestContext.WriteLine($"\n");
        //        }
        //        else
        //        {
        //            TestContext.WriteLine($"No pair to split\n");
        //        }

        //        TestContext.WriteLine($"**Action Check**");
        //        TestContext.WriteLine($"Hand has {hand.Cards[0].CardName} : {hand.Cards[1].CardName}");
        //        TestContext.WriteLine($"Dealer card is {dealerCard.CardName}");

        //        BlackjackHandManager.GetAction(hand, dealerCard);

        //        TestContext.WriteLine($"Hand action : {hand.Action.ToString()}\n");
        //        TestContext.WriteLine($"--------------------------------------------------------\n");
        //    }
        //}
    }
}
