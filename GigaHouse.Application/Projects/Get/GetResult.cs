using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Projects.Get;

public class GetResult
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }
}
