using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.UserProducts.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<UserProduct, GetListResult>();

        CreateMap<GetListResult, GetListResponse>();

        CreateMap<GetListRequest, GetListCommand>();
    }
}
