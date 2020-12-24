using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IDbContext : IReadOnlyDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
