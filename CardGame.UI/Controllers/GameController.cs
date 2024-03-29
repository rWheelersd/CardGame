using CardGame.BL.BlackJack;
using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using CardGameWeb.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static CardGame.BL.Models.Constants.BaseConstants;

namespace CardGameWeb.UI.Controllers
{
    public class GameController : Controller
    {
        // GET: GameController
        //public ActionResult Index(BlackjackGameManager blackjackGameManager)
        //{
        //    return View(blackjackGameManager);
        //}

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
                    case "Blackjack" :
                        //BlackjackGameManager game = new BlackjackGameManager(newGame.PlayerCount, newGame.StartingBalance);
                        //return RedirectToAction("Index", game);
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
    }
}
