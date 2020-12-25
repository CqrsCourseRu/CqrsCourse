using AutoMapper;
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
