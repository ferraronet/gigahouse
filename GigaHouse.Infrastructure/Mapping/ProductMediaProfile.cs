using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.HandlersLayer.ProductMedias;

namespace GigaHouse.Infrastructure.Mapping
{
    public class ProductMediaProfile : Profile
    {
        public ProductMediaProfile()
        {
            CreateMap<ProductMedia, MessageProductMedia>().ReverseMap();
        }
    }
}
