using DbAddsContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P3.SelectEverythingORSelectCertainColumns
{
    class SelectEverythingORSelectCertainColumns
    {
        static void Main()
        {
            var sw = new Stopwatch();

            var ctx = new AdsEntities();
            ctx.Database.ExecuteSqlCommand(@"CHECKPOINT");
            ctx.Database.ExecuteSqlCommand(@"DBCC DROPCLEANBUFFERS");
            ctx.Database.ExecuteSqlCommand(@"DBCC FREEPROCCACHE");

            sw.Start();
            string outputNonOptimized, outputOptimized;
            outputNonOptimized = outputOptimized = "";

            for (int i = 0; i < 10; i++)
			{
                outputNonOptimized += string.IsNullOrEmpty(outputNonOptimized) ? nonOptimizedLoop(sw, i) : "    " + nonOptimizedLoop(sw, i);
                outputOptimized += outputOptimized == "" ? optimizedLoop(sw, i) : "    " + optimizedLoop(sw, i);
			}

            outputNonOptimized += "    " + Regex.Split(outputNonOptimized, @"\s+").Select(int.Parse).Average();
            outputOptimized += "    " + Regex.Split(outputOptimized, @"\s+").Select(int.Parse).Average();
            Console.WriteLine(outputNonOptimized);
            Console.WriteLine(outputOptimized);
        }

        private static string nonOptimizedLoop(Stopwatch sw, int i)
        {
            var ctx = new AdsEntities();
            ctx.Database.ExecuteSqlCommand(@"CHECKPOINT");
            ctx.Database.ExecuteSqlCommand(@"DBCC DROPCLEANBUFFERS");
            ctx.Database.ExecuteSqlCommand(@"DBCC FREEPROCCACHE");
            var b = ctx.Ads.Count();
            sw.Restart();
            var allAdsNotOpti = ctx.Ads;
            foreach (var ad in allAdsNotOpti)
            {
                Console.WriteLine(ad.Title);
            }
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("Optimized    Run{0}: {1}", i+1, time);
            Console.WriteLine();
            return time.ToString();
        }

        private static string optimizedLoop(Stopwatch sw, int i)
        {
            var ctx = new AdsEntities();
            ctx.Database.ExecuteSqlCommand(@"CHECKPOINT");
            ctx.Database.ExecuteSqlCommand(@"DBCC DROPCLEANBUFFERS");
            ctx.Database.ExecuteSqlCommand(@"DBCC FREEPROCCACHE");
            var b = ctx.Ads.Count();
            sw.Restart();
            var allAdsOpti = ctx.Ads.Select(a => a.Title);
            foreach(var ad in allAdsOpti)
            {
                Console.WriteLine(ad);
            }
            var time = sw.ElapsedMilliseconds; 
            Console.WriteLine("Non-optimized    Run{0}: {1}", i + 1, time);
            return time.ToString();
        }
    }
}
