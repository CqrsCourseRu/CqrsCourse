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
    public class CreateProductCommandHandler : CreateEntityCommandHandler<CreateProductCommand, Entities.Product, ChangeProductDto>
    {
        public CreateProductCommandHandler(IDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }
    }
}
