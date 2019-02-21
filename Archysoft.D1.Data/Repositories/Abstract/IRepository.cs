using System.Linq;

namespace Archysoft.D1.Data.Repositories.Abstract
{
    public interface IRepository<T> where T: class
    {
        DataContext Context { get; }
        IQueryable<T> Get();
        IQueryable<T> GetReadonly();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        void DetachAll();
    }
}
