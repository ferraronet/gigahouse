namespace GigaHouse.Application.Tasks.Create;

public class CreateResult
{
    public Guid Id { get; set; }

    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public string Status { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}
