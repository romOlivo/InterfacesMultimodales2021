using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class NewsPaper
    {

        public int[] Winners { get; private set; }

        public NewsPaper()
        {
            Winners = new int[4];
        }

        public void OnNewDraw(HashSet<int> n)
        {
            string t = "El numero ganador es:";
            foreach(var i in n)
            {
                t = $"{t}  {i}";
            }
            Console.WriteLine(t);
        }

        public void OnPrizeWon(string name, int p)
        {
            Winners[p - 3]++;
        }

        public void PublicResults()
        {
            for (int i =0; i<Winners.Length;i++)
            {
                Console.WriteLine($"Ganadores con {i+3} aciertos: {Winners[i]}");
            }
        }
    }
}
