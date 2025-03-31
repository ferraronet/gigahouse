using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.ProjectProducts.Update;

public class UpdateProfile : Profile
{
    public UpdateProfile()
    {
        CreateMap<UpdateCommand, ProjectProduct>();
        CreateMap<ProjectProduct, UpdateResult>();
        CreateMap<UpdateRequest, UpdateResponse>();
        CreateMap<UpdateRequest, UpdateCommand>();
        CreateMap<UpdateResult, UpdateResponse>();
    }
}
