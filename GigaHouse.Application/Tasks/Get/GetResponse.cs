namespace GigaHouse.Application.Tasks.Get;

public class GetResponse
{
    public Guid Id { get; set; }

    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public string Status { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}
