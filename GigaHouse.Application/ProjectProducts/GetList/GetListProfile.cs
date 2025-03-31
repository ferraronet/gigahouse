using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectProducts.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<ProjectProduct, GetListResult>();

        CreateMap<GetListResult, GetListResponse>();

        CreateMap<GetListRequest, GetListCommand>();
    }
}
