using Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.MsSql
{
    public class AppDbContext : ReadOnlyAppDbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

    }
}
