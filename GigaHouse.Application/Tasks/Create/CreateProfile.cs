using AutoMapper;

namespace GigaHouse.Application.Tasks.Create;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CreateCommand, Data.Domain.Task>();
        CreateMap<Data.Domain.Task, CreateResult>();

        CreateMap<CreateRequest, CreateResponse>();
        CreateMap<CreateRequest, CreateCommand>();
        CreateMap<CreateResult, CreateResponse>();
    }
}
