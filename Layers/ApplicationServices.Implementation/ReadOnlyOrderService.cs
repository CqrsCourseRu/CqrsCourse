using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation
{
    public class ReadOnlyOrderService : IReadOnlyOrderService
    {
        private readonly IReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReadOnlyOrderService(IReadOnlyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var result = await _dbContext.Orders
                .Where(x => x.Id == id)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return result;
        }

    }
}
