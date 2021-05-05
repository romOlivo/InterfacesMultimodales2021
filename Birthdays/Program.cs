using HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthdays
{
    class Program
    {
        static void Main(string[] args)
        {
            var mar = new Person("Mar", "Gil", new DateTime(1980, 2, 28));
            Console.WriteLine(mar);
            Console.WriteLine("Su edad es {0}", mar.Age);
            Console.WriteLine($"con iniciales {mar.Initials}");
            Employed somebody = new Employed("Pep", "Paz", new DateTime(1980, 10, 12), Employed.Level.Senior, "CEO");
            Console.WriteLine(somebody);
            Console.ReadLine();
        }
    }
}
