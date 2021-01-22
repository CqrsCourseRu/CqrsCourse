using System.Threading.Tasks;

namespace CQ.CqrsFramework
{
    public delegate Task<TResponse> QueryHandlerDelegate<TResponse>();
}