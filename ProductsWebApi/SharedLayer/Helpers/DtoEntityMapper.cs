using AutoMapper;
using ProductWebApi.DataAccessLayer.Entities;
using ProductWebApi.SharedLayer.Dtos;

namespace ProductWebApi.SharedLayer.AutoMapperProfile
{
    public class DtoEntityMapper :Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<ProductDto, ProductEntity>().ReverseMap();
        }
    }
}
