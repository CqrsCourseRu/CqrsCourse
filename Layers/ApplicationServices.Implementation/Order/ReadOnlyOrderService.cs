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
        private readonly ICurrentUserService _currentUserService;

        public ReadOnlyOrderService(IReadOnlyDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService) 
            : base(dbContext, mapper)
        {
            _currentUserService = currentUserService;
        }

        public override async Task<OrderDto> GetByIdAsync(int id)
        {
            var count = await DbContext.Orders.CountAsync(
                x => x.UserEmail == _currentUserService.Email && x.Id == id);
            if (count != 1) throw new Exception("Order not found");

            return await base.GetByIdAsync(id);
        }
    }
}
