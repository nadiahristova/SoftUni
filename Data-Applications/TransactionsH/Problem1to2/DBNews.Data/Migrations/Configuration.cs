namespace DBNews.Data.Migrations
{
    using DBNews.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<DBNews.Data.NewsEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DBNews.Data.NewsEntities context)
        {       
            var ctx = new NewsEntities();
            var random = new Random();
            if(ctx.DbNews.Count() == 0)
            for(var i = 0; i<100; i++)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";                
                int length = random.Next(25) + 5;
                var randomContent = new string(
                    Enumerable.Repeat(chars, length)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                ctx.DbNews.Add(new News()
                {
                    NewsContent = randomContent + "..."
                });
            }

            ctx.SaveChanges();
        }
    }
}
