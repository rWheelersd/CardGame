using CardGame.BL.Models.BaseModels;
using CardGame.BL.Models.Blackjack;
using System;
using static CardGame.BL.Models.Constants.BlackjackConstants;

namespace CardGame.BL.BlackjackManagers
{
    public class BlackjackGameManager
    {
        private int turnCounter = 0;
        public BlackjackGame BlackjackGame { get; private set; }
        private BlackjackAIManager AIManager { get; set; }
        public BlackjackGameManager(Guid gameId, int playerCount, int humanPlayers, int startingBalance)
        {
            BlackjackGame = new BlackjackGame(gameId, playerCount, humanPlayers, startingBalance);
        }

        public void StartRound()
        {
            for (int i = 0; i < BlackjackGame.Players.Count; i++)
            {
                BlackjackGame.Players[i].Hands.Add(BlackjackGame.GameDeck.DealHand(2));
                BlackjackGame.Players[i].Hands[0].Cards[0].RevealCard();
                //Temporary name handling, change when implementing signalR and DB
                if (BlackjackGame.Players[i].IsDealer)
                {
                    BlackjackGame.Players[i].SetUserName($"Dealer");
                }
                else
                {
                    BlackjackGame.Players[i].SetUserName($"Player {i}");
                }
            }

            Card dealerCard = BlackjackGame.Players.FirstOrDefault(p => p.IsDealer)
                                .Hands.First()
                                .Cards.First(c => c.IsVisible == true);

            BlackjackGame.SetDealerCard(dealerCard);
        }

        public PlayerStatus PlayerTurn(int option, List<BlackjackHand> blackjackHands)
        {
            //Gets initial hand value, i dont like how this is done. Return to figure out a better way to do this later
            if (blackjackHands[0].HardValue == 0)
            {
                BlackjackHandManager.GetHandValues(blackjackHands[0]);
            }

            switch (option)
            {
                case 1: //HIT
                    BlackjackGame.GameDeck.DealCards(blackjackHands[0]);
                    BlackjackHandManager.GetHandValues(blackjackHands[0]);
                    if (blackjackHands[0].Action != HandActions.FlipBust && blackjackHands[0].Action != HandActions.FlipBlackjack)
                    {
                        return PlayerStatus.Active;
                    }
                    else
                    {
                        return PlayerStatus.Inactive;
                    }

                case 2: //Stand
                    blackjackHands[0].SetAction(HandActions.Stand);
                    return PlayerStatus.Inactive;

                case 3: //Double Down
                    BlackjackGame.GameDeck.DealCards(blackjackHands[0]);
                    blackjackHands[0].SetAction(HandActions.Stand);
                    BlackjackHandManager.GetHandValues(blackjackHands[0]);
                    return PlayerStatus.Inactive;

                case 4: //Split
                    BlackjackHandManager.SplitHand(blackjackHands, blackjackHands[0]);
                    return PlayerStatus.Active;

                default: throw new ArgumentOutOfRangeException(nameof(option));
            }
        }

        public void ProcessAI()
        {
            try
            {
                AIManager = new BlackjackAIManager(BlackjackGame.dealerCard, BlackjackGame.GameDeck);
                AIManager.PlayAITurns(BlackjackGame.Players);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ResetGame()
        {
            //Resets each each property that needs to be reset prior to starting a new round
            for (int i = BlackjackGame.Players.Count - 1; i >= 0; i--)
            {
                if (BlackjackGame.Players[i].Balance <= 0 && !BlackjackGame.Players[i].IsDealer)
                {
                    BlackjackGame.Players.Remove(BlackjackGame.Players[i]);
                }
                else
                {
                    BlackjackGame.Players[i].Status = PlayerStatus.Active;
                    BlackjackGame.Players[i].Hands.Clear();
                    BlackjackGame.Players[i].UpdateBet(0, true);
                }
            }
            BlackjackGame.GameDeck.ResetDeck();
        }

        public void ManagePayouts()
        {
            List<BlackjackHand> dealerHands = BlackjackGame.Players.LastOrDefault(p => p.IsDealer).Hands;

            foreach (BlackjackPlayer blackjackPlayer in BlackjackGame.Players)
            {
                foreach (BlackjackHand blackjackHand in blackjackPlayer.Hands)
                {
                    foreach (BlackjackHand dealerHand in dealerHands)
                    {
                        //Looping required in case of players with split hands
                        GetPayoutCondition(blackjackHand, dealerHand);
                    }
                }

                //More handling of the idea that a player may have more than one hand
                //because a player with more than one hand may only win or lose their bet once
                if (blackjackPlayer.Hands.Any(h => h.WinningHand == true))
                {
                    if (blackjackPlayer.Hands.Any(h => h.Action == HandActions.FlipBlackjack))
                    {
                        if (dealerHands.Any(h => h.Action == HandActions.FlipBlackjack))
                        {
                            //If there is a push, no money is won or lost
                        }
                        else
                        {
                            blackjackPlayer.UpdateBalance(blackjackPlayer.Bet + (blackjackPlayer.Bet / 2));
                        }
                    }
                    else
                    {
                        blackjackPlayer.UpdateBalance(blackjackPlayer.Bet * 2);
                    }
                }
                else
                {
                    blackjackPlayer.UpdateBalance(-blackjackPlayer.Bet);
                }
            }
        }

        private void GetPayoutCondition(BlackjackHand playerHand, BlackjackHand dealerHand)
        {
            //All of this determines if a hand is a winning hand or not
            if (playerHand.Action == HandActions.FlipBlackjack)
            {
                if (dealerHand.Action != HandActions.FlipBlackjack)
                {
                    playerHand.SetWinStatus(true);
                }
            }
            else
            {
                int playerValue = playerHand.HardValue;
                int dealerValue = dealerHand.HardValue;

                if (playerValue <= 21)
                {
                    if ((dealerValue > 21) || (playerValue > dealerValue))
                    {
                        playerHand.SetWinStatus(true);
                    }
                    else if (playerValue < dealerValue)
                    {
                        playerHand.SetWinStatus(false);
                    }
                }
                else
                {
                    playerHand.SetWinStatus(false);
                }
            }
        }
    }
}
