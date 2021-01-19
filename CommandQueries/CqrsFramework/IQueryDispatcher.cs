using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.CqrsFramework
{
    public interface IQueryDispatcher
    {
        Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);
    }
}
