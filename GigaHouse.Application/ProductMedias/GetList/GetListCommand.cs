using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using MediatR;

namespace GigaHouse.Application.ProductMedias.GetList;

public record GetListCommand : IRequest<PaginatedList<GetListResult>>
{
    public Guid ProductId { get; set; }
}
