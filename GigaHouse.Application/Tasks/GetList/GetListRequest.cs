using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Tasks.GetList;

public class GetListRequest
{
    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }
}
