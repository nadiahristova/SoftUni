namespace ATM.Data.Migrations
{
    using ATM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<ATM.Data.ATMEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ATM.Data.ATMEntities context)
        {
            var ctx = new ATMEntities();
            var random = new Random();

            if (ctx.CardAccounts.Count() == 0)
                for (var i = 0; i < 10; i++)
                {
                    var randomCardNumber = random.Next(1000000000).ToString().PadLeft(10, '0');
                    var randomCardPIN = random.Next(10000).ToString().PadLeft(4, '0');
                    var randomBalance = (decimal)(random.Next(100000) + 100);

                    ctx.CardAccounts.Add(new CardAccount()
                    {
                        CardNumber = randomCardNumber,
                        CardPIN = randomCardPIN,
                        CardCash = randomBalance
                    });

                }
            ctx.SaveChanges();
        }
    }
}
