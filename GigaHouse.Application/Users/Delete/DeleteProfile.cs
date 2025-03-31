using AutoMapper;

namespace GigaHouse.Application.Users.Delete;

public class DeleteUserProfile : Profile
{
    public DeleteUserProfile()
    {
        CreateMap<Guid, DeleteCommand>()
            .ConstructUsing(id => new DeleteCommand(id));
    }
}
