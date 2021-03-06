﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Handlers.CqrsFramework;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : UpdateEntityCommandHandler<UpdateOrderCommand, Entities.Order, ChangeOrderDto>
    {

        public UpdateOrderCommandHandler(IDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override async Task<Entities.Order> GetTrackedEntityAsync(int id)
        {
            var order = await DbContext.Orders
                .Include(x => x.Items)
                .SingleAsync(x => x.Id == id);
            return order;
        }
    }
}
