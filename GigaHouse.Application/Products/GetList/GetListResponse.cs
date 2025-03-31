namespace GigaHouse.Application.Products.GetList;

public class GetListResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Gtin { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
