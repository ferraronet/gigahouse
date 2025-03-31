using GigaHouse.Data.Domain;
using GigaHouse.Core.Enums;

namespace GigaHouse.Infrastructure.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
