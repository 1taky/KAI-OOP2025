using System;
using System.Runtime.InteropServices;
using Lab1.Models;

namespace Lab1
{

    public abstract class Human
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        protected Human(string fName, string lName, Gender gdr)
        {
            FirstName = fName;
            LastName = lName;
            Gender = gdr;
        }
    }
}
