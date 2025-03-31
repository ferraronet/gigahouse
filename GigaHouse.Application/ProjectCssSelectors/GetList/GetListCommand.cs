using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using MediatR;

namespace GigaHouse.Application.ProjectCssSelectors.GetList;

public record GetListCommand : IRequest<PaginatedList<GetListResult>>
{
    public Guid ProjectId { get; set; }
}
