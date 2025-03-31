using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Tasks.Get;

public class GetProfile : Profile
{
    public GetProfile()
    {
        CreateMap<Data.Domain.Task, GetResult>();

        CreateMap<GetResult, GetResponse>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Guid, GetCommand>()
            .ConstructUsing(id => new GetCommand(id));
    }
}
