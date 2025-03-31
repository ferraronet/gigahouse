namespace GigaHouse.Infrastructure.HandlersLayer.ProductMedias;

public class MessageProductMedia
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Link { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}
