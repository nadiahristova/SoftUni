namespace ProductsShop.Data
{
    using ProductsShop.Data.Migrations;
    using ProductsShop.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ShopEntities : DbContext
    {
        public ShopEntities()
            : base("ShopEntities")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<ShopEntities, Configuration>();
            Database.SetInitializer(migrationStrategy);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            modelBuilder.Entity<User>()
               .HasMany(u => u.Friends)
               .WithMany()
               .Map(m =>
               {
                   m.ToTable("UserFrinds");
                   m.MapLeftKey("UserId");
                   m.MapRightKey("FrindId");
               });

            modelBuilder.Entity<User>()
                .HasMany(u => u.ProductsBought)
                .WithOptional(p => p.Buyer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(p => p.ProductsSold)
                .WithRequired(p => p.Seller)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .Map(m =>
                {
                    m.ToTable("CategoryProducts");
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("CategoryId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}