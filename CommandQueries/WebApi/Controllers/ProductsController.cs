using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using CQ.CqrsFramework;
using CQ.UseCases.Product.Commands.DeleteAllProducts;
using Handlers.UseCases.Order.Commands.CreateOrder;
using Handlers.UseCases.Order.Commands.UpdateOrder;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Handlers.UseCases.Product.Commands.DeleteProduct;
using Layers.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ProductsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id}")]
        public Task<ProductDto> GetByIdAsync(int id)
        {
            return _queryDispatcher.SendAsync(new GetProductByIdQuery { Id = id});
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] ChangeProductDto dto)
        {
            var command = new CreateProductCommand {Dto = dto};
            await _commandDispatcher.SendAsync(command);
            return command.Id;
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeProductDto dto)
        {
            return _commandDispatcher.SendAsync(new UpdateProductCommand { Id = id, Dto = dto});
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(int id)
        {
            return _commandDispatcher.SendAsync(new DeleteProductCommand { Id = id });
        }

        [HttpDelete]
        public Task DeleteAllAsync([FromBody] DeleteAllDto dto)
        {
            return _commandDispatcher.SendAsync(new DeleteAllProductsCommand { Dto = dto });
        }

    }
}
