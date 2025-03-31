using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectCssSelectors.Update;

public class UpdateProfile : Profile
{
    public UpdateProfile()
    {
        CreateMap<UpdateCommand, ProjectCssSelector>();
        CreateMap<ProjectCssSelector, UpdateResult>();
        CreateMap<UpdateRequest, UpdateResponse>();
        CreateMap<UpdateRequest, UpdateCommand>();
        CreateMap<UpdateResult, UpdateResponse>();
    }
}
