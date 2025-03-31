namespace GigaHouse.Application.ProjectProducts.Create;

public class CreateRequest
{
    public string MetaKeywords { get; set; } = string.Empty;

    public string MetaTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}