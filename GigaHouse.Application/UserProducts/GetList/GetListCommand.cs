using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using MediatR;

namespace GigaHouse.Application.UserProducts.GetList;

public record GetListCommand : IRequest<PaginatedList<GetListResult>>
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public Guid UserId { get; set; }
}
