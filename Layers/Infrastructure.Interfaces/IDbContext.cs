using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Interfaces
{
    public interface IDbContext : IReadOnlyDbContext
    {
        IDbContextTransaction BeginTransaction();
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
