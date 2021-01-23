using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using Handlers.CqrsFramework;

namespace Handlers.UseCases.Order.Queries.GetOrderResultById
{
    public class GetOrderResultByIdQuery : IRequest<Result<OrderDto>>, ICheckOrderResultRequest
    {
        public int Id { get; set; }
    }
}
