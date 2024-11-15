using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private IRepository<Customer> customerRepository;

        public CustomerController(ShoppingContext context)
        {
            customerRepository = new CustomerRepository(context);
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                var customers = customerRepository.All();
                return View(customers);
            }
            else
            {
                var customers = new[] { customerRepository.Get(id.Value) };
                return View(customers);
            }
        }
    }
}
