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
    public class OrdersController : ControllerBase
    {
        private readonly IHandlerDispatcher _handlerDispatcher;

        public OrdersController(IHandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        [HttpGet("{id}")]
        public Task<OrderDto> GetByIdAsync(int id)
        {
            return _handlerDispatcher.SendAsync(new GetOrderByIdQuery {Id = id});
        }

        [HttpPost]
        public Task<int> CreateAsync([FromBody] ChangeOrderDto dto)
        {
            return _handlerDispatcher.SendAsync(new CreateOrderCommand {Dto = dto});
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeOrderDto dto)
        {
            return _handlerDispatcher.SendAsync(new UpdateOrderCommand {Id = id, Dto = dto});
        }

    }
}
