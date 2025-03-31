using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Tasks.GetList;

public class GetListProfile : Profile
{
    public GetListProfile()
    {
        CreateMap<Data.Domain.Task, GetListResult>();

        CreateMap<GetListResult, GetListResponse>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<GetListRequest, GetListCommand>();
    }
}
