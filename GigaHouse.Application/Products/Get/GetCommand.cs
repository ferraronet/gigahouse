using MediatR;

namespace GigaHouse.Application.Products.Get;

public record GetCommand : IRequest<GetResult>
{
    public Guid Id { get; }

    public GetCommand(Guid id)
    {
        Id = id;
    }
}
