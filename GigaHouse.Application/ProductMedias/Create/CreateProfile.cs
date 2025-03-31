using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProductMedias.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, ProductMedia>();
        CreateMap<ProductMedia, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
