using System.Threading.Tasks;

namespace ApplicationServices.Interfaces
{
    public interface IReadOnlyEntityService<TDto>
    {
        Task<TDto> GetByIdAsync(int id);
    }
}