using AutoMapper;
using GigaHouse.Data.Domain;

namespace GigaHouse.Application.Tasks.Update;

public class UpdateProfile : Profile
{
    public UpdateProfile()
    {
        CreateMap<UpdateCommand, Data.Domain.Task>();
        CreateMap<Data.Domain.Task, UpdateResult>();
        CreateMap<UpdateRequest, UpdateResponse>();
        CreateMap<UpdateRequest, UpdateCommand>();
        CreateMap<UpdateResult, UpdateResponse>();
    }
}
