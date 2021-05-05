using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Program
    {

        static readonly Random rand = new Random();

        static LotteryGenerator lg;
        static NewsPaper paper;

        static void Main(string[] args)
        {
            lg = new LotteryGenerator();
            paper = new NewsPaper();

            lg.NewDrawPerformed += paper.OnNewDraw;

            int numberOfPlayers = 2000000;
            Console.WriteLine($"Número de participantes: {numberOfPlayers}");
            for (int i = 0; i < numberOfPlayers; i++)
            {
                createPlayer($"Player{i}");
            }

            lg.NewDraw();

            paper.PublicResults();

            Console.ReadLine();
        }

        static Player createPlayer(string name)
        {
            HashSet<int> bet = new HashSet<int>();
            while (bet.Count < 6)
            {
                bet.Add(rand.Next(50));
            }
            var p = new Player(name, bet);
            lg.NewDrawPerformed += p.OnNewDraw;
            p.PrizeWon += paper.OnPrizeWon;
            return p;
        }
    }
}
