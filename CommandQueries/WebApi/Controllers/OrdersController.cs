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
    public class OrdersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public OrdersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id}")]
        public Task<OrderDto> GetByIdAsync(int id)
        {
            return _queryDispatcher.SendAsync(new GetOrderByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] ChangeOrderDto dto)
        {
            var command = new CreateOrderCommand {Dto = dto};
            await _commandDispatcher.SendAsync(command);

            return command.Id;
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeOrderDto dto)
        {
            return _commandDispatcher.SendAsync(new UpdateOrderCommand { Id = id, Dto = dto });
        }
    }
}
