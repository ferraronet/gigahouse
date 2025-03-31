using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Projects.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, Project>();
        CreateMap<Project, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
