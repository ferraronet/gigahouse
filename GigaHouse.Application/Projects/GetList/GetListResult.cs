using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Projects.GetList;

public class GetListResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }
}
