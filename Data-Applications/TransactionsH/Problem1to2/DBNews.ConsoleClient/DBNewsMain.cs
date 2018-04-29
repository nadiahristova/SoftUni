using DBNews.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBNews.ConsoleClient
{
    class DBNewsMain
    {
        static void Main()
        {
            var ctxUser1 = new NewsEntities();
            bool updateFailed = false;
            Console.WriteLine("Application started.");
            Console.WriteLine("Text from DB: " + ctxUser1.DbNews.FirstOrDefault().NewsContent + "\nEnter the corrected text:");
            ctxUser1.DbNews.FirstOrDefault().NewsContent = "Baby pandas team up to resist their medication";


            var ctxUser2 = new NewsEntities();
            Console.WriteLine("Application started.");
            Console.WriteLine("Text from DB: " + ctxUser1.DbNews.FirstOrDefault().NewsContent + "\nEnter the corrected text:");
            ctxUser2.DbNews.FirstOrDefault().NewsContent = @"Two young pandas were unimpressed when a breeder tried to feed them medicine instead of their usual bamboo in southwest China's Chengdu.
The keeper was trying to use a syringe to put medicine in the cubs' mouths but, as this video shows, they resisted by rolling over and climbing on top of him.";

            try
            {
                ctxUser1.SaveChanges();
                Console.WriteLine("Changes successfully saved in the DB.");
            }
            catch (Exception)
            {
                Console.WriteLine("Conflict!");
                updateFailed = true;
            }

            try
            {
                ctxUser2.SaveChanges();
                Console.WriteLine("Changes successfully saved in the DB.");
            }
            catch (Exception)
            {
                Console.WriteLine("Conflict!");
                updateFailed = true;                
            }

            while (updateFailed)
            {
                using(var ctx = new NewsEntities())
                {
                    Console.WriteLine("Text from DB: " + ctx.DbNews.FirstOrDefault().NewsContent + ". \nEnter the corrected text:");
                    ctx.DbNews.FirstOrDefault().NewsContent = @"Two young pandas were unimpressed when a breeder tried to feed them medicine instead of their usual bamboo in southwest China's Chengdu.
The keeper was trying to use a syringe to put medicine in the cubs' mouths but, as this video shows, they resisted by rolling over and climbing on top of him.";
                    try
                    {
                        ctx.SaveChanges();
                        Console.WriteLine("Changes successfully saved in the DB.");
                        updateFailed = false;
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Another user is accessing this entry atm. Please try again later.");
                    }
                }
            }            
        }
    }
}
