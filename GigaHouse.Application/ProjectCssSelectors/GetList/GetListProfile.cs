using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectCssSelectors.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<ProjectCssSelector, GetListResult>();

        CreateMap<GetListResult, GetListResponse>();

        CreateMap<GetListRequest, GetListCommand>();
    }
}
