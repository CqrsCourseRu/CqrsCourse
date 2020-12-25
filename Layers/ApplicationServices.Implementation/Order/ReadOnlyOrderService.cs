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
    public class ReadOnlyOrderService : ReadOnlyEntityService<Order, OrderDto>, IReadOnlyOrderService
    {

        public ReadOnlyOrderService(IReadOnlyDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

    }
}
