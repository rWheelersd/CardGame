using CardGame.BL.BlackJack;
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGame.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGame.UI.Controllers
{
    public class GameController : Controller
    {
        // GET: GameController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GameController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: GameController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewGame newGame)
        {
            try
            {
                switch (newGame.SelectedGame)
                {
                    case "Blackjack" : return PlayBlackjack(new BlackjackGameManager(newGame.PlayerCount, newGame.StartingBalance));
                        break;
                    default:
                        break;
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PlayBlackjack(BlackjackGameManager blackjackGameManager)
        {
            return View();
        }
    }
}
