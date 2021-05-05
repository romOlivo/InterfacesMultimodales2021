using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace Lottery
{

    class LotteryGenerator
    {

        public event Action<HashSet<int>> NewDrawPerformed;

        private readonly Random rand = new Random();

        public void NewDraw()
        {
            HashSet<int> n = new HashSet<int>();
            while (n.Count < 6)
            {
                n.Add(rand.Next(50));
            }
            NewDrawPerformed?.Invoke(n);
        }
    }
}
