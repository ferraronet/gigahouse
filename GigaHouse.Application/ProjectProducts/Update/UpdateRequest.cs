using GigaHouse.Core.Enums;

namespace GigaHouse.Application.ProjectProducts.Update;

public class UpdateRequest
{
    public Guid Id { get; set; }

    public string MetaKeywords { get; set; } = string.Empty;

    public string MetaTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;
}