using System.Collections.Generic;
using MicroServiceExampleAPI.Data.Entities;

namespace MicroServiceExampleAPI.Data
{
    public interface IExampleApiRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders();
        void AddEntity(object model);
        bool SaveAll();
    }
}
