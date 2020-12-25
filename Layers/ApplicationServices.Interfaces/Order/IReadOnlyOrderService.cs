using System.Threading.Tasks;

namespace ApplicationServices.Interfaces
{
    public interface IReadOnlyOrderService : IReadOnlyEntityService<OrderDto>
    {
    }
}