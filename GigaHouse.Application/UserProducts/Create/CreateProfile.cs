using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.UserProducts.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, UserProduct>();
        CreateMap<UserProduct, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
