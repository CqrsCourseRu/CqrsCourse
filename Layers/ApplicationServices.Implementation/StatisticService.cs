using Layers.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.ApplicationServices.Implementation
{
    public class StatisticService : IStatisticService
    {
        public Task WriteStatisticAsync(string area, int id)
        {
            return Task.CompletedTask;
        }

        public Task WriteStatisticAsync(string area, IEnumerable<int> productIds)
        {
            return Task.CompletedTask;
        }
    }
}
