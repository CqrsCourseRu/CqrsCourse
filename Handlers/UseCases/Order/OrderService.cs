using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handlers.CqrsFramework;
using Handlers.UseCases.Order.Commands.CreateOrder;
using Handlers.UseCases.Order.Commands.UpdateOrder;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order
{
    [Obsolete]
    public class OrderService : 
        IRequestHandler<CreateOrderCommand, int>,
        IRequestHandler<UpdateOrderCommand>,
        IRequestHandler<GetOrderByIdQuery, OrderDto>

    {
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(IStatisticService statisticService, 
            IMapper mapper, 
            IDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _statisticService = statisticService;
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<int> HandleAsync(CreateOrderCommand request)
        {
            await _statisticService.WriteStatisticAsync("Order", request.Dto.Items.Select(x => x.ProductId));

            var entity = _mapper.Map<Entities.Order>(request.Dto);
            entity.UserEmail = _currentUserService.Email;
            _dbContext.Set<Entities.Order>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Unit> HandleAsync(UpdateOrderCommand request)
        {
            var entity = await _dbContext.Orders
                .Include(x => x.Items)
                .SingleAsync(x => x.Id == request.Id);
            _mapper.Map(request.Dto, entity);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<OrderDto> HandleAsync(GetOrderByIdQuery request)
        {
            var result = await _dbContext.Set<Entities.Order>()
                .Where(x => x.Id == request.Id)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return result;
        }
    }
}
