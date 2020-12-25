using System.Collections.Generic;
using Layers.ApplicationServices.Interfaces;

namespace ApplicationServices.Interfaces
{
    public class OrderDto : ChangeOrderDto
    {
        public int Id { get; set; }
    }
}