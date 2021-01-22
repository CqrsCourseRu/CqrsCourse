using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.CqrsFramework
{
    public interface IMiddleware<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next);
    }
}
