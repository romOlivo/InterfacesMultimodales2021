using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termometer
{

    public delegate void TemperatureChanged(double newTemp);

    class Termometer
    {
        public TemperatureChanged TemperatureChanged;

        public void SetTemp(double nt)
        {
            TemperatureChanged?.Invoke(nt);
        }
    }
}
