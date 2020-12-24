using System.Threading.Tasks;
using Layers.ApplicationServices.Interfaces;

namespace ApplicationServices.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateAsync(ChangeOrderDto dto);
        Task UpdateAsync(int id, ChangeOrderDto dto);
    }
}
