using SRP.Ex02.Good;
using System;

namespace SRP
{
    class Program
    {
        static void Main(string[] args)
        {
            // 娘を叱るたろう
            Daughter daughter = new Daughter("むすめ");
            TaroFather taroFather = new TaroFather(daughter);
            taroFather.Warn();

            // 後輩を叱るたろう
            JuniorColleague juniorColleague = new JuniorColleague("こうはい");
            TaroWorker taroWorker = new TaroWorker(juniorColleague);
            taroWorker.Warn();
        }
    }
}
