using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Products.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<Product, GetListResult>();

        CreateMap<GetListResult, GetListResponse>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<GetListRequest, GetListCommand>();
    }
}
