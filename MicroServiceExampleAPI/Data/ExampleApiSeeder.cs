using System;
using System.Collections.Generic;
using System.Linq;
using MicroServiceExampleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceExampleAPI.Data
{
    public class ExampleApiSeeder
    {
        private readonly ExampleApiContext _context;

        public ExampleApiSeeder(ExampleApiContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                // Need to create sample data
                var products = new[] {
                    new Product { Category = "A", Price = 1, Size = "2x4" },
                    new Product { Category = "B", Price = 2, Size = "4x8" },
                };

                _context.Products.AddRange(products);

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem> {
                        new OrderItem {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}
