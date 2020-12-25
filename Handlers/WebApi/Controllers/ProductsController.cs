using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Handlers.CqrsFramework;
using Handlers.UseCases.Order.Commands.CreateOrder;
using Handlers.UseCases.Order.Commands.UpdateOrder;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Layers.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        public Task<ProductDto> GetByIdAsync(int id, [FromServices]IRequestHandler<GetProductByIdQuery, ProductDto> handler)
        {
            return handler.HandleAsync(new GetProductByIdQuery { Id = id});
        }

        [HttpPost]
        public Task<int> CreateAsync([FromBody] ChangeProductDto dto, [FromServices] IRequestHandler<CreateProductCommand, int> handler)
        {
            return handler.HandleAsync(new CreateProductCommand { Dto = dto});
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeProductDto dto, [FromServices]IRequestHandler<UpdateProductCommand> handler)
        {
            return handler.HandleAsync(new UpdateProductCommand { Id = id, Dto = dto});
        }

    }
}
