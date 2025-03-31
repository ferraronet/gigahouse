using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProductMedias.Get;

public class GetProfile : Profile
{
    public GetProfile()
    {
        CreateMap<ProductMedia, GetResult>();

        CreateMap<GetResult, GetResponse>();

        CreateMap<Guid, GetCommand>()
            .ConstructUsing(id => new GetCommand(id));
    }
}
