using MediatR;

namespace GigaHouse.Application.Projects.Get;

public record GetCommand : IRequest<GetResult>
{
    public Guid Id { get; }

    public GetCommand(Guid id)
    {
        Id = id;
    }
}
