using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectCssSelectors.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, ProjectCssSelector>();
        CreateMap<ProjectCssSelector, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
