using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Ex02.Good
{
    class TaroFather
    {
        public TaroFather(Daughter daughter)
        {
            this.Daughter = daughter;
        }

        public Daughter Daughter { get; set; }

        public void Warn()
        {
            if (Daughter.PlaysTrick())
            {
                Console.WriteLine("こらっ、{0}ちゃん！！　だめだぞ！！！", Daughter.Name);
            }
            else
            {
                Console.WriteLine("特に叱る必要もないか...");
            }
        }
    }
}
