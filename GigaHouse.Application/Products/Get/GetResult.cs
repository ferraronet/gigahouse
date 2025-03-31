using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Products.Get;

public class GetResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Gtin { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }
}
