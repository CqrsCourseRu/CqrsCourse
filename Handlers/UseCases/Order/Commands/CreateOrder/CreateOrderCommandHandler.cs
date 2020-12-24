using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using Handlers.CqrsFramework;
using Infrastructure.Interfaces;

namespace Handlers.UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStatisticService _statisticService;

        public CreateOrderCommandHandler(IDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IStatisticService statisticService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _statisticService = statisticService;
        }

        public async Task<int> HandleAsync(CreateOrderCommand request)
        {
            await _statisticService.WriteStatisticAsync("Order", request.Dto.Items.Select(x => x.ProductId));

            var order = _mapper.Map<Entities.Order>(request.Dto);
            order.UserEmail = _currentUserService.Email;
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order.Id;
        }
    }
}
