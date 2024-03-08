using CardGame.BL.BlackJack;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    [TestClass]
    public class MethodTests
    {
        [TestMethod]
        public void BuildBlackjackGame()
        {
            bool pause = false;
            BlackjackGame blackjackGame = new BlackjackGame(5, 5000);
            //BlackjackGameManager blackjackGameManager = new BlackjackGameManager(5, 5000);
            Assert.IsTrue(pause);
        }
    }
}
