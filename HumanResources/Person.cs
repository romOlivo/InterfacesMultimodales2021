using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources
{
    public class Person
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime BirthDate { get; protected set; }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            BirthDate = birthDate;
            LastName = lastName;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " +
                " (" + BirthDate.Day + "/" + BirthDate.Month + "/" + BirthDate.Year + ")";
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Now;
                int years = today.Year - BirthDate.Year - 1;
                if (today.Month > BirthDate.Month ||
                    (today.Month == BirthDate.Month && today.Day >= BirthDate.Day))
                    years++;
                return years;

            }
        }

        public string Initials
        {
            get
            {
                return FirstName[0] + "." + LastName[0] + ".";
            }
        }

    }
}
