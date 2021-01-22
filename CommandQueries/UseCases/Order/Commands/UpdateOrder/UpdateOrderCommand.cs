using CQ.UseCases.Order;
using Layers.ApplicationServices.Interfaces;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public class UpdateOrderCommand : UpdateEntityCommand<ChangeOrderDto>, ICheckOrderRequest
    {
    }
}
