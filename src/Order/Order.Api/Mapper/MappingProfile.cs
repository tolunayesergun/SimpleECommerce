using AutoMapper;
using Core.Models.Product;
using Order.Api.Models.DTOs;

namespace Order.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDetailDTO, ProductDetail>().ReverseMap();
        }
    }
}