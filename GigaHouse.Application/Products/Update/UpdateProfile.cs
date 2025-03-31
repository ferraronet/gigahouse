using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Products.Update;

public class UpdateProfile : Profile
{
    public UpdateProfile()
    {
        CreateMap<UpdateCommand, Product>();
        CreateMap<Product, UpdateResult>();
        CreateMap<UpdateRequest, UpdateResponse>();
        CreateMap<UpdateRequest, UpdateCommand>();
        CreateMap<UpdateResult, UpdateResponse>();
    }
}
