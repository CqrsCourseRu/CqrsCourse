using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;

namespace Layers.ApplicationServices.Interfaces.Product
{
    public interface IProductService : IEntityService<ChangeProductDto>
    {
        Task DeleteAllAsync(DeleteAllDto dto);
    }
}
