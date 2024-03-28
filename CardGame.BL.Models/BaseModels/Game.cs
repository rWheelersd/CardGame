using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.BaseModels
{
    public class Game<TPlayer, THand, TCard> where TPlayer : Player<TCard>
                                             where TCard : Card<TCard>, new()
    {
    //    public Guid Id { get; set; }
    //    public List<TPlayer> Players { get; set; }
    //    public Deck<TCard> GameDeck { get; set; }

    //    public Game(int playerCount, int startingBalance)
    //    {
    //        Id = Guid.NewGuid();
    //        Players = new List<TPlayer>();
    //        GameDeck = new Deck<TCard>();
    //        AddPlayers(playerCount, startingBalance);
    //    }

    //    private void AddPlayers(int count, int balance)
    //    {
    //        try
    //        {
    //            for (int i = 0; i < count + 1; i++)
    //            {
    //                Players.Add((TPlayer)Activator.CreateInstance(typeof(TPlayer), i + 1, balance));
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //    }
    }
}
