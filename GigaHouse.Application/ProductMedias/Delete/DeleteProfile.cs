using AutoMapper;

namespace GigaHouse.Application.ProductMedias.Delete;

public class DeleteProfile : Profile
{
    public DeleteProfile()
    {
        CreateMap<Guid, DeleteCommand>()
            .ConstructUsing(id => new DeleteCommand(id));
    }
}
