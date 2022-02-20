using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Good
{
    class TaroFather
    {
        public TaroFather(Daughter daughter, bool isSleepy)
        {
            Daughter = daughter;
            IsSleepy = isSleepy;
        }

        public Daughter Daughter { get; set; }
        public bool IsSleepy { get; set; }

        public void Warn()
        {
            if (Daughter.PlaysTrick() && IsSleepy)
            {
                Console.WriteLine("こらっ、" + Daughter.Name + "ちゃん！！　だめだぞ！！！");
            }
            else
            {
                Console.WriteLine("特に叱る必要もないか...");
            }
        }

    }
}
