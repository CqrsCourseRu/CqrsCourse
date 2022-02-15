using System.Linq;
using Entities;

namespace Infrastructure.Interfaces
{
    public interface IReadOnlyDbContext
    {
        IQueryable<Order> Orders { get; }
        IQueryable<Product> Products { get; }

        IQueryable<T> Query<T>() where T : Entity;
    }
}