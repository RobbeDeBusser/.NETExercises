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
        public virtual IQueryable<T> Find(Expression<Func<T, bool>> filter = null) 
        {
            IQueryable<T> query = table;
            if(filter != null)
                query = query.Where(filter);
            return query;
        }
    }
}
