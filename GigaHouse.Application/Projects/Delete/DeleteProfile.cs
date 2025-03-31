using AutoMapper;

namespace GigaHouse.Application.Projects.Delete;

public class DeleteProfile : Profile
{
    public DeleteProfile()
    {
        CreateMap<Guid, DeleteCommand>()
            .ConstructUsing(id => new DeleteCommand(id));
    }
}
