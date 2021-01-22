using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;

namespace Handlers.UseCases.Common.Queries.GetEntityById
{
    public abstract class GetEntityByIdQuery<TDto> : IRequest<TDto>
    {
        public int Id { get; set; }
    }
}
