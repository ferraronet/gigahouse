using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Products.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, Product>();
        CreateMap<Product, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
