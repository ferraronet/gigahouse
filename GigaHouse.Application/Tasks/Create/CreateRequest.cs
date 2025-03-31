namespace GigaHouse.Application.Tasks.Create;

public class CreateRequest
{
    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}