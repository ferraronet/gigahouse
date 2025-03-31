using GigaHouse.Core.Enums;

namespace GigaHouse.Application.ProjectProducts.GetList;

public class GetListResult
{
    public Guid Id { get; set; }

    public string MetaKeywords { get; set; } = string.Empty;

    public string MetaTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}
