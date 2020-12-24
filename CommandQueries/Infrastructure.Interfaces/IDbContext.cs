using System.Threading;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public interface IDbContext : IReadOnlyDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
