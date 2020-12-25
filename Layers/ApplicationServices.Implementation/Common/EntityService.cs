using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.Implementation
{
    public abstract class EntityService<TEntity, TDto> : IEntityService<TDto>
        where TEntity : Entity, new()
    {
        protected readonly IDbContext DbContext;
        private readonly IMapper _mapper;

        protected EntityService(IDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<int> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            InitializeNewEntity(entity);
            DbContext.Set<TEntity>().Add(entity);
            await DbContext.SaveChangesAsync();
            return entity.Id;
        }

        protected virtual void InitializeNewEntity(TEntity entity)
        {
        }

        public virtual async Task UpdateAsync(int id, TDto dto)
        {
            var entity = await GetTrackedEntityAsync(id);
            _mapper.Map(dto, entity);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            DbContext.Set<TEntity>().Remove(new TEntity {Id = id});
            await DbContext.SaveChangesAsync();
        }

        protected virtual async Task<TEntity> GetTrackedEntityAsync(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
