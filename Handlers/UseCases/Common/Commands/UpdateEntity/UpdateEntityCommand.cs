using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;
using Layers.ApplicationServices.Interfaces;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public abstract class UpdateEntityCommand<TDto> : IRequest
    {
        public int Id { get; set; }
        public TDto Dto { get; set; }
    }
}
