using CardGame.BL.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.BL.Models.Interfaces
{
    public interface IHand<T> where T : Card
    {
        List<T> Cards { get; set; }
    }
}
