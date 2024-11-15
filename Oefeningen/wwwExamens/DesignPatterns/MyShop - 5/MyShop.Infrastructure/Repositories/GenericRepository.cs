using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        internal ShoppingContext _context;
        internal DbSet<T> table = null;
        public GenericRepository(ShoppingContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        virtual public T Add(T obj)
        {
            table.Add(obj);
            return obj;
        }
        virtual public IEnumerable<T> All()
        {
            return table.ToList();
        }
        virtual public async Task<IEnumerable<T>> AllAsync()
        {
            return await table.ToListAsync();
        }
        virtual public T Get(int id)
        {
            return table.Find(id);
        }

        virtual public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual T Update(T obj)
        {
            table.Update(obj);
            return obj;
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                                                    IOrderedQueryable<T>> orderBy = null,
                                                    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = table;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToList();
        }
        virtual public async Task<T> GetAsync(int id)
        {
            return await table.FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
                                                    IOrderedQueryable<T>> orderBy = null,
                                                    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = table;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }


        virtual public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
    }
}
