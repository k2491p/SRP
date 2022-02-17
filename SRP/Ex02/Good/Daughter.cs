using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Ex02.Good
{
    public class Daughter
    {
        public Daughter(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool IsHangry { get; set; } = true;

        public bool PlaysTrick()
        {
            return IsHangry;
        }
    }
}
