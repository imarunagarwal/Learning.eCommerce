using AutoMapper;
using CartWebApi.SharedLayer.Dtos;
using CartWebApi.WebApi.ViewModels;

namespace CartWebApi.SharedLayer.AutoMapperProfile
{
    public class DtoVmMapper : Profile
    {
        public DtoVmMapper()
        {
            CreateMap<ItemsDto, ItemsViewModel>().ReverseMap();
            CreateMap<CartDto, CartViewModel>().ReverseMap();
        }
    }
}
