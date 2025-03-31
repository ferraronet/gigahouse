using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.UserProducts.Get;

public class GetProfile : Profile
{
    public GetProfile()
    {
        CreateMap<UserProduct, GetResult>();

        CreateMap<GetResult, GetResponse>();

        CreateMap<Guid, GetCommand>()
            .ConstructUsing(id => new GetCommand(id));
    }
}
