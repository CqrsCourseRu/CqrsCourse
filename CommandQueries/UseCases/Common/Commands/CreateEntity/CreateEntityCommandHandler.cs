using System.Threading.Tasks;
using AutoMapper;
using CQ.CqrsFramework;
using Entities;
using Infrastructure.Interfaces;

namespace Handlers.UseCases.Order.Commands.CreateOrder
{
    public abstract class CreateEntityCommandHandler<TRequest, TEntity, TDto> : ICommandHandler<TRequest>
        where TEntity : Entity
        where TRequest : CreateEntityCommand<TDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        protected CreateEntityCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task HandleAsync(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request.Dto);
            InitializeNewEntity(entity);
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            request.Id = entity.Id;
        }

        protected virtual void InitializeNewEntity(TEntity entity)
        {
        }
    }
}
