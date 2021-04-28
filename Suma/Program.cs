using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suma
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            Console.WriteLine("Introduzca un número");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca otro número");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine("El resultado de la suma es {0}", a+b);
        }
    }
}
