﻿using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using static CardGame.BL.Models.Constants.BaseConstants;

public class BlackjackGame : Game<BlackjackCard, BlackjackHand, BlackjackPlayer>
{
    public readonly int minBet;
    public readonly int maxBet;

    public BlackjackGame(int numberOfPlayers, int startingBalance) : base(numberOfPlayers, startingBalance)
    {
        minBet = (startingBalance * 5) / 100;
        maxBet = (startingBalance * 15) / 100;
        InitializeGame();
    }
    private void InitializeGame()
    {
        try
        {
            Players.Last().IsDealer = true;
            Players.Last().Balance = Players[0].Balance * Players.Count;
            Players[0].IsHuman = true;
        }
        catch (Exception)
        {

            throw;
        }
    }

}