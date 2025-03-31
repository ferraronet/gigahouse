using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using MediatR;

namespace GigaHouse.Application.Products.GetPriceList;

public record GetPriceListCommand : IRequest<GetPriceListResult>
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }
}
