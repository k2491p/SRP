using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Ex02.Good
{
    public class JuniorColleague
    {
        public JuniorColleague(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool IsHangry { get; set; } = true;
        public bool IsTiered { get; set; } = true;

        public bool SkipsWork()
        {
            return IsHangry && IsTiered;
        }
    }
}
