namespace GigaHouse.Infrastructure.HandlersLayer.UserProducts;

public class MessageUserProduct
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid UserId { get; set; }
}
