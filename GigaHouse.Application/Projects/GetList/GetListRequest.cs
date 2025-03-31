using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Projects.GetList;

public class GetListRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Name { get; set; }

    public ProjectStatus? Status { get; set; }
}
