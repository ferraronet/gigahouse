using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Products.GetList;

public class GetListRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Gtin { get; set; }

    public string? Name { get; set; }

    public ProductStatus? Status { get; set; }
}
