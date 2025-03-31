using GigaHouse.Data.Domain;

namespace GigaHouse.Infrastructure.Interfaces.Services;

public interface IUserService
{
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);


    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);


    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
