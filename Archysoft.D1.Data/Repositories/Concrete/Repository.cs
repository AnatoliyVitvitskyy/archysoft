using System.Linq;
using Archysoft.D1.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Archysoft.D1.Data.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        public DataContext Context { get; }

        private readonly DbSet<T> _entities;

        public Repository(DataContext dataContext)
        {
            Context = dataContext;
            _entities = Context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _entities.AsQueryable();
        }

        public IQueryable<T> GetReadonly()
        {
            return _entities.AsNoTracking().AsQueryable();
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void DetachAll()
        {
            foreach (var dbEntityEntry in Context.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                    dbEntityEntry.State = EntityState.Detached;
            }
        }
    }
}
