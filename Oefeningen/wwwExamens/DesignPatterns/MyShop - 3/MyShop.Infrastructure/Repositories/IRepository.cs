using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        T Add(T obj);
        T Update(T obj);
        T Get(int id);
        IEnumerable<T> All();
        void SaveChanges();
    }
}
