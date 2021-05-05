using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termometer
{
    class Visor
    {

        public string Name { get; set; }

        public Visor(string name)
        {
            Name = name;
        }

        public void OnTemperatureChanged(double nt)
        {
            Console.WriteLine($"{Name} -- Temp: {nt}");
        }
    }
}
