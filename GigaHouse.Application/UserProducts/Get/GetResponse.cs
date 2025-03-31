namespace GigaHouse.Application.UserProducts.Get;

public class GetResponse
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? TaskId { get; set; }
}
