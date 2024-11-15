using MyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Domain.Models;

namespace MyShop.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Order> OrderRepository { get; }
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Product> ProductRepository { get; }
        void SaveChanges();
    }
}
