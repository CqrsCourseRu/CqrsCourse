using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation.Order
{
    public class OrderServiceDecorator : IOrderService
    {
        private readonly IOrderService _orderService;
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public OrderServiceDecorator(IOrderService orderService,
            IDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        public Task<int> CreateAsync(ChangeOrderDto dto)
        {
            return _orderService.CreateAsync(dto);
        }

        public async Task UpdateAsync(int id, ChangeOrderDto dto)
        {
            var count = await _dbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == id);
            if (count != 1) throw new Exception("Order not found");

            await _orderService.UpdateAsync(id, dto);
        }

        public Task DeleteAsync(int id)
        {
            return _orderService.DeleteAsync(id);
        }
    }
}
