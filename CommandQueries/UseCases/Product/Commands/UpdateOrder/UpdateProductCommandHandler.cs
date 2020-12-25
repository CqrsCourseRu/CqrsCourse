using AutoMapper;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public class UpdateProductCommandHandler : UpdateEntityCommandHandler<UpdateProductCommand, Entities.Product, ChangeProductDto>
    {

        public UpdateProductCommandHandler(IDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
