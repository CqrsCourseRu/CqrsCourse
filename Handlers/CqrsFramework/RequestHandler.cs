using System.Threading.Tasks;

namespace Handlers.CqrsFramework
{
    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>
    {
        async Task<Unit> IRequestHandler<TRequest, Unit>.HandleAsync(TRequest request)
        {
            await HandleAsync(request);

            return Unit.Value;
        }

        protected abstract Task HandleAsync(TRequest request);
    }
}