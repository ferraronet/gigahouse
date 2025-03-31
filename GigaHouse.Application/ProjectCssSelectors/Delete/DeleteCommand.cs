using MediatR;

namespace GigaHouse.Application.ProjectCssSelectors.Delete;

public record DeleteCommand : IRequest<DeleteResponse>
{
    public Guid Id { get; }

    public DeleteCommand(Guid id)
    {
        Id = id;
    }
}
