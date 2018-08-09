using System;
using System.Collections.Generic;

namespace MicroServiceExampleAPI.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
