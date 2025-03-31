using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectProducts.Get;

public class GetProfile : Profile
{
    public GetProfile()
    {
        CreateMap<ProjectProduct, GetResult>();

        CreateMap<GetResult, GetResponse>();

        CreateMap<Guid, GetCommand>()
            .ConstructUsing(id => new GetCommand(id));
    }
}
