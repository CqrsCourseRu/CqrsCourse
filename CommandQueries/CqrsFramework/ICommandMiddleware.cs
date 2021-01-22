using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.CqrsFramework
{
    public interface ICommandMiddleware<TRequest>
    {
        Task HandleAsync(TRequest request, CommandHandlerDelegate next);
    }
}
