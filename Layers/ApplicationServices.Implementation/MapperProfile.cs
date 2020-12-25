using ApplicationServices.Interfaces;
using AutoMapper;
using Entities;
using Layers.ApplicationServices.Interfaces;
using Layers.ApplicationServices.Interfaces.Product;

namespace ApplicationServices.Implementation
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<ChangeOrderDto, Order>();


            CreateMap<Product, ProductDto>();
            CreateMap<ChangeProductDto, Product>();
        }
    }
}
