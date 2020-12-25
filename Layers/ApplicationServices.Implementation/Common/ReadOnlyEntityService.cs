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
        private readonly IReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;

        protected ReadOnlyEntityService(IReadOnlyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var result = await _dbContext.Set<TEntity>()
                .Where(x => x.Id == id)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return result;
        }

    }
}
