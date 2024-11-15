using System.Linq.Expressions;

namespace MvcMovie.DAL
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        T GetByID(int id);
        void Insert(T obj);
        void Delete(int id);
        void Update(T obj);
        //void Save();
    }
}
