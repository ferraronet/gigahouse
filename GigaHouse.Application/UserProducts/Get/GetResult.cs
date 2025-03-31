using GigaHouse.Core.Enums;

namespace GigaHouse.Application.UserProducts.Get;

public class GetResult
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? TaskId { get; set; }
}
