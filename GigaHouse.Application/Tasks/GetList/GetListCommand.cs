using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using MediatR;

namespace GigaHouse.Application.Tasks.GetList;

public record GetListCommand : IRequest<PaginatedList<GetListResult>>
{
    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}
