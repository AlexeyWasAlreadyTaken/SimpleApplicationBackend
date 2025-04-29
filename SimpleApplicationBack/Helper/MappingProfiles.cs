using AutoMapper;
using SimpleApplicationBack.DTO;
using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO,Product>();


            CreateMap<Order, OrderDTO>();
            CreateMap<OrderProduct, OrderProductDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderProductDTO, OrderProduct>();
        }
    }
}
