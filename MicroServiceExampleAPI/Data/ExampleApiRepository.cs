using System.Collections.Generic;
using System.Linq;
using MicroServiceExampleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceExampleAPI.Data
{
    public class ExampleApiRepository : IExampleApiRepository
    {
        private readonly ExampleApiContext _context;

        public ExampleApiRepository(ExampleApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts(){
            return _context.Products
                           .OrderBy(prop => prop.Category)
                           .ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                           .Where(prop => prop.Category == category)
                           .OrderBy(prop => prop.Category)
                           .ToList();
        }

        public IEnumerable<Order> GetAllOrders() {
            return _context.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .OrderBy(prop => prop.OrderDate)
                           .ToList();
        }

        public void AddEntity(object model) {
            _context.Add(model);
        }

        public bool SaveAll() {
            return _context.SaveChanges() > 0;
        }
    }
}
    