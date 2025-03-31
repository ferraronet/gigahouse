namespace GigaHouse.Application.UserProducts.Create;

public class CreateRequest
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? TaskId { get; set; }
}