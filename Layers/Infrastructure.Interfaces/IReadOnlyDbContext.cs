using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public interface IReadOnlyDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }

        DbSet<T> Set<T>() where T : Entity;
    }
}