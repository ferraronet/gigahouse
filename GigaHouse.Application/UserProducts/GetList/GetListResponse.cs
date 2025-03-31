namespace GigaHouse.Application.UserProducts.GetList;

public class GetListResponse
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? TaskId { get; set; }
}
