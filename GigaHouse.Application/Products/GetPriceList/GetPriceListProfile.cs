using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Models.Caches;

namespace GigaHouse.Application.Products.GetPriceList;

public class GetPriceListProfile : Profile
{
    public GetPriceListProfile()
    {
        CreateMap<UserProductPriceLog, UserProductPriceResult>().ReverseMap();
        CreateMap<UserProductLog, GetPriceListResult>()
            .ForMember(dest => dest.ProductPrices, opt => opt.MapFrom(src => src.ProductPrices));

        CreateMap<UserProductPriceResult, UserProductPriceResponse>().ReverseMap();
        CreateMap<GetPriceListResult, GetPriceListResponse>()
            .ForMember(dest => dest.ProductPrices, opt => opt.MapFrom(src => src.ProductPrices));

        CreateMap<GetPriceListRequest, GetPriceListCommand>();
    }
}
