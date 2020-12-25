using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handlers.CqrsFramework;
using Handlers.UseCases.Common.Queries.GetEntityById;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order.Queries.GetOrderById
{
    public class GetProductByIdQueryHandler : GetEntityByIdQueryHandler<GetProductByIdQuery, Entities.Product, ProductDto>
    {

        public GetProductByIdQueryHandler(IReadOnlyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        
    }
}
