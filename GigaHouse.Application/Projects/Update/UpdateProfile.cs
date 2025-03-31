using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Projects.Update;

public class UpdateProfile : Profile
{
    public UpdateProfile()
    {
        CreateMap<UpdateCommand, Project>();
        CreateMap<Project, UpdateResult>();
        CreateMap<UpdateRequest, UpdateResponse>();
        CreateMap<UpdateRequest, UpdateCommand>();
        CreateMap<UpdateResult, UpdateResponse>();
    }
}
