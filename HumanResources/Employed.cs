﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources
{
    public class Employed : Person
    {

        public enum Level { Junior, Senior };
        public Level PayLevel { get; protected set; }
        public string Position { get; protected set; }

        public Employed(string firstName, string lastName, DateTime birthDate, Level payLevel,
            string position) : base(firstName, lastName, birthDate)
        {
            PayLevel = payLevel;
            Position = position;
        }

        public override string ToString()
        {
            return base.ToString() + " # " + PayLevel + " # " + Position;
        }
    }
}
