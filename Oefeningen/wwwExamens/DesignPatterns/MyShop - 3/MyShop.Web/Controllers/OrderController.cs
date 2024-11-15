using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Models;

namespace MyShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private IRepository<Order> orderRepository;
        private IRepository<Product> productRepository;
        public OrderController(ShoppingContext context)
        {
            orderRepository = new OrderRepository(context);
            productRepository = new ProductRepository(context);
        }

        public IActionResult Index()
        {
            var orders = orderRepository.All();

            return View(orders);
        }


        public IActionResult Create()
        {
            var products = productRepository.All();
            
            return View(products);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderModel model)
        {
            if (!model.LineItems.Any()) return BadRequest("Please submit line items");

            if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");

            var customer = new Customer
            {
                Name = model.Customer.Name,
                ShippingAddress = model.Customer.ShippingAddress,
                City = model.Customer.City,
                PostalCode = model.Customer.PostalCode,
                Country = model.Customer.Country
            };

            var order = new Order
            {
                Orderlines = model.LineItems
                    .Select(line => new Orderline { ProductID = line.ProductID, Quantity = line.Quantity })
                    .ToList(),
                OrderDate = DateTime.Now,
                Customer = customer
            };

            orderRepository.Add(order);
            orderRepository.SaveChanges();

            return Ok("Order Created");
        }

    }
}
