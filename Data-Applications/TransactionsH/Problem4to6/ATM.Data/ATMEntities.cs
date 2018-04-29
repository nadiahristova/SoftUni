namespace ATM.Data
{
    using ATM.Data.Migrations;
    using ATM.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ATMEntities : DbContext
    {
        public ATMEntities()
            : base("name=ATMEntities")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<ATMEntities, Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        public virtual DbSet<CardAccount> CardAccounts { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}