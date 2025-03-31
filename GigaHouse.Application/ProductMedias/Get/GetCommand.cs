using MediatR;

namespace GigaHouse.Application.ProductMedias.Get;

public record GetCommand : IRequest<GetResult>
{
    public Guid Id { get; }

    public GetCommand(Guid id)
    {
        Id = id;
    }
}
