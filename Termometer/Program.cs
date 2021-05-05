using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termometer
{
    class Program
    {
        static void Main(string[] args)
        {
            var gf = new Visor("Grafico");
            var tx = new Visor("Texto");
            var t = new Termometer();

            t.TemperatureChanged += gf.OnTemperatureChanged;
            t.TemperatureChanged += tx.OnTemperatureChanged;

            t.SetTemp(4.5);

            Console.ReadLine();
        }
    }
}
