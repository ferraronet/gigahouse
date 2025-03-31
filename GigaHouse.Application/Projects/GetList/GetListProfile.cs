using AutoMapper;
using GigaHouse.Data.Domain;
using System.Net;

namespace GigaHouse.Application.Projects.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<Project, GetListResult>();

        CreateMap<GetListResult, GetListResponse>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<GetListRequest, GetListCommand>();
    }
}
