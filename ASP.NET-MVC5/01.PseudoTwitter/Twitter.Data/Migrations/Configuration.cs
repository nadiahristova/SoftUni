namespace Twitter.Data.Migrations
{
    using Models;

    using System.Linq;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;    

    public sealed class Configuration : DbMigrationsConfiguration<Twitter.Data.TwitterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Twitter.Data.TwitterContext context)
        {
            if (!context.Users.Any())
            {
                var manager = new UserManager<User>(
               new UserStore<User>(context));

                for (int i = 0; i < 4; i++)
                {
                    var user = new User()
                    {
                        UserName = "Vitka" + i + "@myemail.com",
                        Email = "Vitty" + i + "@myemail.com"
                    };
                    manager.Create(user, "Temp_123" + i);
                }
            }            
        }
    }
}
