﻿using Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DAL.Data
{
    public class DBInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            //Add products
            context.AddRange(
                new Product { Name = "Red Woody pajama", Price = 15.99M },
                new Product { Name = "Blue cap", Price = 5.99M },
                new Product { Name = "Green t-shirt", Price = 20.99M }
                );
            context.AddRange(
                new User { UserName = "micclo", Password = "test", CustomerId = 1 },
                new User { UserName = "jos", Password = "test", CustomerId = 2 }
                );

            //Add customers
            Customer customerMichael = new Customer()
            {
                FirstName = "Michaël",
                LastName = "Cloots",
                Email = "michael.cloots@thomasmore.be"
            };

            Customer customerJos = new Customer()
            {
                FirstName = "Jos",
                LastName = "Jossen",
                Email = "jos.jossen@thomasmore.be"
            };
            context.Add(customerMichael);
            context.Add(customerJos);

            //Orders
            Order order = new Order()
            {
                OrderPlaced = DateTime.Now,
                CustomerId = 1
            };

            Order order2 = new Order()
            {
                OrderPlaced = DateTime.Now.AddDays(-7),
                CustomerId = 1
            };

            context.Add(order);
            context.Add(order2);

            //ProductOrders
            ProductOrder po = new ProductOrder()
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 5
            };

            ProductOrder po2 = new ProductOrder()
            {
                OrderId = 2,
                ProductId = 2,
                Quantity = 2
            };

            ProductOrder po3 = new ProductOrder()
            {
                OrderId = 1,
                ProductId = 2,
                Quantity = 10
            };

            context.Add(po);
            context.Add(po2);
            context.Add(po3);


            context.SaveChanges();
        }
    }
}
