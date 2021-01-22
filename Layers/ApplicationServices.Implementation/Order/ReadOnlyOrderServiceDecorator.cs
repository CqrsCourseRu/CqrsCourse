using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation.Order
{
    public class ReadOnlyOrderServiceDecorator : IReadOnlyOrderService
    {
        private readonly IReadOnlyOrderService _orderService;
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public ReadOnlyOrderServiceDecorator(IReadOnlyOrderService orderService, 
            IDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var count = await _dbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == id);
            if (count != 1) throw new Exception("Order not found");

            return await _orderService.GetByIdAsync(id);
        }
    }
}
