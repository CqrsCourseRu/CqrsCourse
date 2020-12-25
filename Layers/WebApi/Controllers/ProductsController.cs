using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationServices.Interfaces;
using Layers.ApplicationServices.Interfaces;
using Layers.ApplicationServices.Interfaces.Product;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IReadOnlyProductService _readOnlyProductService;

        public ProductsController(IProductService productService, IReadOnlyProductService readOnlyProductService)
        {
            _productService = productService;
            _readOnlyProductService = readOnlyProductService;
        }

        [HttpGet("{id}")]
        public Task<ProductDto> GetByIdAsync(int id)
        {
            return _readOnlyProductService.GetByIdAsync(id);
        }

        [HttpPost]
        public Task<int> CreateAsync([FromBody] ChangeProductDto dto)
        {
            return _productService.CreateAsync(dto);
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(int id, [FromBody] ChangeProductDto dto)
        {
            return _productService.UpdateAsync(id, dto);
        }

    }
}
