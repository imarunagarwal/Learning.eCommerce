using AutoMapper;
using UserWebApi.DataAccessLayer.Entities;
using UserWebApi.SharedLayer.Dtos;

namespace UserWebApi.SharedLayer.AutoMapperProfile
{
    public class DtoEntityMapper :Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<UserDto, UserEntity>().ReverseMap();
        }
    }
}
