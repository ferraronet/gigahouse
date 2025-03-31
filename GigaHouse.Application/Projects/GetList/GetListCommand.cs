using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using MediatR;

namespace GigaHouse.Application.Projects.GetList;

public record GetListCommand : IRequest<PaginatedList<GetListResult>>
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string? Name { get; set; }

    public ProjectStatus? Status { get; set; }
}
