using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Good
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
                Console.WriteLine(JuniorColleague.Name + "さん、僕も仕事をサボりたいよ。");
            }
            else
            {
                Console.WriteLine("特に叱る必要もないか...");
            }
        }

    }
}
