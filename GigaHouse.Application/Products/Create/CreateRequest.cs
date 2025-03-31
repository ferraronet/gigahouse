namespace GigaHouse.Application.Products.Create;

public class CreateRequest
{
    public string Name { get; set; } = string.Empty;

    public string Gtin { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;
}