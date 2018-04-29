using System;
using System.Collections.Generic;
using System.Linq;
using DbAddsContext;
using System.Data.Entity;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace PlayWithToList
{
    class PlayWithToList
    {
        static void Main()
        {
            var sw = new Stopwatch();
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

        private static string optimizedLoop(Stopwatch sw, int i)
        {
            var ctx = new AdsEntities();
            var b = ctx.Ads.Count();
            ctx.Database.ExecuteSqlCommand(@"CHECKPOINT");
            ctx.Database.ExecuteSqlCommand(@"DBCC DROPCLEANBUFFERS");
            ctx.Database.ExecuteSqlCommand(@"DBCC FREEPROCCACHE");
            sw.Restart();            
            var allAdsOptimized = ctx.Ads.Where(a => a.AdStatus.Status == "Published")
                .OrderBy(a => a.Date)
                .Select(a => new
                {
                    a.Title,
                    a.Category,
                    a.Town
                }).ToList();
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("Optimized    Run{0}: {1}", i+1, time);
            Console.WriteLine();
            return time.ToString();
        }

        private static string nonOptimizedLoop(Stopwatch sw, int i)
        {
            var ctx = new AdsEntities();
            var b = ctx.Ads.Count();
            //string txt = "CHECKPOINT;GO;DBCC DROPCLEANBUFFERS;GO;DBCC FREEPROCCACHE;GO;";
            //using (var connection = new SqlConnection("Data Source=.;Initial Catalog=Ads;Integrated Security=True"))//also not working
            //{
            //    using (var command = connection.CreateCommand())
            //    {
            //        connection.Open();
            //        command.CommandText = txt;
            //    }
            //}
            ctx.Database.ExecuteSqlCommand(@"CHECKPOINT");
            ctx.Database.ExecuteSqlCommand(@"DBCC DROPCLEANBUFFERS");
            ctx.Database.ExecuteSqlCommand(@"DBCC FREEPROCCACHE");
            sw.Restart();
            var allAds = ctx.Ads.ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    a.Title,
                    a.Category,
                    a.Town,
                    a.Date
                }).ToList()
                .OrderBy(a => a.Date);
            var time = sw.ElapsedMilliseconds; 
            Console.WriteLine("Non-optimized    Run{0}: {1}", i + 1, time);
            return time.ToString();
        }
    }
}
