using AutoMapper;
using CartWebApi.DataAccessLayer.Entities;
using CartWebApi.SharedLayer.Dtos;

namespace CartWebApi.SharedLayer.AutoMapperProfile
{
    public class DtoEntityMapper :Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<CartDto, CartEntity>().ReverseMap();
            CreateMap<ItemsDto, ItemsEntity>().ReverseMap();
        }
    }
}
