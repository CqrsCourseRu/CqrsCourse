using System.Threading.Tasks;
using Layers.ApplicationServices.Interfaces;

namespace ApplicationServices.Interfaces
{
    public interface IEntityService<TDto>
    {
        Task<int> CreateAsync(TDto dto);
        Task UpdateAsync(int id, TDto dto);

        Task DeleteAsync(int id);
    }
}
