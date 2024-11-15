using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;

namespace MyShop.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(ShoppingContext context) : base(context) { }
        public override Order Update(Order entity)
        {
            var order = _context.Orders
            .Single(o => o.OrderID == entity.OrderID);
            order.Orderlines = entity.Orderlines;
            order.OrderDate = entity.OrderDate;
            return base.Update(order);
        }
        public override IEnumerable<Order> All()
        {
            return _context.Orders.Include(o=>o.Customer).Include(o=>o.Orderlines).ThenInclude(ol=>ol.Product).ToList();
        }
    }
}
