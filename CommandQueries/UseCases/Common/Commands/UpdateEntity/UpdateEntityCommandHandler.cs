using System.Threading.Tasks;
using AutoMapper;
using CQ.CqrsFramework;
using Entities;
using Infrastructure.Interfaces;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public abstract class UpdateEntityCommandHandler<TRequest, TEntity, TDto> : ICommandHandler<TRequest>
        where TEntity : Entity
        where TRequest : UpdateEntityCommand<TDto>
    {
        protected readonly IDbContext DbContext;
        private readonly IMapper _mapper;

        protected UpdateEntityCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task HandleAsync(TRequest request)
        {
            var entity = await GetTrackedEntityAsync(request.Id);
            _mapper.Map(request.Dto, entity);
            await DbContext.SaveChangesAsync();
        }

        protected virtual async Task<TEntity> GetTrackedEntityAsync(int id)
        {
            var entity = await DbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }
    }
}
