using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.Bad
{
    class Taro
    {
        public Taro(bool isSleepyTaro, string daughterName, bool isHungryDaughter, string juniorColleagueName, bool isHungryJuniorColleague, bool isTieredJuniorColleague)
        {
            IsSleepyTaro = isSleepyTaro;
            DaughterName = daughterName;
            IsHungryDaughter = isHungryDaughter;
            JuniorColleagueName = juniorColleagueName;
            IsHungryJuniorColleague = isHungryJuniorColleague;
            IsTieredJuniorColleague = isTieredJuniorColleague;
        }

        public bool IsSleepyTaro { get; set; }
        public string DaughterName { get; set; }
        public bool IsHungryDaughter { get; set; }
        public string JuniorColleagueName { get; set; }
        public bool IsHungryJuniorColleague { get; set; }
        public bool IsTieredJuniorColleague { get; set; }

        private bool PlaysTrick()
        {
            return IsHungryDaughter;
        }

        private bool SkipsWork()
        {
            return IsHungryJuniorColleague && IsTieredJuniorColleague;
        }

        public void Warn(bool isDaughter)
        {
            string warnMessage;
            if (isDaughter)
            {
                warnMessage = "こらっ、" + DaughterName + "ちゃん！！　だめだぞ！！！";
                ShowWarnMessage(PlaysTrick(), warnMessage);
            
            }
            else
            {
                warnMessage = JuniorColleagueName + "さん、僕も仕事をサボりたいよ。";
                ShowWarnMessage(SkipsWork(), warnMessage);
            }
        }

        private void ShowWarnMessage(bool DoneBadThing, string warnMessage)
        {
            if (DoneBadThing && IsSleepyTaro)
            {
                Console.WriteLine(warnMessage);
            }
            else
            {
                Console.WriteLine("特に叱る必要もないか...");
            }
        }
    }
}
