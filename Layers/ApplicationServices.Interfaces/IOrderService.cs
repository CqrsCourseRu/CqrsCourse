using System.Threading.Tasks;
using Layers.ApplicationServices.Interfaces;

namespace ApplicationServices.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetByIdAsync(int id);
        Task<int> CreateAsync(ChangeOrderDto dto);
        Task UpdateAsync(int id, ChangeOrderDto dto);
    }
}
