using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardGame.BL.Models.Constants.BaseConstants;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackJack
{
    //public class BlackjackGameManager
    //{
    //    private BlackjackGame game;
    //    private BlackjackPlayerManager playerManager;
    //    private Random rng = new Random();

    //    public BlackjackGameManager()
    //    {
    //    }

    //    public BlackjackGameManager(int players, int balance) 
    //    {
    //        game = new BlackjackGame(players, balance);

    //        StartGame();
    //    }

    //    private void StartGame()
    //    {
    //        game.DealHands();
    //        bool roundActive = false;
    //        bool gameOver = false;
    //        BlackjackCard dealerShownCard = new BlackjackCard();

    //        try
    //        {
    //            while (game.Players.Count > 0)
    //            {
    //                if (!gameOver)
    //                {
    //                    if (roundActive)
    //                    {
    //                        game.DealHands();
    //                        dealerShownCard = game.Players.Last().Hand.Cards.FirstOrDefault(card => card.IsVisible);

    //                        foreach (BlackjackPlayer player in game.Players)
    //                        {
    //                            if (!player.IsHuman)
    //                            {
    //                                while (player.Status == PlayerStatus.Active)
    //                                {
    //                                    DealerResponse(player, playerManager.PlayTurn(player, dealerShownCard));
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        //foreach (BlackjackPlayer player in game.Players)
    //                        //{
    //                        //    if (!player.IsHuman && !player.IsDealer)
    //                        //    {
    //                        //        if (player.Balance <= 0)
    //                        //        {
    //                        //            game.Players.Remove(player);
    //                        //            continue;
    //                        //        }
    //                        //        playerManager.PlayerBet(player, game.minBet, game.maxBet, rng);
    //                        //    }
    //                        //}

    //                        for (int i = game.Players.Count - 1; i >= 0; i--)
    //                        {
    //                            BlackjackPlayer player = game.Players[i];
    //                            if (!player.IsHuman && !player.IsDealer)
    //                            {
    //                                if (player.Balance <= 0)
    //                                {
    //                                    game.Players.RemoveAt(i);
    //                                }
    //                                else
    //                                {
    //                                    playerManager.PlayerBet(player, game.minBet, game.maxBet, rng);
    //                                }
    //                            }
    //                        }
    //                        roundActive = true;
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }
    //    }

    //    private void DealerResponse(BlackjackPlayer player, PlayerActions playerActions)
    //    {
    //        try
    //        {

    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }
    //    }
    //}
}
