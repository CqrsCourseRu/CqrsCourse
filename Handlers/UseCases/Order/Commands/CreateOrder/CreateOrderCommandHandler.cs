using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using Handlers.CqrsFramework;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;

namespace Handlers.UseCases.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : CreateEntityCommandHandler<CreateOrderCommand, Entities.Order, ChangeOrderDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IStatisticService _statisticService;

        public CreateOrderCommandHandler(IDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IStatisticService statisticService) : base(dbContext, mapper)
        {
            _currentUserService = currentUserService;
            _statisticService = statisticService;
        }

        public override async Task<int> HandleAsync(CreateOrderCommand request)
        {
            await _statisticService.WriteStatisticAsync("Order", request.Dto.Items.Select(x => x.ProductId));

            return await base.HandleAsync(request);
        }

        protected override void InitializeNewEntity(Entities.Order entity)
        {
            entity.UserEmail = _currentUserService.Email;
        }
    }
}
