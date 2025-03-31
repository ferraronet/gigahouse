using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProductMedias.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<ProductMedia, GetListResult>();

        CreateMap<GetListResult, GetListResponse>();

        CreateMap<GetListRequest, GetListCommand>();
    }
}
