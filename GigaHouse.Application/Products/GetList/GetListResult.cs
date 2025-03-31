using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Products.GetList;

public class GetListResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Gtin { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;

    public ProductStatus Status { get; set; }
}
