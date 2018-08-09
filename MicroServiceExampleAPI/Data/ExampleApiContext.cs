using System;
using MicroServiceExampleAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceExampleAPI.Data
{
    public class ExampleApiContext : DbContext
    {
        public ExampleApiContext(DbContextOptions<ExampleApiContext> options) : base(options) {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
