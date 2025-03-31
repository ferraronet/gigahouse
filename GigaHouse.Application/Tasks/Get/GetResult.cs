namespace GigaHouse.Application.Tasks.Get;

public class GetResult
{
    public Guid Id { get; set; }

    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public Core.Enums.TaskStatus Status { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}
