using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectCssSelectors.Get;

public class GetProfile : Profile
{
    public GetProfile()
    {
        CreateMap<ProjectCssSelector, GetResult>();

        CreateMap<GetResult, GetResponse>();

        CreateMap<Guid, GetCommand>()
            .ConstructUsing(id => new GetCommand(id));
    }
}
