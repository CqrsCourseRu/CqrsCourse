using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Handlers.UseCases.Common.Queries.GetEntityById;

namespace Handlers.UseCases.Order.Queries.GetOrderById
{
    public class GetOrderByIdQuery : GetEntityByIdQuery<OrderDto>, ICheckOrderRequest
    {
    }
}
