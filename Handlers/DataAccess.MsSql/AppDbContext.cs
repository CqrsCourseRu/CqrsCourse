using Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.MsSql
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<T> Set<T>() where T : Entity => base.Set<T>();

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserEmail = "test@test.test" },
                new Order { Id = 2, UserEmail = "other" });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Price = 1 },
                new Product { Id = 2, Name = "Product 2", Price = 10 });
        }
    }
}
