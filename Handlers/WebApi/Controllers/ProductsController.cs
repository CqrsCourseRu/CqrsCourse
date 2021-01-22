using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Handlers.CqrsFramework;
using Handlers.UseCases.Order.Commands.CreateOrder;
using Handlers.UseCases.Order.Commands.UpdateOrder;
using Handlers.UseCases.Order.Queries.GetOrderById;
using Handlers.UseCases.Product.Commands.DeleteAllProducts;
using Handlers.UseCases.Product.Commands.DeleteProduct;
using Layers.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IHandlerDispatcher _handlerDispatcher;

        public ProductsController(IHandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        [HttpGet("{id}")]
        public Task<ProductDto> GetByIdAsync(int id)
        {
            return _handlerDispatcher.SendAsync(new GetProductByIdQuery { Id = id});
        }

        [HttpPost]
        public Task<int> CreateAsync([FromBody] ChangeProductDto dto)
        {
            return _handlerDispatcher.SendAsync(new CreateProductCommand { Dto = dto});
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeProductDto dto)
        {
            return _handlerDispatcher.SendAsync(new UpdateProductCommand { Id = id, Dto = dto});
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(int id)
        {
            return _handlerDispatcher.SendAsync(new DeleteProductCommand {Id = id});
        }

        [HttpDelete]
        public Task DeleteAllAsync([FromBody]DeleteAllDto dto)
        {
            return _handlerDispatcher.SendAsync(new DeleteAllProductsCommand {Dto = dto});
        }

    }
}
