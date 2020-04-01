using AutoMapper;
using UserWebApi.SharedLayer.Dtos;
using UserWebApi.WebApi.ViewModels;

namespace UserWebApi.SharedLayer.AutoMapperProfile
{
    public class DtoVmMapper : Profile
    {
        public DtoVmMapper()
        {
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<LoginUserDto, LoginUserViewModel>().ReverseMap();
        }
    }
}
