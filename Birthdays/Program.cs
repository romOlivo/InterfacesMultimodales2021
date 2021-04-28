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
            Console.WriteLine( mar );
            Console.ReadLine();
        }
    }
}
