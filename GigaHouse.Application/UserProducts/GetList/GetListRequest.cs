using GigaHouse.Core.Enums;

namespace GigaHouse.Application.UserProducts.GetList;

public class GetListRequest
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public Guid UserId { get; set; }
}
