namespace BookShopSystem.Data
{
    using BookShopSystem.Data.Migrations;
    using BookShopSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity;
    using System.Linq;
    using BookShopSystem.Data.Models;

    public class BookShopEntities : IdentityDbContext<ApplicationUser>, IBookShopData 
    {
        public BookShopEntities()
            : base("BookShopEntities")
        {
            var migrateStrategy = new MigrateDatabaseToLatestVersion<BookShopEntities, Configuration>();
            Database.SetInitializer(migrateStrategy);
        }

        public virtual IDbSet<Author> Authors { get; set; }
        public virtual IDbSet<Book> Books { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }

        public static BookShopEntities Create()
        {
            return new BookShopEntities();
        }
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}