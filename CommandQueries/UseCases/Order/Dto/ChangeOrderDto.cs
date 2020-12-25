using System.Collections.Generic;
using ApplicationServices.Interfaces;

namespace Layers.ApplicationServices.Interfaces
{
    public class ChangeOrderDto
    {
        public List<OrderItemDto> Items { get; set; }
    }
}
