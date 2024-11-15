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
        private IUnitOfWork _uow;
        public OrderController(UnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var orders = _uow.OrderRepository.All();

            return View(orders);
        }


        public IActionResult Create()
        {
            var products = _uow.ProductRepository.All();
            
            return View(products);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderModel model)
        {
            if (!model.LineItems.Any()) return BadRequest("Please submit line items");

            if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");
            var customer = _uow.CustomerRepository.Find(filter: x=>x.Name == model.Customer.Name).SingleOrDefault();
            if (customer == null)
            {
                customer = new Customer
                {
                    Name = model.Customer.Name,
                    ShippingAddress = model.Customer.ShippingAddress,
                    City = model.Customer.City,
                    PostalCode = model.Customer.PostalCode,
                    Country = model.Customer.Country
                };
                _uow.CustomerRepository.Add(customer);
            } else
            {
                customer.PostalCode = model.Customer.PostalCode;
                customer.City = model.Customer.City;
                customer.Country = model.Customer.Country;
                customer.ShippingAddress = model.Customer.ShippingAddress;
                _uow.CustomerRepository.Update(customer);
            }

            var order = new Order
            {
                Orderlines = model.LineItems
                    .Select(line => new Orderline { ProductID = line.ProductID, Quantity = line.Quantity })
                    .ToList(),
                OrderDate = DateTime.Now,
                Customer = customer
            };

            _uow.OrderRepository.Add(order);
            _uow.OrderRepository.SaveChanges();

            return Ok("Order Created");
        }

    }
}
