using Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.MsSql
{
    public class AppDbContext : ReadOnlyAppDbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }
}
