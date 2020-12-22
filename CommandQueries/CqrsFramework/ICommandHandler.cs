using System.Threading.Tasks;

namespace CQ.CqrsFramework
{
    public interface ICommandHandler<TRequest>
    {
        Task HandleAsync(TRequest request);
    }
}