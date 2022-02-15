using Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.MsSql
{
    public class ReadOnlyAppDbContext : AppDbContext, IReadOnlyDbContext
    {
        public ReadOnlyAppDbContext(DbContextOptions<ReadOnlyAppDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        IQueryable<Order> IReadOnlyDbContext.Orders => Orders;
        IQueryable<Product> IReadOnlyDbContext.Products => Products;
        IQueryable<T> IReadOnlyDbContext.Query<T>() => Set<T>();
    }
}