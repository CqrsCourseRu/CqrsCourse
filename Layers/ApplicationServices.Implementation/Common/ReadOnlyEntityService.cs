using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation
{
    public abstract class ReadOnlyEntityService<TEntity, TDto> : IReadOnlyEntityService<TDto>
        where TEntity : Entity
    {
        protected readonly IReadOnlyDbContext DbContext;
        private readonly IMapper _mapper;

        protected ReadOnlyEntityService(IReadOnlyDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var result = await DbContext.Set<TEntity>()
                .Where(x => x.Id == id)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return result;
        }

    }
}
