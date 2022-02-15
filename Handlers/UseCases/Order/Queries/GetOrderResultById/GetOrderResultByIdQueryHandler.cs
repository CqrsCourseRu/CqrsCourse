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

namespace Handlers.UseCases.Order.Queries.GetOrderResultById
{
    public class GetOrderResultByIdQueryHandler : IRequestHandler<GetOrderResultByIdQuery, Result<OrderDto>>
    {
        private readonly IReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderResultByIdQueryHandler(IReadOnlyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<OrderDto>> HandleAsync(GetOrderResultByIdQuery request)
        {
            var orderDto = await _dbContext.Query<Entities.Order>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return orderDto == null ? Result<OrderDto>.Fail() : Result<OrderDto>.Success(orderDto);
        }
    }
}
