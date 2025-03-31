using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Tasks.Update;

public class UpdateRequest
{
    public Guid Id { get; set; }

    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public Core.Enums.TaskStatus Status { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}