using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities;
using Handlers.CqrsFramework;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Common.Queries.GetEntityById
{
    public abstract class GetEntityByIdQueryHandler<TRequest, TEntity, TDto> : IRequestHandler<TRequest, TDto>
        where TEntity : Entity
        where TRequest : GetEntityByIdQuery<TDto>
    {
        private readonly IReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;

        protected GetEntityByIdQueryHandler(IReadOnlyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<TDto> HandleAsync(TRequest request)
        {
            var result = await _dbContext.Set<TEntity>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return result;
        }
    }
}
