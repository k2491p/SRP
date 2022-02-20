using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Good
{
    public class Daughter
    {
        public Daughter(string name, bool isHangry)
        {
            Name = name;
            IsHangry = isHangry;
        }

        public string Name { get; set; }
        public bool IsHangry { get; set; }

        public bool PlaysTrick()
        {
            return IsHangry;
        }
    }
}
