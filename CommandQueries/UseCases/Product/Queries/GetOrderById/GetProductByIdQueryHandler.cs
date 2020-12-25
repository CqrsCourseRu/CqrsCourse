using ApplicationServices.Interfaces;
using AutoMapper;
using Handlers.UseCases.Common.Queries.GetEntityById;
using Infrastructure.Interfaces;

namespace Handlers.UseCases.Order.Queries.GetOrderById
{
    public class GetProductByIdQueryHandler : GetEntityByIdQueryHandler<GetProductByIdQuery, Entities.Product, ProductDto>
    {

        public GetProductByIdQueryHandler(IReadOnlyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        
    }
}
