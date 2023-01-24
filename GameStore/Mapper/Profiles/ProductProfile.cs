using AutoMapper;
using GameStore.Models;
using GameStore.Models.Dtos;

namespace GameStore.Mapper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductDto>();
        }
    }
}
