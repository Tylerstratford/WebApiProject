using Microsoft.EntityFrameworkCore;
using WebApiProject.Models.Entities;

namespace WebApiProject.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        public DbSet<AddressEntity> Address { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<OrderLinesEntity> OrderLines { get; set; }
        public DbSet<OrdersEntity> Orders { get; set; }
        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }
        public DbSet<ProductEntity> Products { get; set; }  

    
    }
}
