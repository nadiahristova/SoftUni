using System;
using System.Collections.Generic;
using System.Linq;
using DbAddsContext;
using System.Diagnostics;
using System.Data.Entity;

namespace ShowDataFromRelatedTables
{
    class ShowDataFromRelatedTables
    {
        static void Main()
        {
            var context = new AdsEntities();
            var c = context.Ads.Count();
            var sw = new Stopwatch();

            sw.Start();
            var allAddsStaightforward = context.Ads;
            PrintAdInfo(context, allAddsStaightforward);            
            Console.WriteLine("\nTime for execution: " + sw.Elapsed);

            Console.WriteLine("\n\n");

            sw.Restart();
            var allAddsPartialWithInclude = context.Ads
                                            .Include(a => a.AdStatus)
                                            .Include(a => a.Category)
                                            .Include(a => a.Town)
                                            .Include(a => a.AspNetUser);
            PrintAdInfo(context, allAddsPartialWithInclude);
            Console.WriteLine("\nTime for execution: " + sw.Elapsed);
            sw.Stop();
        }

        private static void PrintAdInfo(AdsEntities ctx, IQueryable<Ad> ads)
        {
 	        foreach (var ad in ads)
            {
                Console.WriteLine("Ad's title: {0}, Status: {1}, Category: {2}, Town: {3}, Created by: {4}",
                    ad.Title,
                    ad.AdStatus.Status,
                    ad.Category == null ? "<<Unknown Category>>" : ad.Category.Name,
                    ad.Town == null ? "<<No Data for Town>>" : ad.Town.Name,
                    ad.AspNetUser.Name);
            }
            //not all of the Ads's table columns are required. So we need to chech whether we have an existing connection between the tables, before we try to join them
        }

    }
}
