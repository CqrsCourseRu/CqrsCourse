using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using CQ.CqrsFramework;
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
        public Task<ProductDto> GetByIdAsync(int id, [FromServices]IQueryHandler<GetProductByIdQuery, ProductDto> handler)
        {
            return handler.HandleAsync(new GetProductByIdQuery { Id = id});
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] ChangeProductDto dto, [FromServices] ICommandHandler<CreateProductCommand> handler)
        {
            var command = new CreateProductCommand {Dto = dto};
            await handler.HandleAsync(command);
            return command.Id;
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeProductDto dto, [FromServices] ICommandHandler<UpdateProductCommand> handler)
        {
            return handler.HandleAsync(new UpdateProductCommand { Id = id, Dto = dto});
        }

    }
}
