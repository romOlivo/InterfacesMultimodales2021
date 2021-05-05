using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Player
    {

        public event Action<string, int> PrizeWon;

        public string Name { get; private set; }
        public HashSet<int> Bet { get; private set; }

        public Player(string name, HashSet<int> bet)
        {
            Name = name;
            Bet = bet;
            // PrizeWon += OnPrizeWon;
        }

        public void OnNewDraw(HashSet<int> n)
        {
            Bet.IntersectWith(n);
            int p = Bet.Count;
            if (p >= 3)
                PrizeWon?.Invoke(Name, p);
        }

        public void OnPrizeWon(string name, int p)
        {
            Console.WriteLine($"{Name} -- {p}");
        }

    }
}
