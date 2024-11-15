using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using MyShop.Infrastructure;

namespace MyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IUnitOfWork _uow;
        public OrdersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
              if (_uow.OrderRepository == null)
              {
                  return NotFound();
              }
            var products = await _uow.OrderRepository.AllAsync();
            return products.ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_uow.OrderRepository == null)
          {
              return NotFound();
          }
            var order = await _uow.OrderRepository.GetAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            var bestaat = await OrderExists(id);
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _uow.OrderRepository.Update(order);

            try
            {
                await _uow.ProductRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bestaat)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_uow.OrderRepository == null)
          {
              return Problem("Entity set 'ShoppingContext.Orders'  is null.");
          }
            _uow.OrderRepository.Add(order);
            await _uow.OrderRepository.SaveAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_uow.OrderRepository == null)
            {
                return NotFound();
            }
            var order = await _uow.OrderRepository.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _uow.OrderRepository.Delete(order.OrderID);
            await _uow.OrderRepository.SaveAsync();
            return NoContent();
        }

        private async Task<bool> OrderExists(int id)
        {
            var item = await _uow.OrderRepository.FindAsync(e => e.OrderID == id);
            return item.Any();
        }
    }
}
