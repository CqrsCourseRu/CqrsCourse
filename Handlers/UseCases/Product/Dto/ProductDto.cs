using System.Collections.Generic;
using Layers.ApplicationServices.Interfaces;

namespace ApplicationServices.Interfaces
{
    public class ProductDto : ChangeProductDto
    {
        public int Id { get; set; }
    }
}