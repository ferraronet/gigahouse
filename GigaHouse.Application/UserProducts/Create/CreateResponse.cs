namespace GigaHouse.Application.UserProducts.Create;

public class CreateResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? TaskId { get; set; }
}
