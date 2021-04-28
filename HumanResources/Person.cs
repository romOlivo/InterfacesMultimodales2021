using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources
{
    public class Person
    {
        string firstName;
        string lastName;

        DateTime birthDate;

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.birthDate = birthDate;
            this.lastName = lastName;
        }

        public override string ToString()
        {
            return firstName + " " + lastName + " " +
                " (" + birthDate.Day + "/" + birthDate.Month + "/" + birthDate.Year + ")";
        }
    }
}
