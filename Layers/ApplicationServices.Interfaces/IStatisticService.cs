using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.ApplicationServices.Interfaces
{
    public interface IStatisticService
    {
        Task WriteStatisticAsync(string area, int productId);
        Task WriteStatisticAsync(string area, IEnumerable<int> productIds);
    }
}
