using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Ex02.Good
{
    public class TaroWorker
    {
        public TaroWorker(JuniorColleague juniorColleague)
        {
            JuniorColleague = juniorColleague;
        }

        public JuniorColleague JuniorColleague { get; set; }

        public void Warn()
        {
            if (JuniorColleague.SkipsWork())
            {
                Console.WriteLine("{0}さん、僕も仕事をサボりたいよ。", JuniorColleague.Name);
            }
            else
            {
                Console.WriteLine("特に叱る必要もないか...");
            }
        }

    }
}
