using MediatR;

namespace GigaHouse.Application.Users.Delete;

public record DeleteCommand : IRequest<DeleteResponse>
{
    public Guid Id { get; }

    public DeleteCommand(Guid id)
    {
        Id = id;
    }
}
