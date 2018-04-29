using System.Data.Entity;
using System.Linq;

namespace BookShopSystem.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private IDbSet<T> set;
        private IBookShopData context;
        public Repository(): this(new BookShopEntities()) { }
        public Repository(IBookShopData ctx)
        {
            this.context = ctx;
            this.set = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = EntityState.Added;
        }
    }
}
