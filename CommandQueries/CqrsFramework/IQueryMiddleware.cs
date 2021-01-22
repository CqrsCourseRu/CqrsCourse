using System.Threading.Tasks;

namespace CQ.CqrsFramework
{
    public interface IQueryMiddleware<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, QueryHandlerDelegate<TResponse> next);
    }
}