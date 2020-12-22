using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CQ.CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task HandleAsync(UpdateOrderCommand request)
        {
            var order = await _dbContext.Orders
                .Include(x => x.Items)
                .SingleAsync(x => x.Id == request.Id);
            _mapper.Map(request.Dto, order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
