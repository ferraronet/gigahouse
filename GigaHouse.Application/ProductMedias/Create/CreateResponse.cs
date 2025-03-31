namespace GigaHouse.Application.ProductMedias.Create;

public class CreateResponse
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Link { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}
