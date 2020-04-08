using AutoMapper;
using ProductWebApi.SharedLayer.Dtos;
using ProductWebApi.WebApi.ViewModels;

namespace ProductWebApi.SharedLayer.AutoMapperProfile
{
    public class DtoVmMapper : Profile
    {
        public DtoVmMapper()
        {
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<CartCheckoutDto, CartCheckoutViewModel>().ReverseMap();
        }
    }
}
