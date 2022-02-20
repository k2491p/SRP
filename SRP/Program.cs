using SRP.Bad;
using SRP.Good;
using System;

namespace SRP
{
    class Program
    {
        static void Main(string[] args)
        {
            // GoodCode
            Console.WriteLine("GoodCode");
            // 娘を叱るたろう
            Daughter daughter = new Daughter("むすめ", true);
            TaroFather taroFather = new TaroFather(daughter, true);
            taroFather.Warn();

            // 後輩を叱るたろう
            JuniorColleague juniorColleague = new JuniorColleague("こうはい");
            TaroWorker taroWorker = new TaroWorker(juniorColleague);
            taroWorker.Warn();


            // BadCode
            Console.WriteLine("BadCode");
            Taro taro = new Taro(false, "むすめ", true, "こうはい", true, true);
            taro.Warn(true);
            taro.Warn(false);

        }
    }
}
