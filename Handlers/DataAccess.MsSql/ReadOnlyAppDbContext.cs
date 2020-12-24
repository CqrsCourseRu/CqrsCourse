using Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.MsSql
{
    public class ReadOnlyAppDbContext : DbContext, IReadOnlyDbContext
    {
        public ReadOnlyAppDbContext(DbContextOptions<ReadOnlyAppDbContext> options) : this((DbContextOptions)options)
        {
        }

        protected ReadOnlyAppDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

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