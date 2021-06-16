using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiimoteLib;

namespace WiimoteLeds
{
    class Program
    {
        static void Main(string[] args)
        {

            var wm = new Wiimote();

            wm.Connect();

            wm.SetLEDs(3);

        }
    }
}
