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
        [HttpGet("{id}")]
        public Task<OrderDto> GetByIdAsync(int id, [FromServices] IQueryHandler<GetOrderByIdQuery, OrderDto> handler)
        {
            return handler.HandleAsync(new GetOrderByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] ChangeOrderDto dto, [FromServices] ICommandHandler<CreateOrderCommand> handler)
        {
            var command = new CreateOrderCommand {Dto = dto};
            await handler.HandleAsync(command);

            return command.Id;
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeOrderDto dto, [FromServices] ICommandHandler<UpdateOrderCommand> handler)
        {
            return handler.HandleAsync(new UpdateOrderCommand { Id = id, Dto = dto });
        }
    }
}
