using System.Threading.Tasks;

namespace Handlers.CqrsFramework
{
    public delegate Task<TResponse> HandlerDelegate<TResponse>();
}