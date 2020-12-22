using System.Threading;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public interface IDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }

        DbSet<T> Set<T>() where T : Entity;

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
