using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.HandlersLayer.UserProducts;

namespace GigaHouse.Infrastructure.Mapping
{
    public class UserProductProfile : Profile
    {
        public UserProductProfile()
        {
            CreateMap<UserProduct, MessageUserProduct>().ReverseMap();
        }
    }
}
