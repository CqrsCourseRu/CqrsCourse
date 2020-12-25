using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQ.CqrsFramework;
using Entities;
using Infrastructure.Interfaces;

namespace Handlers.UseCases.Common.Commands.DeleteEntity
{
    public abstract class DeleteEntityCommandHandler<TRequest, TEntity> : ICommandHandler<TRequest>
        where TRequest : DeleteEntityCommand
        where TEntity : Entity, new()
    {
        private readonly IDbContext _dbContext;

        protected DeleteEntityCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task HandleAsync(TRequest request)
        {
            _dbContext.Set<TEntity>().Remove(new TEntity {Id = request.Id});
            await _dbContext.SaveChangesAsync();
        }
    }
}
