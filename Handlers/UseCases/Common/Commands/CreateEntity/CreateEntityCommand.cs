using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;
using Layers.ApplicationServices.Interfaces;

namespace Handlers.UseCases.Order.Commands.CreateOrder
{
    public abstract class CreateEntityCommand<TDto> : IRequest<int>
    {
        public TDto Dto { get; set; }
    }
}
