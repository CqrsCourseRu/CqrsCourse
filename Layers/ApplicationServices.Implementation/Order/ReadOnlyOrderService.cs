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
using Layers.ApplicationServices.Implementation.Order;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation
{
    public class ReadOnlyOrderService : ReadOnlyEntityService<Entities.Order, OrderDto>, IReadOnlyOrderService
    {
        public ReadOnlyOrderService(IReadOnlyDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        [CheckOrder]
        public override async Task<OrderDto> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }
    }
}
