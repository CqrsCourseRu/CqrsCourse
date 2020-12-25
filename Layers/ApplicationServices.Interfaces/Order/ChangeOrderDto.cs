using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;

namespace Layers.ApplicationServices.Interfaces
{
    public class ChangeOrderDto
    {
        public List<OrderItemDto> Items { get; set; }
    }
}
