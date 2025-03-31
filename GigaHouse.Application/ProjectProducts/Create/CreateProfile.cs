using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectProducts.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, ProjectProduct>();
        CreateMap<ProjectProduct, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
