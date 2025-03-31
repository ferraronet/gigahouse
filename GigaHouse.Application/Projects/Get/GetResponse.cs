namespace GigaHouse.Application.Projects.Get;

public class GetResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
