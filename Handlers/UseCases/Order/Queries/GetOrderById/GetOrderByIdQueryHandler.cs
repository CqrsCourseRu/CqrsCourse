using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handlers.CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> HandleAsync(GetOrderByIdQuery request)
        {
            var result = await _dbContext.Orders
                .Where(x => x.Id == request.Id)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return result;
        }
    }
}
