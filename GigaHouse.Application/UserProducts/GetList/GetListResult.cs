using GigaHouse.Core.Enums;

namespace GigaHouse.Application.UserProducts.GetList;

public class GetListResult
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? TaskId { get; set; }
}
