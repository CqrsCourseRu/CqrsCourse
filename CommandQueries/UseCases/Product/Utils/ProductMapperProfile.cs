using ApplicationServices.Interfaces;
using AutoMapper;
using Entities;
using Layers.ApplicationServices.Interfaces;

namespace ApplicationServices.Implementation
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ChangeProductDto, Product>();
        }
    }
}
