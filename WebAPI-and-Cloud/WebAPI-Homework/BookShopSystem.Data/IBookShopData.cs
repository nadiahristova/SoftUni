using BookShopSystem.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BookShopSystem.Data
{
    public interface IBookShopData
    {
        IDbSet<Author> Authors { get; set; }
        IDbSet<Book> Books { get; set; }
        IDbSet<Category> Categories { get; set; }

        void SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
